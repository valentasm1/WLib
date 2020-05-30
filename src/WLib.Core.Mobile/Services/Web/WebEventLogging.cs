using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WLib.Core.Mobile.Services.Auth;

namespace WLib.Core.Mobile.Services.Web
{
    public class EventLog
    {
        public string SessionID { get; set; }

        public string ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string Event { get; set; }
        public string User { get; set; }

        public string Message { get; set; }
        public string MessageAdditional { get; set; }
        public DateTimeOffset Date { get; set; }
    }

    public interface IWebEventLogging
    {
        Task LogToRemote(string objectId, string objectName, string eventName, string message, string messageAdditional);


    }

    public class WebEventLogging : IWebEventLogging
    {
        private readonly IAuthService<IAppUser> _authService;
        private readonly string _submitUrl;
        private readonly HttpClient _webClient;

        public WebEventLogging(IAuthService<IAppUser> authService, string submitUrl)
        {
            _authService = authService;
            _submitUrl = submitUrl;
            _webClient = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
        }

        public virtual async Task LogToRemote(string objectId, string objectName, string eventName, string message, string messageAdditional)
        {
            if (string.IsNullOrEmpty(_submitUrl))
            {
                Console.WriteLine("WebEventLogging has not set _submitUrl");
                return;
            }
            var eventLog = new EventLog();
            eventLog.ObjectId = objectId;
            eventLog.ObjectName = objectName;
            eventLog.Event = eventName.ToString();
            eventLog.Message = message;
            eventLog.MessageAdditional = messageAdditional;

            eventLog.Date = DateTimeOffset.Now;
            eventLog.User = _authService?.AppUser?.Name + " B. " + Xamarin.Essentials.AppInfo.BuildString + " d " + Xamarin.Essentials.DeviceInfo.Platform.ToString();
            eventLog.SessionID = _authService?.SessionId;


            try
            {
                var jsonObj = JsonSerializer.Serialize(eventLog);
                //var jsonObj = JsonConvert.SerializeObject(eventLog);
                _webClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                await _webClient.PostAsync(_submitUrl, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }


    }
}
