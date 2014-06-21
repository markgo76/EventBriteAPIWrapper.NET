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
    /// EventBrite Venue Class
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public class Venue : Ticketing
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }

        protected override bool ProcessEventBriteAPIObjectData(string ResponseString)
        {
            JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
            Venue thisVenue = jsonEngine.Deserialize<Venue>(ResponseString);

            return true;
        }

        //public bool Create(string UserToken)
        //{
        //    ExecuteAPICall("event_new", UserToken, "&title="+ title + "&description=" + description);
        //    return true;
        //}

        ///// <summary>
        ///// Gets an Event from EventBritw API by its ID
        ///// </summary>
        ///// <param name="EventId"></param>
        ///// <returns></returns>
        //public static Event GetEvent(string EventId, string UserToken)
        //{
        //    Event thisEvent = new Event();
        //    if (thisEvent.ExecuteAPICall("event_get", UserToken, "&id=" + EventId))
        //    {
        //        return thisEvent;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }

}
