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

        public LogEntryType EntryType { get; set; } = LogEntryType.None;

        public virtual void ProcessEvent(ref Commander commander)
        {

        }

        /// <summary>
        /// Translate the string timestamp to DateTime value
        /// </summary>
        /// <returns> DateTime value for entry timestamp or DateTime.MinValue</returns>
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
    }
}
