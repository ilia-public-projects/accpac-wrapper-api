using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Services.Common;
using Wrapper.Services;
using Microsoft.Extensions.Logging;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;

namespace Wrapper.AccpacTest.ApModule
{
    [TestClass]
    public class ApInvoiceBatchInsertProcessorTests
    {
        private Mock<ILogger<ApInvoiceBatchInsertProcessor>> mockLogger;
        private Mock<INotificationMessenger> mockNotificationMessenger;
        private Mock<IApInvoiceBatchViewComposer> mockViewComposer;
        private Mock<IAPInvoiceBatchValidator> mockValidator;
        private Mock<IApInvoiceBatchViewProcessor> mockViewProcessor;
        private Mock<IOperationContext> mockContext;
        private Mock<IBatchModel<ApInvoiceBatchHeader>> mockBatchModel;

        private ApInvoiceBatchInsertProcessor processor;

        [TestInitialize]
        public void Setup()
        {
            // Set up mocks
            mockLogger = new Mock<ILogger<ApInvoiceBatchInsertProcessor>>();
            mockNotificationMessenger = new Mock<INotificationMessenger>();
            mockViewComposer = new Mock<IApInvoiceBatchViewComposer>();
            mockValidator = new Mock<IAPInvoiceBatchValidator>();
            mockViewProcessor = new Mock<IApInvoiceBatchViewProcessor>();
            mockContext = new Mock<IOperationContext>();
            mockBatchModel = new Mock<IBatchModel<ApInvoiceBatchHeader>>();

            // Mock behavior for the batch model headers
            mockBatchModel.Setup(b => b.Headers).Returns(new List<ApInvoiceBatchHeader>
            {
                new ApInvoiceBatchHeader(), new ApInvoiceBatchHeader()
            });

            // Set up AccpacDBLink transaction mocks
            mockContext.Setup(c => c.AccpacDBLink.TransactionBegin(out It.Ref<int>.IsAny));
            mockContext.Setup(c => c.AccpacDBLink.TransactionCommit(out It.Ref<int>.IsAny));

            // Initialize the ApInvoiceBatchInsertProcessor with mocked dependencies
            processor = new ApInvoiceBatchInsertProcessor(
                mockLogger.Object,
                mockNotificationMessenger.Object,
                mockViewComposer.Object,
                mockValidator.Object,
                mockViewProcessor.Object
            );
        }

        [TestMethod]
        public async Task InsertAsync_ShouldCallValidateBatchAndPerformBatchInsert()
        {
            // Arrange
            var batchModel = new ApInvoiceBatch(); // Concrete batch model
            batchModel.Headers = new List<ApInvoiceBatchHeader>
            {
                new ApInvoiceBatchHeader { },
                new ApInvoiceBatchHeader { }
            };


            // Act
            await processor.InsertAsync(mockContext.Object, batchModel);

            // Assert that validation was called
            mockValidator.Verify(v => v.ValidateCreateInvoiceBatchAsync(mockContext.Object, It.IsAny<ApInvoiceBatch>()), Times.Once);

            // Assert that the batch was processed (headers and lines)
            mockViewProcessor.Verify(v => v.CreateBatch(mockContext.Object, It.IsAny<ApInvoiceBatchView>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            mockViewProcessor.Verify(v => v.CreateHeader(mockContext.Object, It.IsAny<ApInvoiceBatchView>(), It.IsAny<ApInvoiceBatchHeader>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task InsertAsync_ShouldSendProgressNotification()
        {
            // Arrange
            var batchModel = new ApInvoiceBatch(); // Concrete batch model
            batchModel.Headers = new List<ApInvoiceBatchHeader>
            {
                new ApInvoiceBatchHeader { },
                new ApInvoiceBatchHeader { }
            };

            // Act
            await processor.InsertAsync(mockContext.Object, batchModel);

            // Verify that progress notification was sent for each header
            mockNotificationMessenger.Verify(m => m.NotifyProgressAsync(
                mockContext.Object,
                It.IsAny<NotificationModel>()), Times.Exactly(3)); // Two headers and one finalize 
        }

        [TestMethod]
        public async Task InsertAsync_ShouldHandleExceptionsAndLogError()
        {
            // Arrange
            var batchModel = new ApInvoiceBatch(); // Concrete batch model
            batchModel.Headers = new List<ApInvoiceBatchHeader>
            {
                new ApInvoiceBatchHeader { },
                new ApInvoiceBatchHeader { }
            };

            mockViewProcessor
                .Setup(v => v.CreateBatch(mockContext.Object, It.IsAny<ApInvoiceBatchView>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Throws(new InvalidOperationException("Test exception"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CreateEntityException>(() =>
                processor.InsertAsync(mockContext.Object, batchModel));

            // Verify that the error was logged
            mockLogger.Verify(l => l.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}



