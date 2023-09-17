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

Console.WriteLine("Welcome to the Hockey Player Test App");

//default 
HockeyPlayer player1 = new HockeyPlayer();
//Object-Initializer
HockeyPlayer player2 = new HockeyPlayer()
{
    FirstName = "Connor",
    LastName = "McDavid",
};

HockeyPlayer player3 = new HockeyPlayer("Bobby", "Orr", "Parry Sound", new DateOnly(1948, 03, 20), 
                                        196, 73, Position.Defense, Shot.Right);

//Output things about the players
Console.WriteLine($"The player's name is {player1.FirstName} {player1.LastName} and they are born on {player1.DateOfBirth}.");
Console.WriteLine($"The player's name is {player1.FirstName} {player1.LastName} and they are born on {player1.DateOfBirth}.");
Console.WriteLine($"The player's name is {player1.FirstName} {player1.LastName} and they are born on {player1.DateOfBirth}.");