namespace Wrapper.Models.Common
{
    /// <summary>
    /// Contains the application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// API authentication secret
        /// </summary>
        public string ApiAuthSecret { get; set; }

        /// <summary>
        /// Username used to login into Accpac application
        /// </summary>
        public string AccpacUsername { get; set; }
        /// <summary>
        /// Password for the Accpac user
        /// </summary>
        public string AccpacPassword { get; set; }
        /// <summary>
        /// Version of Accpac
        /// </summary>
        public string AccpacVersion { get; set; }
        /// <summary>
        /// Company to login to
        /// </summary>
        public string AccpacCompany { get; set; }
        /// <summary>
        /// Database connection string to AccpacCompany database
        /// </summary>
        public string AccpacDbConnectionString { get; set; }

        /// <summary>
        /// Base uri to the notification system API
        /// </summary>
        public string NotificationSystemApiUri { get; set; }

        /// <summary>
        /// API authentication secret for the notification system
        /// </summary>
        public string NotificationSystemApiAuthSecret { get; set; }
    }
}
