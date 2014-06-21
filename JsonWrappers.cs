using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace Triplestones.Ticketing
{


    // JSON Nested API handling classes ///////////////////////////////////////////////////

    public class EventBriteError : Exception
    {
        public EventBriteError()
            : base()
        { }

        public EventBriteError(string message)
            : base(message)
        { }

        public EventBriteErrorDetails error { get; set; }

    }

    public class EventBriteErrorDetails
    {
        public string error_type { get; set; }
        public string error_message { get; set; }
    }

    public class EventBriteAuthenticationError : EventBriteError
    {
        public EventBriteAuthenticationError(string message)
            : base(message)
        { }
    }

    public class EventBriteGenericProcessResponse
    {
        public string id { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

    public class OAuthAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }

}
