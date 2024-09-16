using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wrapper.Accpac.BatchInsertTemplate;
using Wrapper.Models;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Common;

namespace Wrapper.AccpacTest
{
    [TestClass]
    public class BatchInsertProcessorTests
    {
        private Mock<ILogger<BatchInsertProcessor<object>>> mockLogger;
        private Mock<INotificationMessenger> mockNotificationMessenger;
        private Mock<IOperationContext> mockContext;
        private Mock<IBatchModel<object>> mockBatchModel;

        [TestInitialize]
        public void Setup()
        {
            // Mock dependencies
            mockLogger = new Mock<ILogger<BatchInsertProcessor<object>>>();
            //mockLogger.Setup(x=>x.LogError()).Returns(true);
            mockNotificationMessenger = new Mock<INotificationMessenger>();
            mockContext = new Mock<IOperationContext>();
            mockBatchModel = new Mock<IBatchModel<object>>();

            // Set up AccpacDBLink transaction mocks
            mockContext.Setup(c => c.AccpacDBLink.TransactionBegin(out It.Ref<int>.IsAny));
            mockContext.Setup(c => c.AccpacDBLink.TransactionCommit(out It.Ref<int>.IsAny));

        }

        [TestMethod]
        public async Task InsertAsync_ShouldCallValidateBatchAndPerformBatchInsert()
        {
            // Arrange
            var processor = new MockBatchInsertProcessor(mockLogger.Object, mockNotificationMessenger.Object);
            mockBatchModel.Setup(b => b.Headers).Returns(new List<object> { new object(), new object() });

            // Act
            await processor.InsertAsync(mockContext.Object, mockBatchModel.Object);

            // Assert
            Assert.IsTrue(processor.ValidateBatchCalled);
            Assert.IsTrue(processor.PerformBatchInsertCalled);
        }

        [TestMethod]
        public async Task InsertAsync_ShouldBeginAndEndTransaction()
        {
            // Arrange
            var processor = new MockBatchInsertProcessor(mockLogger.Object, mockNotificationMessenger.Object);
            mockBatchModel.Setup(b => b.Headers).Returns(new List<object> { new object(), new object() });

            // Act
            await processor.InsertAsync(mockContext.Object, mockBatchModel.Object);

            // Assert that transactions are properly started and committed
            mockContext.Verify(c => c.AccpacDBLink.TransactionBegin(out It.Ref<int>.IsAny), Times.Once);
            mockContext.Verify(c => c.AccpacDBLink.TransactionCommit(out It.Ref<int>.IsAny), Times.Once);
        }

        [TestMethod]
        public async Task InsertAsync_ShouldSendProgressNotification()
        {
            // Arrange
            var processor = new MockBatchInsertProcessor(mockLogger.Object, mockNotificationMessenger.Object);
            mockBatchModel.Setup(b => b.Headers).Returns(new List<object> { new object(), new object() });

            // Act
            await processor.InsertAsync(mockContext.Object, mockBatchModel.Object);

            // Verify that progress notification was sent for each header and finalize method
            mockNotificationMessenger.Verify(m => m.NotifyProgressAsync(
                It.IsAny<IOperationContext>(),
                It.IsAny<NotificationModel>()), Times.Exactly(3));
        }

        [TestMethod]
        public async Task InsertAsync_ShouldHandleExceptionsAndLogError()
        {
            // Arrange
            var processor = new MockBatchInsertProcessor(mockLogger.Object, mockNotificationMessenger.Object);
            mockBatchModel.Setup(b => b.Headers).Returns(new List<object> { new object(), new object() });

            // Force an exception during PerformBatchInsertAsync
            processor.ForceExceptionDuringPerformBatchInsert = true;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CreateEntityException>(() =>
                processor.InsertAsync(mockContext.Object, mockBatchModel.Object));

            // Verify that the error was logged
            mockLogger.Verify(l => l.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }

    public class MockBatchInsertProcessor : BatchInsertProcessor<object>
    {
        public bool ValidateBatchCalled { get; private set; }
        public bool PerformBatchInsertCalled { get; private set; }
        public bool ForceExceptionDuringPerformBatchInsert { get; set; }

        public MockBatchInsertProcessor(ILogger logger, INotificationMessenger notificationMessenger)
            : base(logger, notificationMessenger, LongRunningProcessType.NominalcashbookAccpacPosting)
        {
        }

        protected override async Task ValidateBatchAsync(IOperationContext context, IBatchModel<object> batchModel)
        {
            ValidateBatchCalled = true;
            await Task.CompletedTask;
        }

        protected override async Task PerformBatchInsertAsync(IOperationContext context, IBatchModel<object> batchModel, BatchHeaderProgressTrigger trigger)
        {
            PerformBatchInsertCalled = true;

            if (ForceExceptionDuringPerformBatchInsert)
            {
                throw new CreateEntityException("Error during batch insert");
            }

            for (int i = 0; i < batchModel.Headers.Count; i++)
            {
                await trigger(i + 1);
            }

            await Task.CompletedTask;
        }
    }
}

