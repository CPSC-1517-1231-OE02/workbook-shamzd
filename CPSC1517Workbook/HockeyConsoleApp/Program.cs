/*namespace HockeyConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}*/

using Hockey.Data;
//into to the testing screen
Console.WriteLine("**********Welcome to the HockeyPlayer Test App**********");

//default 
HockeyPlayer player1 = new HockeyPlayer();
player1.FirstName = "Stewart";
player1.LastName = "Skinner";

//Object-Initializer ==> one of the option to initilize properties (This is the same as the default one upto)
HockeyPlayer player2 = new HockeyPlayer()
{
    FirstName = "Connor",
    LastName = "McDavid",
};

//Greedy Constructor with the values inputted
HockeyPlayer player3 = new HockeyPlayer("Bobby", "Orr", "Parry Sound", new DateOnly(1948, 03, 20), 196, 73, Position.Defense, Shot.Right);

//Output things about the players
Console.WriteLine($"The player's name is {player1.FirstName} {player1.LastName} and they are born on {player1.DateOfBirth}.");
Console.WriteLine($"The player's name is {player2.FirstName} {player2.LastName} and they are born on {player2.DateOfBirth}.");
Console.WriteLine($"The player's name is {player3.FirstName} {player3.LastName} and they are born on {player3.DateOfBirth}.");