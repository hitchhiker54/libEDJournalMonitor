using System;

namespace libEDJournalMonitor
{
    /// <summary>
    /// Common interface for log, status, cargo and module etc file entries
    /// </summary>
    public interface IEDLog
    {
        void ProcessEvent(ref Commander commander);

        string GetEventName();
        DateTime GetDateTime();
        string GetRawJson();
    }
}
