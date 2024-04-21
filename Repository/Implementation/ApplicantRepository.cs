using Microsoft.EntityFrameworkCore;
using Simple_API_user.Models;
using Simple_API_user.Repository.Interface;

namespace Simple_API_user.Repository.Implementation
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly PostgresContext _context;
        public ApplicantRepository(PostgresContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Applicant model)
        {
            await _context.Applicants.AddAsync(model);

            return await _context.SaveChangesAsync() > 0 ? model.Id : 0;
        }

        public async Task<bool> DeleteById(int Id)
        {
            var delete = _context.Applicants.Where(x => x.Id == Id).FirstOrDefault();
            if (delete != null)
            {
                _context.Applicants.Remove(delete);
            }

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Applicant>> GetAll() => await _context.Applicants.ToListAsync();

        public async Task<Applicant> GetById(int Id) => await _context.Applicants.Where(x => x.Id == Id).FirstOrDefaultAsync() ?? new();

        public async Task<bool> Update(Applicant model)
        {
            var update = _context.Applicants.Where(x => x.Id == model.Id).FirstOrDefault();
            if (update != null)
            {
                update.Name = model.Name;
                _context.Applicants.Update(update);
            }

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
