using Simple_API_user.Models;

namespace Simple_API_user.ViewModels
{
    public class ApplicantViewModel
    {
        public ApplicantModel Applicant { get; set; }
        public List<SkillModel> Skills { get; set; }
    }
}
