namespace RomeVsOrcs;
public static class Constants
{
    private static int numberOfKills = 0;

    public static int NumberOfKills
    {
        get => numberOfKills;
        set
        {
            numberOfKills = value;
            Rank = GetRank();
        }
    }

    public static string Rank { get; private set; }

    private static string GetRank()
    {
        return numberOfKills switch
        {
            0 => "Novice",
            1 => "Fresh Boy",
            > 1 => "Master Slayer",
            _ => "Novice",
        };
    }
}
