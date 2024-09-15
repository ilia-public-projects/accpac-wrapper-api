namespace Wrapper.Models.Common
{
    /// <summary>
    /// Represents the notification model used to send progress updates for long-running processes.
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationModel"/> class.
        /// </summary>
        /// <param name="currentEntry">The current entry number being processed.</param>
        /// <param name="totalCount">The total number of entries to be processed.</param>
        /// <param name="progressType">The type of progress being reported (e.g., validation, calculating, importing).</param>
        /// <param name="processType">The type of long-running process for which the progress is being reported (e.g., AP cashbook posting).</param>
        /// <param name="targetUserId">The ID of the user to whom the notification is targeted.</param>
        public NotificationModel(int currentEntry, int totalCount, ProgressType progressType, LongRunningProcessType processType, Guid targetUserId)
        {
            CurrentEntry = currentEntry;
            TotalCount = totalCount;
            ProgressType = progressType;
            ProcessType = processType;
            TargetUserId = targetUserId;
        }

        /// <summary>
        /// Gets or sets the ID of the target user for the notification.
        /// </summary>
        public Guid TargetUserId { get; set; }

        /// <summary>
        /// Gets or sets the current entry number being processed.
        /// </summary>
        public int CurrentEntry { get; set; }

        /// <summary>
        /// Gets or sets the total number of entries to be processed.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the type of progress being reported, such as validation, calculating, or importing.
        /// </summary>
        public ProgressType ProgressType { get; set; }

        /// <summary>
        /// Gets or sets the type of long-running process for which the progress is being reported, such as AP cashbook posting.
        /// </summary>
        public LongRunningProcessType ProcessType { get; set; }
    }

}
