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
    /// EventBrite Organizer Class
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public class Organizer : Ticketing
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }

        protected override bool ProcessEventBriteAPIObjectData(string ResponseString)
        {
            JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
            Organizer thisOrganizer = jsonEngine.Deserialize<Organizer>(ResponseString);

            return true;
        }

        public bool Create()
        {
            string UserToken = "XXX";
            string UserName = "Bob Smith";

            if (thisProfile != null)
            {
                string requestData = "&name={0}&description=DefaultOrganizer";
                requestData = string.Format(requestData, UserName);
                return ExecuteAPICall("organizer_new", UserToken, requestData);
            }
            else return false;
        }


    }

}
