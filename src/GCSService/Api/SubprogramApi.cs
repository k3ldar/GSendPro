using System;
using System.Collections.Generic;

using GSendCommon;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public class SubprogramApi : BaseController
    {
        private readonly ISubprograms _subprograms;

        public SubprogramApi(ISubprograms subprograms)
        {
            _subprograms = subprograms ?? throw new ArgumentNullException(nameof(subprograms));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult GetAllSubprograms()
        {
            List<ISubprogram> subprograms = _subprograms.GetAll();

            List<ISubprogram> names = [];

            subprograms.ForEach(sp => names.Add(new SubprogramModel(sp.Name, sp.Description, String.Empty)));

            return GenerateJsonSuccessResponse(names);
        }

        [HttpGet]
        [Route("/SubprogramApi/SubprogramGet/{name}/")]
        [ApiAuthorization]
        public IActionResult SubprogramGet(string name)
        {
            ISubprogram subProgram = _subprograms.Get(name);

            if (subProgram == null)
            {
                return GenerateJsonErrorResponse(400, String.Format(GSend.Language.Resources.SubprogramNotFound, name));
            }

            return GenerateJsonSuccessResponse(subProgram);
        }

        [HttpGet]
        [Route("/SubprogramApi/SubprogramExists/{name}/")]
        [ApiAuthorization]
        public IActionResult SubprogramExists(string name)
        {
            if (String.IsNullOrEmpty(name))
                return GenerateJsonSuccessResponse(false);

            return GenerateJsonSuccessResponse(_subprograms.Exists(name));
        }

        [HttpDelete]
        [Route("/SubprogramApi/SubprogramDelete/{name}/")]
        [ApiAuthorization]
        public IActionResult SubprogramDelete(string name)
        {
            if (String.IsNullOrEmpty(name))
                return GenerateJsonSuccessResponse(false);

            return GenerateJsonSuccessResponse(_subprograms.Delete(name));
        }

        [HttpPut]
        [ApiAuthorization]
        public IActionResult SubprogramUpdate([FromBody] ISubprogram model)
        {
            if (model == null)
                return GenerateJsonErrorResponse(400, "Invalid model");

            _subprograms.Update(model);
            return GenerateJsonSuccessResponse();
        }
    }
}
