using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices;

namespace Wrapper.Accpac.CashbookModule.CashbookSetupServices
{
    public class CashbookSetupValidor : ICashbookSetupValidor
    {
        private readonly IBankLoader bankLoader;
        private readonly ISourceCodeLoader sourceCodeLoader;

        public CashbookSetupValidor(
                IBankLoader bankLoader,
                ISourceCodeLoader sourceCodeLoader
            )
        {
            this.bankLoader = bankLoader;
            this.sourceCodeLoader = sourceCodeLoader;
        }

        public async Task ValidateBankExistsAndIsActiveAsync(IOperationContext context, Validator validator, string bankCode, int? lineNo)
        {
            List<BankModel> banks = await bankLoader.GetActiveBanksAsync(context);
            BankModel bank = banks.FirstOrDefault(b => b.BankCode == bankCode);
            if (bank == null)
            {
                validator.WriteErrorAndValidate($"Bank ({bankCode}) does not exist", lineNo);
            }

            if (!bank.IsActive)
            {
                validator.WriteErrorAndValidate($"Bank ({bankCode}) is not active", lineNo);
            }
        }

        public async Task ValidateSourceCodeExistsAsync(IOperationContext context, Validator validator, string sourceCode, int? lineNo)
        {
            List<SourceCodeModel> codes = await sourceCodeLoader.GetSourceCodesAsync();
            SourceCodeModel code = codes.FirstOrDefault(b => b.Code == sourceCode);
            if (code == null)
            {
                validator.WriteErrorAndValidate($"Source code ({sourceCode}) does not exist", lineNo);
            }
        }
    }
}
