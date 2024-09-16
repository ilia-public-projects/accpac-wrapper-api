using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wrapper.Accpac.CashbookModule.NominalCashbookBatchServices;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CommonServicesModule;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.AccpacTest.CBModule
{
    [TestClass]
    public class NominalCashbookBatchValidatorTests
    {
        private Mock<ICashbookSetupValidor> mockCashbookSetupValidator;
        private Mock<ICommonServicesValidator> mockCommonServicesValidator;
        private Mock<IGLSetupValidator> mockGLSetupValidator;
        private NominalCashbookBatchValidator validator;

        [TestInitialize]
        public void SetUp()
        {
            mockCashbookSetupValidator = new Mock<ICashbookSetupValidor>();
            mockCommonServicesValidator = new Mock<ICommonServicesValidator>();
            mockGLSetupValidator = new Mock<IGLSetupValidator>();

            validator = new NominalCashbookBatchValidator(
                mockCashbookSetupValidator.Object,
                mockCommonServicesValidator.Object,
                mockGLSetupValidator.Object
            );
        }

        [TestMethod]

        public async Task ValidateBatchAsyncValidModelNoErrors()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "USD",
                        ExchangeRate = 1.0m,
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>
                        {
                            new CashbookBatchDetail
                            {
                                SourceCode = "Src001",
                                AccountCode = "Acc001",
                                DebitAmount = 100.0m,
                                CreditAmount = null
                            }
                        }
                    }
                }
            };

            mockCommonServicesValidator.Setup(v => v.ValidateCurrencyExistsAsync(context, It.IsAny<Validator>(), "USD", 1))
                .Returns(Task.CompletedTask);
            mockCashbookSetupValidator.Setup(v => v.ValidateSourceCodeExistsAsync(context, It.IsAny<Validator>(), "Src001", 1))
                .Returns(Task.CompletedTask);
            mockGLSetupValidator.Setup(v => v.ValidateAccountExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "Acc001", 1))
                .Returns(Task.CompletedTask);

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert
            mockCommonServicesValidator.Verify(v => v.ValidateCurrencyExistsAsync(context, It.IsAny<Validator>(), "USD", 1), Times.Once);
            mockCashbookSetupValidator.Verify(v => v.ValidateSourceCodeExistsAsync(context, It.IsAny<Validator>(), "Src001", 1), Times.Once);
            mockGLSetupValidator.Verify(v => v.ValidateAccountExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "Acc001", 1), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncInvalidBankCodeError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "InvalidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "USD",
                        ExchangeRate = 1.0m,
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>
                        {
                            new CashbookBatchDetail
                            {
                                SourceCode = "Src001",
                                AccountCode = "Acc001",
                                DebitAmount = 100.0m,
                                CreditAmount = null
                            }
                        }
                    }
                }
            };

            mockCashbookSetupValidator.Setup(v => v.ValidateBankExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "InvalidBank", null))
                .Throws(new ValidationException("Bank does not exist"));

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncEmptyHeadersError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>()
            };

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncHeadersWithoutDetailsError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "USD",
                        ExchangeRate = 1.0m,
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>()
                    }
                }
            };

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncInvalidCurrencyError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "InvalidCurrency",
                        ExchangeRate = 1.0m,
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>
                        {
                            new CashbookBatchDetail
                            {
                                SourceCode = "Src001",
                                AccountCode = "Acc001",
                                DebitAmount = 100.0m,
                                CreditAmount = null
                            }
                        }
                    }
                }
            };

            mockCommonServicesValidator.Setup(v => v.ValidateCurrencyExistsAsync(context, It.IsAny<Validator>(), "InvalidCurrency", 1))
                .Throws(new ValidationException("Currency does not exist"));

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncInvalidExchangeRateError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "USD",
                        ExchangeRate = -1.0m, // Invalid exchange rate
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>
                        {
                            new CashbookBatchDetail
                            {
                                SourceCode = "Src001",
                                AccountCode = "Acc001",
                                DebitAmount = 100.0m,
                                CreditAmount = null
                            }
                        }
                    }
                }
            };

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateBatchAsyncInvalidSourceCodeError()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new NominalCashbookBatch
            {
                BankCode = "ValidBank",
                Headers = new List<CashbookBatchHeader>
                {
                    new CashbookBatchHeader
                    {
                        Currency = "USD",
                        ExchangeRate = 1.0m,
                        EntryDate = DateTime.UtcNow,
                        ReferenceNo = "Ref001",
                        Details = new List<CashbookBatchDetail>
                        {
                            new CashbookBatchDetail
                            {
                                SourceCode = "InvalidSourceCode",
                                AccountCode = "Acc001",
                                DebitAmount = 100.0m,
                                CreditAmount = null
                            }
                        }
                    }
                }
            };

            mockCashbookSetupValidator.Setup(v => v.ValidateSourceCodeExistsAsync(context, It.IsAny<Validator>(), "InvalidSourceCode", 1))
                .Throws(new ValidationException("Source code does not exist"));

            // Act
            await validator.ValidateBatchAsync(context, model);

            // Assert


        }
    }
}
