const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

string[] points = File.ReadAllLines(POINTS_FILE);

string[] players = File.ReadAllLines(PLAYERS_FILE);

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
for(int i = 0; i < points.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}
System.Console.WriteLine("Leader: " + players[LeaderIndex(points)] + " with " + points[LeaderIndex(points)]);

//Methods
static int LeaderIndex(string[] points)
{
    int leaderPoints = 0;
    int leaderPos = 0;
    

    // foreach(int point in points)
    // {
    //     if(point > leaderPoints)
    //     {
            
    //     }
        
    // }

    for(int i = 0; i < points.Length; i++)
    {
        int currentPoints = Convert.ToInt32(points[i]);
        // System.Console.WriteLine($"Loop {i + 1}");
        if(currentPoints > leaderPoints)
        {
            leaderPoints = currentPoints;
            leaderPos = i;
        }
    }
    return leaderPos;
}

// static int TotalPoints(string[] points)
// {
    
// }

// static string HistoryEntry(int night, string winner, int points)