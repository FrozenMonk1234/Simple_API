using Simple_API_user.Models;

namespace Simple_API_user.Helpers
{
    public static class MapExtensions
    {
        public static ApplicantModel MapApplicantToModel(this Applicant applicant)
        {
            return new ApplicantModel { Id = applicant.Id, Name = applicant.Name };
        }

        public static Applicant MapModelToApplicant(this ApplicantModel model)
        {
            return new Applicant { Id = model.Id, Name = model.Name };
        }

        public static List<SkillModel> MapSkillListToModelList(this List<Skill> skills)
        {
            var result = new List<SkillModel>();
            for (int i = 0; i < skills.Count; i++)
            {
                result.Add(new SkillModel { Id = skills[i].Id, Name = skills[i].Name, Applicantid = skills[i].Applicantid });
            }
            return result;
        }

        public static List<Skill> MapModelListToSkillList(this List<SkillModel> models)
        {
            var result = new List<Skill>();
            for (int i = 0; i < models.Count; i++)
            {
                result.Add(new Skill { Id = models[i].Id, Name = models[i].Name });
            }
            return result;
        }
    }
}
