using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Dwp.Adep.Framework.Management.Web.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace Dwp.Adep.Framework.Management.Web.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}