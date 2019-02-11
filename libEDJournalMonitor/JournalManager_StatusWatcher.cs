using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace libEDJournalMonitor
{
    public partial class JournalManager
    {
        private List<string> statusLines = new List<string>();
        private FileSystemWatcher statusWatcher;

        private void StartStatusWatcher()
        {
            if (LoadStatus())
            {
                ParseStatus();

                statusWatcher = new FileSystemWatcher
                {
                    Path = JournalDirectory,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size,
                    Filter = "Status.json"
                };

                statusWatcher.Changed += new FileSystemEventHandler(OnStatusChanged);
                statusWatcher.Created += new FileSystemEventHandler(OnStatusChanged);

                statusWatcher.EnableRaisingEvents = true;
            }
        }

        private bool LoadStatus()
        {
            statusLines.Clear();
            bool result = false;

            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"{JournalDirectory}\\Status.json", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        statusLines.Add(line);
                    }

                    result = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Caught exception {e.Message} {Environment.NewLine} Stack Trace :{Environment.NewLine}{e.StackTrace}{Environment.NewLine} Journal file Status.json could not be read");
            }

            return result;
        }

        private void ParseStatus()
        {
            EDStatus status = new EDStatus();
            string t = statusLines.ElementAt(0);
            status = JsonConvert.DeserializeObject<EDStatus>(t);

            status.ProcessEvent(ref Commander);
        }

        private void OnStatusChanged(object source, FileSystemEventArgs e)
        {
            LoadStatus();
            var linesCount = statusLines.Count;

            if (linesCount > 0) ParseStatus();
        }
    }
}
