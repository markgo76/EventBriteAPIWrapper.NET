using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;


namespace Triplestones.Ticketing
{
    /// <summary>
    /// Base class for ticketing API operations
    /// Includes common API call logic
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public abstract class Ticketing
    {
        protected abstract bool ProcessEventBriteAPIObjectData(string ResponseString);
        public EventBriteGenericProcessResponse process { get; set; }

        /// <summary>
        /// Generic method for talking to the EventBrite API.  Responses are passed back to the child objects for processing. Errors are managed here.
        /// </summary>
        protected bool ExecuteAPICall(string Method, string UserToken, string args)
        {
            using (WebClient wc = new WebClient())
            {
                string requestData = "";
                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers.Add("Authorization", "Bearer" + UserToken);
                    requestData = "https://www.eventbrite.com/json/" + Method + "&app_key=" + "[EVENTBRITE_API_KEY]" + args;
                    string response = wc.UploadString("https://www.eventbrite.com/json/" + Method, requestData);

                    if (response.Contains("error_message"))
                    {
                        // Process JSON error
                        JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
                        EventBriteError thisError = jsonEngine.Deserialize<EventBriteError>(response);

                        if (thisError.error.error_type == "Authentication Error" || thisError.error.error_type == "Authentication Failed") throw new EventBriteAuthenticationError(thisError.error.error_message);
                        throw new EventBriteError(thisError.error.error_message);
                    }
                    else
                    {
                        // PROCESS
                        // Populate child object with deserialized data
                        return ProcessEventBriteAPIObjectData(response);
                    }
                }
                catch (EventBriteAuthenticationError aex)
                {
                    Emailing.Emailing.EmailBug("Error authenticating on EventBrite API", aex.Message + " (" + requestData + ")");
                    
                    // Call auth set up process
                    Authentication.InitiateAuthProcess();

                    return false;


                }
                catch (Exception ex)
                {
                    Emailing.Emailing.EmailBug("Error talking to EventBrite API", ex.Message + " : " + ex.StackTrace + " (" + requestData + ")");
                    return false;
                }

            }
        }

    }


}
