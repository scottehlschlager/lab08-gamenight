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
System.Console.WriteLine($"Season total: {TotalPoints(points)}");

int tonightTotal = 0;
int highestScore = 0;
string winner = " ";

for(int i = 0; i < players.Length; i++)
{
	Console.Write($"Points tonight for {players[i]}: ");
	int tonightScore = Convert.ToInt32(Console.ReadLine());
	while(tonightScore < 0 || tonightScore > MAX_POINTS)
	{
		Console.WriteLine("Points must be between 0 and 20.");
		
		Console.Write($"Points tonight for {players[i]}: ");
		tonightScore = Convert.ToInt32(Console.ReadLine());
	}

   // int currentPoints = Convert.ToInt32(points[i]);

    if(tonightScore > highestScore)
    {
        highestScore = tonightScore;
        winner = Convert.ToString(players[i]);
    }

	tonightTotal = tonightTotal + tonightScore;

    int season = Convert.ToInt32(points[i] + tonightScore);
    points[i] = season.ToString();
}

System.Console.WriteLine($"Tongiht's winner: {winner} with {highestScore}");

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

static int TotalPoints(string[] points)
{
	int totalPoints = 0;

	for(int i = 0; i < points.Length; i++)
	{
	totalPoints = totalPoints + Convert.ToInt32(points[i]);
	}
    return totalPoints;
}

// static string HistoryEntry(int night, string winner, int points)