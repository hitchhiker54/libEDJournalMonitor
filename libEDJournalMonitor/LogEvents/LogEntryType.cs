
namespace libEDJournalMonitor
{
    /// <summary>
    /// Additional index for log events for event consumption and intellisense for the app dev
    /// </summary>
    public enum LogEntryType
    {
        None,

        // x.json updates
        Status,
        Market,
        ModulesInfo,
        Shipyard,

        // Startup
        Cargo,              // also for cargo.json updates
        ClearSavedGame,
        Commander,
        Loadout,
        Materials,
        Missions,
        NewCommander,
        LoadGame,
        Passengers,
        Powerplay,
        Progress,
        Rank,
        Reputation,
        Statistics,

        // Travel
    }
}
