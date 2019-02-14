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
        }


        public void Start()
        {
            JournalDirectory = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            StartLogWatcher();
            StartStatusWatcher();
        }

        public void Start(string JournalDirectory)
        {
            this.JournalDirectory = JournalDirectory;
            StartLogWatcher();
            StartStatusWatcher();
        }

        private void EmitLogEvent(IEDLog log)
        {
            JournalEventArgs args = new JournalEventArgs
            {
                Log = log,
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
        public IEDLog Log { get; internal set; }
    }

    public delegate void JournalEventHandler(object sender, JournalEventArgs e);
}
