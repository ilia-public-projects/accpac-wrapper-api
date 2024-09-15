using AccpacCOMAPI;

namespace Wrapper.Services.Accpac
{
    /// <summary>
    /// Provides functionality for creating and managing Accpac sessions using COMAPI.
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Creates a new Accpac session using COMAPI for the specified program and view.
        /// </summary>
        /// <param name="programName">The name of the Accpac module, e.g., "CB" for Cashbook or "AP" for Accounts Payable.</param>
        /// <param name="startView">The starting view required to open the session, e.g., "CB0010" for a Cashbook view.</param>
        /// <param name="sessionDate">The date for which the session is created, which determines the session context.</param>
        /// <returns>A new <see cref="AccpacSession"/> instance for the specified program and view.</returns>
        AccpacSession GetSession(string programName, string startView, DateTime sessionDate);
    }

}
