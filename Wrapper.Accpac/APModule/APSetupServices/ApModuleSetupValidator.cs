using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APSetupServices;

namespace Wrapper.Accpac.APModule.APSetupServices
{
    public class ApModuleSetupValidator : IApModuleSetupValidator
    {
        private readonly IVendorLoader vendorLoader;

        public ApModuleSetupValidator(
                IVendorLoader vendorLoader
            )
        {
            this.vendorLoader = vendorLoader;
        }


        public async Task ValidateVendorExistsAndIsActiveAsync(IOperationContext context, Validator validator, string code, int? lineNo)
        { 
            var vendor = await vendorLoader.GetVendorByCodeAsync(context, code);
            if (vendor == null) 
            { 
                validator.WriteErrorAndValidate($"Vendor ({code}) does not exist", lineNo);
            }
        }
    }
}
