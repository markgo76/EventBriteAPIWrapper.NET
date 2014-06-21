using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace Triplestones.Ticketing
{
    /// <summary>
    /// Authentication Class
    /// Contains the EventBrite User Authentication logic
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    /// 
    public class Authentication : Ticketing
    {
        protected override bool ProcessEventBriteAPIObjectData(string ResponseString)
        {
            JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
            Authentication thisEvent = jsonEngine.Deserialize<Authentication>(ResponseString);

            return true;
        }

        /// <summary>
        /// Starts EB Auth Process for user
        /// </summary>
        public static void InitiateAuthProcess()
        {

            // OPTIONAL - Store current URL for redirection after auth

            // Send to EB for Approval
            HttpContext.Current.Response.Redirect("https://www.eventbrite.com.au/oauth/authorize?response_type=code&ref=artshub&client_id=" + "[EVENTBRITE_API_KEY]");

        }

        /// <summary>
        /// Completes EB Auth Process for user
        /// </summary>
        public static string CompleteAuthProcess(HttpContext context)
        {
            // Authorise

            // Get auth code
            string code = context.Request.QueryString["code"];
            string YourEBRef = "[YourEventBriteRewardsRef]"; // OPTIONAL

            // Swap authcode for token
            using (WebClient wc = new WebClient())
            {
                string requestURL = "https://www.eventbrite.com.au/oauth/token";
                string requestData = "ref=" + YourEBRef + "&code=" + code + "&client_secret=" + "[EVENTBRITE_CLIENT_SECRET]" + "&client_id=" + "[EVENTBRITE_API_KEY]" + "&grant_type=authorization_code";
                string response = "";

                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    response = wc.UploadString(requestURL, requestData);

                    JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
                    OAuthAccessToken thisToken = jsonEngine.Deserialize<OAuthAccessToken>(response);

                    // Store token in UserRecord
                    //UserRecord.EventBriteUserToken = thisToken.access_token;
                    //UserRecord.Save();


                    // Redirect User back to where they came from (OPTIONAL)

                    return response;
                }
                catch (Exception ex)
                {

                    Emailing.Emailing.SendSimpleEmail("exception@artshub.com.au", "Error Completing OAuth Token swap", ex.Message);
                    return "Error - " + ex.Message + " : " + ex.StackTrace + " : " + response;
                }
            }            
        }
    }
}
