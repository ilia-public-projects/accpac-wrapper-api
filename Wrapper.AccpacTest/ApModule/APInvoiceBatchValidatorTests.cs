using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wrapper.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models.Accpac.APModels.APSetupModels;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APSetupServices;
using Wrapper.Services.Accpac.CommonServicesModule;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.AccpacTest.ApModule
{
    [TestClass]
    public class APInvoiceBatchValidatorTests
    {
        private Mock<IGLSetupValidator> _mockGLSetupValidator;
        private Mock<IApModuleSetupValidator> _mockApModuleSetupValidator;
        private Mock<ICommonServicesValidator> _mockCommonServicesValidator;
        private Mock<IOptionalFieldValueLoader> _mockOptionalFieldValueLoader;
        private APInvoiceBatchValidator _validator;

        [TestInitialize]
        public void SetUp()
        {
            _mockGLSetupValidator = new Mock<IGLSetupValidator>();
            _mockApModuleSetupValidator = new Mock<IApModuleSetupValidator>();
            _mockCommonServicesValidator = new Mock<ICommonServicesValidator>();
            _mockOptionalFieldValueLoader = new Mock<IOptionalFieldValueLoader>();
            _mockOptionalFieldValueLoader.Setup(x=>x.OptionalFieldDefinedInApLocationAsync(It.IsAny<IOperationContext>(), It.IsAny<string>(), It.IsAny<ApOptionalFieldLocation>())).ReturnsAsync(true);

            _validator = new APInvoiceBatchValidator(
                _mockGLSetupValidator.Object,
                _mockApModuleSetupValidator.Object,
                _mockCommonServicesValidator.Object,
                _mockOptionalFieldValueLoader.Object
            );
        }

        [TestMethod]
        public async Task ValidateCreateInvoiceBatchAsync_ValidModel_NoErrors()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            _mockCommonServicesValidator.Setup(v => v.ValidateFiscalDate(context, It.IsAny<Validator>(), model.BatchDate, null, "Batch date"))
                .Verifiable();
            _mockApModuleSetupValidator.Setup(v => v.ValidateVendorExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "ValidVendor", 1))
                .Returns(Task.CompletedTask)
                .Verifiable();
            _mockGLSetupValidator.Setup(v => v.ValidateAccountExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "ValidAccount", 1))
                .Returns(Task.CompletedTask)
                .Verifiable();
            _mockOptionalFieldValueLoader.Setup(v => v.OptionalFieldDefinedInApLocationAsync(context, It.IsAny<string>(), ApOptionalFieldLocation.Invoices))
                .ReturnsAsync(true)
                .Verifiable();

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
            _mockCommonServicesValidator.Verify(v => v.ValidateFiscalDate(context, It.IsAny<Validator>(), model.BatchDate, null, "Batch date"), Times.Once);
            _mockApModuleSetupValidator.Verify(v => v.ValidateVendorExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "ValidVendor", 1), Times.Once);
            _mockGLSetupValidator.Verify(v => v.ValidateAccountExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "ValidAccount", 1), Times.Once);
            _mockOptionalFieldValueLoader.Verify(v => v.OptionalFieldDefinedInApLocationAsync(context, It.IsAny<string>(), ApOptionalFieldLocation.Invoices), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_NoHeaders_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>()
            };

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_HeadersWithoutDetails_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>()
                    }
                }
            };

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidVendor_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "InvalidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            _mockApModuleSetupValidator.Setup(v => v.ValidateVendorExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "InvalidVendor", 1))
                .Throws(new ValidationException("Vendor does not exist"));

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidAccount_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "InvalidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            _mockGLSetupValidator.Setup(v => v.ValidateAccountExistsAndIsActiveAsync(context, It.IsAny<Validator>(), "InvalidAccount", 1))
                .Throws(new ValidationException("Account does not exist"));

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidTotalAmount_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = -100.0m, // Invalid total amount
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidTransactionType_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = (ApInvoiceBatchTransactionType)999, // Invalid transaction type
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidAmount_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = -500.0m // Invalid amount
                            }
                        }
                    }
                }
            };

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidateCreateInvoiceBatchAsync_InvalidOptionalField_Error()
        {
            // Arrange
            var context = Mock.Of<IOperationContext>();
            var model = new ApInvoiceBatchEntryModel
            {
                BatchDate = DateTime.UtcNow,
                Headers = new List<ApInvoiceBatchHeaderEntryModel>
                {
                    new ApInvoiceBatchHeaderEntryModel
                    {
                        VendorId = "ValidVendor",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNo = "Inv001",
                        TotalAmount = 1000.0m,
                        TransactionType = ApInvoiceBatchTransactionType.CreditNote,
                        CreditCode = "InvalidCreditCode",
                        Details = new List<ApInvoiceBatchDetailEntryModel>
                        {
                            new ApInvoiceBatchDetailEntryModel
                            {
                                AccountId = "ValidAccount",
                                Amount = 500.0m
                            }
                        }
                    }
                }
            };

            _mockCommonServicesValidator.Setup(v => v.ValidateOptionalFieldValueExistsAsync(context, It.IsAny<Validator>(), "InvalidCreditCode", null))
                .ReturnsAsync(new Models.Accpac.CommonServicesModels.OptionalFieldValueModel { OptionalField = "InvalidField" });

            _mockOptionalFieldValueLoader.Setup(v => v.OptionalFieldDefinedInApLocationAsync(context, "InvalidCreditCode", ApOptionalFieldLocation.Invoices))
                .ReturnsAsync(false);

            // Act
            await _validator.ValidateCreateInvoiceBatchAsync(context, model);

            // Assert
        }
    }
}
