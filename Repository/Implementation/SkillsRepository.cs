using Microsoft.EntityFrameworkCore;
using Simple_API_user.Models;
using Simple_API_user.Repository.Interface;
using System.Reflection.Metadata.Ecma335;

namespace Simple_API_user.Repository.Implementation
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly PostgresContext _context;
        public SkillsRepository(PostgresContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Skill skils)
        {
            await _context.Skills.AddAsync(skils);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteById(int applicantId)
        {
            var delete = await _context.Skills.Where(x => x.Applicantid == applicantId).ToListAsync();
            if (delete.Count > 0)
            {
                for (int i = 0; i < delete.Count; i++)
                {
                    _context.Skills.Remove(delete[i]);
                }
            }

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Skill>> GetAllByApplicantId(int id) => await _context.Skills.Where(x => x.Applicantid == id).ToListAsync();

        public async Task<bool> Update(Skill model)
        {
            var update = _context.Skills.Where(x => x.Id == model.Id).FirstOrDefault();
            if (update != null)
            {
                update.Name = model.Name;
                _context.Skills.Update(update);
            }

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
