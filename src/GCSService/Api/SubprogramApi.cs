using GSendCommon;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public class SubprogramApi : BaseController
    {
        private readonly ISubPrograms _subPrograms;

        public SubprogramApi(ISubPrograms subPrograms)
        {
            _subPrograms = subPrograms ?? throw new ArgumentNullException(nameof(subPrograms));
        }

        [HttpGet]
        public IActionResult GetAllSubprograms()
        {
            List<ISubProgram> subPrograms = _subPrograms.GetAll();

            List<ISubProgram> names = new();

            subPrograms.ForEach(sp => names.Add(new SubProgramModel(sp.Name, sp.Description, String.Empty)));

            return GenerateJsonSuccessResponse(names);
        }

        [HttpGet]
        [Route("/SubprogramApi/SubprogramGet/{name}/")]
        public IActionResult SubprogramGet(string name)
        {
            ISubProgram subProgram = _subPrograms.Get(name);

            if (subProgram == null)
            {
                return GenerateJsonErrorResponse(400, String.Format(GSend.Language.Resources.SubprogramNotFound, name));
            }

            return GenerateJsonSuccessResponse(subProgram);
        }

        [HttpGet]
        [Route("/SubprogramApi/SubprogramExists/{name}/")]
        public IActionResult SubprogramExists(string name)
        {
            if (String.IsNullOrEmpty(name))
                return GenerateJsonSuccessResponse(false);

            return GenerateJsonSuccessResponse(_subPrograms.Exists(name));
        }

        [HttpDelete]
        [Route("/SubprogramApi/SubprogramDelete/{name}/")]
        public IActionResult SubprogramDelete(string name)
        {
            if (String.IsNullOrEmpty(name))
                return GenerateJsonSuccessResponse(false);

            return GenerateJsonSuccessResponse(_subPrograms.Delete(name));
        }

        [HttpPost]
        public IActionResult SubprogramUpdate([FromBody] ISubProgram model)
        {
            if (model == null)
                return GenerateJsonErrorResponse(400, "Invalid model");

            _subPrograms.Update(model);
            return GenerateJsonSuccessResponse();
        }
    }
}
