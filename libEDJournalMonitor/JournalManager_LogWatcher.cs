﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace libEDJournalMonitor
{
    public partial class JournalManager
    {
        private string currentLogName;
        private int currentLogLine;
        private int lastLogLine;
        private List<string> logLines = new List<string>();

        private void StartLogWatcher()
        {
            if (Directory.Exists(JournalDirectory))
            {
                currentLogName = GetLatestLogName();
                LoadLog();
                currentLogLine = 0;
                lastLogLine = logLines.Count;

                ParseJournal(false);

                logWatcher = new FileSystemWatcher
                {
                    Path = JournalDirectory,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size,
                    Filter = "*.log"
                };

                logWatcher.Changed += new FileSystemEventHandler(OnLogChanged);
                logWatcher.Created += new FileSystemEventHandler(OnLogChanged);
                logWatcher.Deleted += new FileSystemEventHandler(OnLogChanged);

                logWatcher.EnableRaisingEvents = true;

                IsValidJournal = true;
            }
        }

        private void StopLogWatcher()
        {
            if(IsValidJournal)
            {
                logWatcher.Deleted -= new FileSystemEventHandler(OnLogChanged);
                logWatcher.Created -= new FileSystemEventHandler(OnLogChanged);
                logWatcher.Changed -= new FileSystemEventHandler(OnLogChanged);
            }
        }

        private void ParseJournal(bool newEntry)
        {
            EDLogEntry logEntry = new EDLogEntry();

            for (int i = currentLogLine; i < lastLogLine; i++)
            {
                string entry = logLines.ElementAt(i);
                logEntry = JsonConvert.DeserializeObject<EDLogEntry>(entry);
                logEntry.RawJson = entry;

                switch (logEntry.Event)
                {
                    case "Cargo":
                        {
                            EDLogCargo edLogCargo = new EDLogCargo();
                            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

                            edLogCargo.ProcessEvent(ref Commander);
                        }
                        break;
                    case "ClearSavedGame":
                        {
                            EDLogClearSavedGame edLogClearSavedGame = new EDLogClearSavedGame();
                            edLogClearSavedGame = JsonConvert.DeserializeObject<EDLogClearSavedGame>(entry);

                            edLogClearSavedGame.ProcessEvent(ref Commander);
                        }
                        break;
                    case "Commander":
                        {
                            EDLogCommander edLogCommander = new EDLogCommander();
                            edLogCommander = JsonConvert.DeserializeObject<EDLogCommander>(entry);

                            edLogCommander.ProcessEvent(ref Commander);
                        }
                        break;
                    case "Loadout":
                        {
                            EDLogLoadout edLogLoadout = new EDLogLoadout();
                            edLogLoadout = JsonConvert.DeserializeObject<EDLogLoadout>(entry);

                            edLogLoadout.ProcessEvent(ref Commander);
                        }
                        break;
                    case "Materials":
                        {
                            EDLogMaterials edLogMaterials = new EDLogMaterials();
                            edLogMaterials = JsonConvert.DeserializeObject<EDLogMaterials>(entry);

                            edLogMaterials.ProcessEvent(ref Commander);
                        }
                        break;
                    case "Missions":
                        {
                            EDLogMissions edLogMissions = new EDLogMissions();
                            edLogMissions = JsonConvert.DeserializeObject<EDLogMissions>(entry);

                            edLogMissions.ProcessEvent(ref Commander);
                        }
                        break;
                }

                // don't emit events for old logs, just keep the data
                if (newEntry) EmitLogEvent(logEntry);
            }
        }

        private void OnLogChanged(object source, FileSystemEventArgs e)
        {
            currentLogName = e.FullPath;

            LoadLog();

            var linesCount = logLines.Count();

            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                currentLogLine = 0;
                lastLogLine = linesCount;
            }

            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (linesCount > lastLogLine)
                {
                    currentLogLine = lastLogLine;
                    lastLogLine = linesCount;
                }
            }

            ParseJournal(true);
        }

        private bool LoadLog()
        {
            logLines.Clear();
            bool result = false;

            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(currentLogName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        logLines.Add(line);
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

        private string GetLatestLogName()
        {
            var sortedFiles = new DirectoryInfo(JournalDirectory).GetFiles()
                                                  .OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            string latest = "";

            foreach (FileInfo fileInfo in sortedFiles)
            {
                if (fileInfo.Extension == ".log")
                {
                    latest = fileInfo.FullName;
                    break;
                }
            }

            return latest;
        }
    }
}
