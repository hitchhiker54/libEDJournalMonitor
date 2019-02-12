using System;
using libEDJournalMonitor;

namespace TestCLI_core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("libEDJournalMonitor CLI Test");

            JournalManager journalManager = new JournalManager();
            journalManager.RaiseJournalEvent += new JournalEventHandler(LogEvent);

            journalManager.Start();

            Console.ReadLine();

            journalManager.RaiseJournalEvent -= LogEvent;
        }

        static void LogEvent(object sender, JournalEventArgs e)
        {
            Console.WriteLine($"The event {e.Event} was read at {e.Timestamp}.");
        }
    }
}
