using System.Threading.Tasks;

namespace WLib.Core.Mobile.Services.Auth
{
    public interface IAuthService<out TUser> where TUser : IAppUser
    {
        /// <summary>
        /// Unique id for each user app usage
        /// </summary>
        string SessionId { get; }
        bool IsLogged();
        void LogOff();

        /// <summary>
        /// Set SessionId/start session and 
        /// </summary>
        void StartSession();
        /// <summary>
        /// Mostly on app destroy
        /// </summary>
        void StopSession();

        Task<bool> AuthFacebook();
        TUser AppUser { get; }

        Task<bool> AuthGoogle();

        Task<bool> MockAuthLogin();
    }
}
