using System.ComponentModel.DataAnnotations;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public class AddLicenseModel : BaseModel
    {
        public AddLicenseModel()
        {

        }

        public AddLicenseModel(BaseModelData modelData, string username, DateTime expireDate, bool isValid)
            : base(modelData)
        {
            Username = username;
            ExpireDate = expireDate;
            IsValid = isValid;
        }

        [Required(ErrorMessage = nameof(GSend.Language.Resources.PleaseEnterAValidLicense))]
        public string NewLicense {  get; set; }

        [Display(Name = nameof(GSend.Language.Resources.RegisteredUser))]
        public string Username { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.ExpireDate))]
        public DateTime ExpireDate { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.LicenseIsValid))] 
        public bool IsValid { get; set; }
    }
}
