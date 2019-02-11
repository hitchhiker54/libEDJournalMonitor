using System;
using System.IO;

/// <summary>
/// .NET Standard library to monitor and process Elite Dangerous journal logs
/// 
/// store all data in Commander class for consumption
/// 
/// raise events with event name & timestamp for consumer application to react
/// </summary>
namespace libEDJournalMonitor
{
    public partial class JournalManager
    {
        private FileSystemWatcher logWatcher;
        private string JournalDirectory;

        public Commander Commander;

        public bool IsValidJournal { get; private set; } = false;

        // default for testing
        public JournalManager()
        {
            Commander = new Commander();
            JournalDirectory = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            StartLogWatcher();
            //StartStatusWatcher();
        }

        public JournalManager(string JournalDirectory)
        {
            Commander = new Commander();
            this.JournalDirectory = JournalDirectory;
            StartLogWatcher();
            //StartStatusWatcher();
        }

        private void EmitLogEvent(LogEntryType logEntryType, string eventName, DateTime dateTime)
        {
            JournalEventArgs args = new JournalEventArgs
            {
                LogEntryType = logEntryType,
                Event = eventName,
                Timestamp = dateTime
            };
            OnJournalEvent(args);
        }

        protected virtual void OnJournalEvent(JournalEventArgs e)
        {
            RaiseJournalEvent?.Invoke(this, e);
        }

        public event JournalEventHandler RaiseJournalEvent;
    }

    public class JournalEventArgs : EventArgs
    {
        public LogEntryType LogEntryType { get; internal set; }
        public string Event { get; internal set; }
        public DateTime Timestamp { get; internal set; }
    }

    public delegate void JournalEventHandler(object sender, JournalEventArgs e);
}
