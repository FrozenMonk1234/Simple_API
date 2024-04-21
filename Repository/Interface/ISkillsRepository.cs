using Simple_API_user.Models;

namespace Simple_API_user.Repository.Interface
{
    public interface ISkillsRepository
    {
        Task<bool> Create(Skill skils);
        Task<bool> DeleteById(int Id);
        Task<List<Skill>> GetAllByApplicantId(int id);
        Task<bool> Update(Skill model);
    }
}
