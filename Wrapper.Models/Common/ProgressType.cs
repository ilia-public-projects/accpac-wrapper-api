namespace Wrapper.Models.Common
{
    /// <summary>
    /// Defines the types of progress that can be reported for a long-running process.
    /// </summary>
    public enum ProgressType
    {
        /// <summary>
        /// Indicates that the process is in the validation phase.
        /// </summary>
        Validation,

        /// <summary>
        /// Indicates that the process is in the calculation phase.
        /// </summary>
        Calculating,

        /// <summary>
        /// Indicates that the process is in the import phase.
        /// </summary>
        Import
    }
}
