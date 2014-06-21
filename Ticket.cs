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
    /// EventBrite Ticket Class
    /// Mark Godfrey
    /// April 2014
    /// </summary>
    public class Ticket : Ticketing
    {
        public string id { get; set; }
        public string event_id { get; set; }
        public string is_donation { get; set; }
        public string include_fee { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string display_price { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string max { get; set; }
        public string min { get; set; }
        public string price { get; set; }
        public string quantity_available { get; set; }
        public string quantity_sold { get; set; }
        public string type { get; set; }
        public string visible { get; set; }
        public Ticket ticket { get; set; }

        protected override bool ProcessEventBriteAPIObjectData(string ResponseString)
        {
            JavaScriptSerializer jsonEngine = new JavaScriptSerializer();
            Ticket thisTicket = jsonEngine.Deserialize<Ticket>(ResponseString);            

            return true;
        }

        /// <summary>
        /// Creates this ticket types on EventBrite. Object must be pre-populated
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            string UserToken = "XXX";

            if (thisProfile != null)
            {
                string requestData = "&event_id={0}&description={1}&start_date={2}&end_date={3}&name={4}&is_donation={5}&price={6}&quantity_available={7}&include_fee={8}&min={9}&max={10}";
                requestData = String.Format(requestData, event_id, description, start_date, end_date, name, is_donation, price, quantity_available, include_fee, min, max);

                return ExecuteAPICall("ticket_new", UserToken, requestData);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates this ticket types on EventBrite. Object must be pre-populated
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string UserToken = "XXX";

            if (thisProfile != null)
            {
                string requestData = "&id={0}&description={1}&start_date={2}&end_date={3}&name={4}&is_donation={5}&price={6}&quantity_available={7}&include_fee={8}&min={9}&max={10}";
                requestData = String.Format(requestData, id, description, start_date, end_date, name, is_donation, price, quantity_available, include_fee, min, max);

                return ExecuteAPICall("ticket_update", UserToken, requestData);
            }
            else
            {
                return false;
            }
        }

    }

}
