# libEDJournalMonitor
Lightweight .NET Standard log &amp; json files monitor library for Elite Dangerous 3.3

Example startup :

            JournalManager journalManager = new JournalManager();
            journalManager.RaiseJournalEvent += new JournalEventHandler(LogEvent);

            // start with default Windows journal location. Start(<journalpath>) in other cases
            journalManager.Start();

            // do stuff

            journalManager.RaiseJournalEvent -= LogEvent;

Individual log events can be read and accessed through the above event handler :

            Console.WriteLine($"The event {e.Log.GetEventName()} dated {e.Log.GetDateTime()}.");

            if (e.Log.GetEventName() == "Commander")
            {
                Console.WriteLine($"{e.Log.GetRawJson()}");
            }
