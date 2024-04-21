using Simple_API_user.Models;

namespace Simple_API_user.Repository.Interface
{
    public interface IApplicantRepository
    {
        Task<int> Create(Applicant model);
        Task<bool> DeleteById(int Id);
        Task<List<Applicant>> GetAll();
        Task<Applicant> GetById(int Id);
        Task<bool> Update(Applicant model);
    }
}
