using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace libEDJournalMonitor
{
    public class EDLogEntry : IEDLog
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        public string RawJson { get; set; }

        public virtual void ProcessEvent(ref Commander commander)
        {

        }

        public string GetEventName()
        {
            return Event;
        }

        public DateTime GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;

            try
            {
                dateTime = DateTime.ParseExact(Timestamp, "yyyy-MM-ddTHH:mm:ssZ",
                                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Debug.WriteLine("Invalid Timestamp passed to GetDateTime()");
            }

            return dateTime;
        }

        public string GetRawJson()
        {
            return RawJson;
        }
    }
}
