using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Triplestones.Ticketing
{
    /// <summary>
    /// Handler class for EventBrite OAuth2 Token exchange
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public class EventBriteOAuth2Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string output = "";

            try
            {
                output = Ticketing.Authentication.CompleteAuthProcess(context);
            }
            catch (Exception ex)
            {
                Emailing.Emailing.EmailBug("Error in Oauth handler", ex.Message + "|" + ex.StackTrace);
                output = ex.Message;
            }


            context.Response.ContentType = "text/html";
            context.Response.Write("<html><body><h2>" + output + "</h2></body></html>");
            context.ApplicationInstance.CompleteRequest();
        }



        public bool IsReusable
        {
            get { return false; }
        }
    }
}
