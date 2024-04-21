using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Simple_API_user.Helpers;
using Simple_API_user.Models;
using Simple_API_user.Repository.Interface;
using Simple_API_user.ViewModels;

namespace Simple_API_user.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : Controller
    {
        private readonly ISkillsRepository skillsRepository;
        private readonly IApplicantRepository applicantRepository;
        private ILogger<ApplicantController> logger;
        public ApplicantController(IApplicantRepository applicantRepository,
            ISkillsRepository skillsRepository,
            ILogger<ApplicantController> logger)
        {
            this.skillsRepository = skillsRepository;
            this.applicantRepository = applicantRepository;
            this.logger = logger;
        }

        [HttpGet("GetAllApplicants")]
        public async Task<IActionResult> GetAllApplicants()
        {
            logger.LogInformation($"{GetControllerActionNames} Executed");
            try
            {
                List<ApplicantViewModel> result = new List<ApplicantViewModel>();
                var applicants = await applicantRepository.GetAll();
                if (applicants.Count > 0)
                {
                    for (int i = 0; i < applicants.Count; i++)
                    {
                        var applicantSkills = await skillsRepository.GetAllByApplicantId(applicants[i].Id);
                        result.Add(new ApplicantViewModel
                        {
                            Applicant = applicants[i].MapApplicantToModel(),
                            Skills = applicantSkills.MapSkillListToModelList()
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest($"Unable to execute: {GetControllerActionNames}");
            }
        }


        [HttpGet("GetApplicantById")]
        public async Task<IActionResult> GetApplicantById(int Id)
        {
            logger.LogInformation($"{GetControllerActionNames} Executed");
            try
            {
                List<ApplicantViewModel> result = new List<ApplicantViewModel>();
                var applicant = await applicantRepository.GetById(Id);

                if (applicant == new Applicant())
                    return Ok(result);
                else
                {
                    var applicantSkills = await skillsRepository.GetAllByApplicantId(applicant.Id);
                    result.Add(new ApplicantViewModel
                    {
                        Applicant = applicant.MapApplicantToModel(),
                        Skills = applicantSkills.MapSkillListToModelList()
                    });
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"{GetControllerActionNames}: {e.Message}");
                return BadRequest($"Unable to execute: {GetControllerActionNames}");
            }
        }

        [HttpPost("CreateApplicant")]
        public async Task<IActionResult> CreateApplicant([FromBody] ApplicantViewModel applicantViewModel)
        {
            logger.LogInformation($"{GetControllerActionNames} Executed");
            try
            {
                var createApplicantResult = await applicantRepository.Create(applicantViewModel.Applicant.MapModelToApplicant());
                if (createApplicantResult > 0)
                {
                    if (applicantViewModel.Skills.Count > 0)
                    {
                        if (applicantViewModel.Skills.FirstOrDefault()?.Applicantid == applicantViewModel.Applicant.Id)
                        {
                            var applicantSkills = applicantViewModel.Skills.MapModelListToSkillList();
                            for (int i = 0; i < applicantSkills.Count; i++)
                            {
                                applicantSkills[i].Applicantid = createApplicantResult;
                                await skillsRepository.Create(applicantSkills[i]);
                            }
                        }
                    }
                    return Ok(createApplicantResult > 0 ? true : false);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"{GetControllerActionNames}: {e.Message}");
                return BadRequest($"Unable to execute: {GetControllerActionNames}");
            }

        }

        [HttpPost("UpdateApplicant")]
        public async Task<IActionResult> UpdateApplicant([FromBody] ApplicantViewModel applicantViewModel)
        {
            logger.LogInformation($"{GetControllerActionNames} Executed");
            try
            {
                var updateApplicantResult = await applicantRepository.Update(applicantViewModel.Applicant.MapModelToApplicant());
                if (updateApplicantResult)
                {
                    if (applicantViewModel.Skills.Count > 0)
                    {
                        if (applicantViewModel.Skills.FirstOrDefault()?.Applicantid == applicantViewModel.Applicant.Id)
                        {
                            var applicantskills = applicantViewModel.Skills.MapModelListToSkillList();
                            for (var i = 0; i < applicantskills.Count; i++)
                            {
                                await skillsRepository.Update(applicantskills[i]);
                            }
                        }
                    }
                    return Ok(updateApplicantResult);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"{GetControllerActionNames}: {e.Message}");
                return BadRequest($"Unable to execute: {GetControllerActionNames}");
            }

        }

        [HttpGet("DeleteApplicantbyId")]
        public async Task<IActionResult> DeleteApplicantbyId(int Id)
        {
            logger.LogInformation($"{GetControllerActionNames} Executed");
            try
            {
                var deleteSkillsResult = await skillsRepository.DeleteById(Id);
                if (deleteSkillsResult)
                {
                    var deleteApplicantResult = await applicantRepository.DeleteById(Id);
                    return Ok(deleteApplicantResult);
                }

                return Ok(deleteSkillsResult);
            }
            catch (Exception e)
            {
                logger.LogError($"{GetControllerActionNames}: {e.Message}");
                return BadRequest($"Unable to execute: {GetControllerActionNames}");
            }
        }

        private string GetControllerActionNames()
        {
            var ControlledContext = ControllerContext.ActionDescriptor.ControllerName;
            var ControlledAction = ControllerContext.ActionDescriptor.ActionName;
            return $"{ControlledContext} - {ControlledAction}";
        }
    }
}
