/*
* Name: Scott Ehlschlager
* Course: CSCI 1250, Section 002
* Assignment: Lab 08, Game Night
* Date: November 21, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/

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

string[] nights = File.ReadAllLines(HISTORY_FILE);

System.Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");

int tonightNumber = 0;

if(nights.Length == 0)
{
    System.Console.WriteLine("No nights played yet.");
    System.Console.WriteLine("Tonight is Night 1.");

    tonightNumber = 1;
}
else
{
    System.Console.WriteLine($"Nights played: {nights.Length}");
    tonightNumber = nights.Length + 1;

    //We want to grab every single previous night and display the night number, who won, and with how many points.
    //Since we simply want to go through every night, a for loop with a controllable incrementer is not necessary.
    foreach(string night in nights)
    {
        System.Console.WriteLine(night.PadLeft(night.Length + 2));
    }
    System.Console.WriteLine($"Tonight is Night {tonightNumber}");
}

System.Console.WriteLine();

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
//We are taking values for both name and points which exist in different arrays, so we need to keep them together
//with a shared index.
for(int i = 0; i < points.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}
System.Console.WriteLine("Leader: " + players[LeaderIndex(points)] + " with " + points[LeaderIndex(points)]);
System.Console.WriteLine($"Season total: {TotalPoints(points)}");

int highestScore = 0;
string winner = " ";

System.Console.WriteLine();

//We once again are pulling values from both arrays, making a for loop ideal to keep them together with a shared index.
for(int i = 0; i < players.Length; i++)
{
	Console.Write($"Points tonight for {players[i]}: ");
	int tonightScore = Convert.ToInt32(Console.ReadLine());
    //We need to check whether the score given fits between 0 and 20 before executing the loop. If it is within 0-20 the
    //loop does not need to run and will continue, if not it will continue to give the question until a valid input is given.
	while(tonightScore < 0 || tonightScore > MAX_POINTS)
	{
		Console.WriteLine("Points must be between 0 and 20.");
		
		Console.Write($"Points tonight for {players[i]}: ");
		tonightScore = Convert.ToInt32(Console.ReadLine());
	}

    if(tonightScore > highestScore)
    {
        highestScore = tonightScore;
        winner = Convert.ToString(players[i]);
    }

    int season = Convert.ToInt32(points[i]) + tonightScore;
    points[i] = season.ToString();
}

System.Console.WriteLine($"Tonight's winner: {winner} with {highestScore}");

System.Console.WriteLine();

File.WriteAllLines(POINTS_FILE, points);
System.Console.WriteLine($"Saved {POINTS_FILE}.");

string[] entry = {HistoryEntry(tonightNumber, winner, highestScore)};
File.AppendAllLines(HISTORY_FILE, entry);

System.Console.WriteLine($"Added to {HISTORY_FILE}: {HistoryEntry(tonightNumber, winner, highestScore)}");

//Methods

//Returns both the amount of points the leading player has, as well as the index of the leading player in the array.
static int LeaderIndex(string[] points)
{
    int leaderPoints = 0;
    int leaderPos = 0;

    //I used a for loop because it makes it easier to track the index of the leading player.
    for(int i = 0; i < points.Length; i++)
    {
        int currentPoints = Convert.ToInt32(points[i]);
        if(currentPoints > leaderPoints)
        {
            leaderPoints = currentPoints;
            leaderPos = i;
        }
    }
    return leaderPos;
}

//This adds up the points given to it and returns a total.
static int TotalPoints(string[] points)
{
	int totalPoints = 0;

    //I chose a for loop because I am more comfortable using it to iterate through an array. However, you could use
    //either a for or foreach loop here.
	for(int i = 0; i < points.Length; i++)
	{
	totalPoints = totalPoints + Convert.ToInt32(points[i]);
	}
    return totalPoints;
}

//Builds a string displaying each night and its winner and their points, returning the built string to then be saved
//to the history file.
static string HistoryEntry(int night, string winner, int points)
{
    string history = $"Night {night}: {winner} won with {points}";
    return history;
}