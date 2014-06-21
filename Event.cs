using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web;
using System.IO;

namespace Triplestones.Ticketing
{
    /// <summary>
    /// EventBrite Core Event Class
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public class Event : Ticketing
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public string timezone { get; set; }
        public string capacity { get; set; }
        public string num_attendee_rows { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string privacy { get; set; }
        public string locale { get; set; }
        public string currency { get; set; }
        public string password { get; set; }
        public string url { get; set; }
        public string logo { get; set; }
        public string logo_ssl { get; set; }
        public string status { get; set; }
        public Event ebevent { get; set; }
        public Venue venue { get; set; }
        public Organizer organizer { get; set; }
        public List<Ticket> tickets { get; set; }

        public string repeats { get; set; }
        public string box_text_color { get; set; }
        public string title_text_color { get; set; }
        public string text_color { get; set; }
        public string timezone_offset { get; set; }
        public string box_header_background_color { get; set; }
        public string background_color { get; set; }
        public string box_border_color { get; set; }
        public string box_background_color { get; set; }
        public string link_color { get; set; }
        public string box_header_text_color { get; set; }

        /// <summary>
        /// Perfroms custom response processing for this object (Event) from the generic return method in Ticketing.cs.  May populate a full object or just the Generic result message contained in the 'process' property.
        /// </summary>
        protected override bool ProcessEventBriteAPIObjectData(string ResponseString)
        {

            JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
            Event thisEvent = jsonEngine.Deserialize<Event>(ResponseString.Replace("event", "ebevent"));

            // Map the process child object back to the calling object
            if (thisEvent.ebevent != null)
            {
                this.id = thisEvent.ebevent.id;
                this.title = thisEvent.ebevent.title;
                this.description = thisEvent.ebevent.description;
                this.start_date = thisEvent.ebevent.start_date;
                this.end_date = thisEvent.ebevent.end_date;
                this.category = thisEvent.ebevent.category;
                this.tags = thisEvent.ebevent.tags;
                this.timezone = thisEvent.ebevent.timezone;
                this.capacity = thisEvent.ebevent.capacity;
                this.num_attendee_rows = thisEvent.ebevent.num_attendee_rows;
                this.created = thisEvent.ebevent.created;
                this.modified = thisEvent.ebevent.modified;
                this.privacy = thisEvent.ebevent.privacy;
                this.locale = thisEvent.ebevent.locale;
                this.currency = thisEvent.ebevent.currency;
                this.password = thisEvent.ebevent.password;
                this.url = thisEvent.ebevent.url;
                this.logo = thisEvent.ebevent.logo;
                this.logo_ssl = thisEvent.ebevent.logo_ssl;
                this.status = thisEvent.ebevent.status;
                this.venue = thisEvent.ebevent.venue;
                this.organizer = thisEvent.ebevent.organizer;
                this.tickets = thisEvent.ebevent.tickets;
                this.venue = thisEvent.ebevent.venue;
            }
            this.process = thisEvent.process;

            return true;
        }

        /// <summary>
        /// Creates this Event on EventBrite. Object must be pre-populated correctly
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            string UserToken = "XXX";

            HttpContext context = HttpContext.Current;

            timezone = "Australia/Melbourne";

            string requestData = "&title={0}&description={1}&start_date={2}&end_date={3}&timezone={4}&privacy={5}&personalized_url={6}&capacity={7}&currency={8}&locale={9}&status={10}";
            requestData = String.Format(requestData, context.Server.UrlEncode(title), context.Server.UrlEncode(description), start_date, end_date, timezone, "0", "", capacity, currency, "en_AU", status);

            return ExecuteAPICall("event_new", UserToken, requestData);
        }

        /// <summary>
        /// Updates this Event on EventBrite. Object must be pre-populated correctly
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string UserToken = "XXX";
            HttpContext context = HttpContext.Current;


            string requestData = "&id={0}&title={1}&description={2}&start_date={3}&end_date={4}&capacity={5}&status={6}";
            requestData = String.Format(requestData, id, context.Server.UrlEncode(title), context.Server.UrlEncode(description), start_date, end_date, capacity, status);

            return ExecuteAPICall("event_update", UserToken, requestData);
        }

        /// <summary>
        /// Gets an Event from EventBrite API by its ID
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public static Event GetEvent(string EventId)
        {
            string UserToken = "XXX";

            Event thisEvent = new Event();
            if (thisEvent.ExecuteAPICall("event_get", UserToken, "&id=" + EventId))
            {
                return thisEvent;
            }
            else
            {
                return null;
            }
        }

    }

}
