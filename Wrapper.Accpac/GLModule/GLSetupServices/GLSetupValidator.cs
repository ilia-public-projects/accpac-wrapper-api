using Wrapper.Models.Accpac.GLModels.GLSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.GLModule.GLSetupServices
{
    public class GLSetupValidator : IGLSetupValidator
    {
        private readonly IGLAccountLoader gLAccountLoader;

        public GLSetupValidator(
                IGLAccountLoader gLAccountLoader
            )
        {
            this.gLAccountLoader = gLAccountLoader;
        }

        public async Task ValidateAccountExistsAndIsActiveAsync(IOperationContext context, Validator validator, string accountCode, int? lineNo)
        {
            List<GLAccountModel> accounts = await gLAccountLoader.GetActiveAccountsAsync(context);
            GLAccountModel account = accounts.FirstOrDefault(b => b.Code == accountCode.Trim());
            if (account == null)
            {
                validator.WriteErrorAndValidate($"GL Account ({accountCode}) does not exist or is not active", lineNo);
            }
        }
    }
}
