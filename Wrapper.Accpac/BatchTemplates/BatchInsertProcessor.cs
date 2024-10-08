﻿using Microsoft.Extensions.Logging;
using Wrapper.Models;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Accpac.BatchInsertTemplate
{
    /// <summary>
    /// Abstract class responsible for handling batch insert operations with 
    /// support for transaction management, validation, and progress notification.
    /// </summary>
    /// <typeparam name="HeaderModel">Represents the model for the batch headers.</typeparam>
    public abstract class BatchInsertProcessor<HeaderModel> where HeaderModel : class
    {
        private readonly ILogger logger;
        private readonly INotificationMessenger notificationMessenger;
        private LongRunningProcessType processType;

        protected BatchInsertProcessor(
            ILogger logger,
            INotificationMessenger notificationMessenger,
            LongRunningProcessType processType
        )
        {
            this.logger = logger;
            this.notificationMessenger = notificationMessenger;
            this.processType = processType;
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes to handle 
        /// batch validation before performing the insert.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="batchModel">The model representing the batch to be validated.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected abstract Task ValidateBatchAsync(IOperationContext context, IBatchModel<HeaderModel> batchModel);

        /// <summary>
        /// Begins a database transaction within the current Accpac session.
        /// </summary>
        /// <param name="context">The operation context used to initiate the transaction.</param>
        protected virtual void BeginTransaction(IOperationContext context)
        {
            // Begin transaction in Accpac database
            context.AccpacDBLink.TransactionBegin(out int p);
        }

        /// <summary>
        /// Commits the current database transaction within the Accpac session.
        /// </summary>
        /// <param name="context">The operation context used to commit the transaction.</param>
        protected virtual void EndTransaction(IOperationContext context)
        {
            // Commit transaction in Accpac database
            context.AccpacDBLink.TransactionCommit(out int p);
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes to handle the 
        /// actual batch insert logic.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="batchModel">The batch model containing the headers to be inserted.</param>
        /// <param name="trigger">Delegate used to trigger progress notification during header insertion.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected abstract Task PerformBatchInsertAsync(IOperationContext context, IBatchModel<HeaderModel> batchModel, BatchHeaderProgressTrigger trigger);

        /// <summary>
        /// Delegate used to notify progress during the batch header insertion process.
        /// </summary>
        /// <param name="currentEntry">The current entry number in the batch being processed.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected delegate Task BatchHeaderProgressTrigger(int currentEntry);

        /// <summary>
        /// Public method to handle the entire batch insert process, including 
        /// validation, transaction management, and notification.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="batchModel">The batch model to be processed.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task InsertAsync(IOperationContext context, IBatchModel<HeaderModel> batchModel)
        {

            await ValidateBatchAsync(context, batchModel);

            int totalEntries = batchModel.Headers.Count;

            BeginTransaction(context);

            try
            {
                // Define the trigger delegate for notifying progress
                BatchHeaderProgressTrigger trigger = async (currentEntry) =>
                {
                    // Notify progress after each header is processed
                    await notificationMessenger.NotifyProgressAsync(context, new NotificationModel(
                        currentEntry,
                        totalEntries,
                        ProgressType.Import,
                        processType,
                        context.UserId));
                };

                // Perform the actual batch insert operation
                await PerformBatchInsertAsync(context, batchModel, trigger);

                EndTransaction(context);
            }
            catch (Exception ex)
            {

                EndTransaction(context);

                ErrorUtils.LogAndThrowAccpacException(context, logger, $"Failed to create batch ({processType})",
                    unhandledExceptionAction: () => throw new CreateEntityException(ex.Message, ex.InnerException));
            }
            finally
            {
                // Ensure progress notification is completed even if an error occurs
                await notificationMessenger.NotifyProgressAsync(context, new NotificationModel
                (
                    totalEntries, totalEntries, ProgressType.Import, processType, context.UserId
                ));
            }
        }
    }

}
