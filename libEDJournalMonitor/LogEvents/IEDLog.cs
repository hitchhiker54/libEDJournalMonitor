using System;

namespace libEDJournalMonitor
{
    /// <summary>
    /// Common interface for log, status, cargo and module etc file entries
    /// </summary>
    interface IEDLog
    {
        void ProcessEvent(ref Commander commander);
        DateTime GetDateTime();
    }
}
