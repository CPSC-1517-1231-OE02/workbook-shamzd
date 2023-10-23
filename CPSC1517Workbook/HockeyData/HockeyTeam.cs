namespace Hockey.Data
{
    public class HockeyTeam
    {
        //In this example the class has been given a rule to follow ie Goalies 2-3, players 20-23, so we use the const var and set them.
        private const int MaxGoalies = 3;
        private const int MaxPlayers = 23;
        private const int MinGoalies = 2;
        private const int MinPlayers = 20;

        // Properties for the hockey team 

        //Property for the list in hockey team as the player will need to be added in.
        //To create a list as per the diagram
        //make it private as per the instructions
        //The list should go on first as it will need to be created first to put the rest of the information in
        public List<HockeyPlayer> Players { get; private set; } = new List<HockeyPlayer>();

        //property for city auto implemented
        public string City {  get; private set; }

        //Property for Name auto implemented
        public string Name { get; private set; }

        //Property for IsValidRoster is readonly (get)
        //Here we do a check to see if the rule of players and goalies is per the requirements
        public bool IsValidRoster
        {
            get
            {
                // Using built-in FindAll with Predicate
                //int numOfGoalies = Players.FindAll(
                //    (HockeyPlayer player) => player.Position == Position.Goalie
                //).Count;

                // Using LINQ
                int numOfGoalies = Players.Where(p => p.Position == Position.Goalie).Count();

                return TotalPlayers >= MinPlayers && TotalPlayers <= MaxPlayers && numOfGoalies >= MinGoalies && numOfGoalies <= MaxGoalies;
            }
        }

        //Property for Total Players is a readonly (get) 
        //In this property the count is added for the number of players from the list created up top
        public int TotalPlayers => Players.Count;


        /// <summary>
        /// Creates a new hockey team
        /// </summary>
        /// <param name="city">Home city for the team</param>
        /// <param name="name">Team name</param>
        /// <exception cref="ArgumentException">Throws if eitehr the city or name are empty</exception>
        public HockeyTeam(string city, string name)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            City = city;
            Name = name;
        }


        
        //Adds a hockey player to the roster.
        
        // <param name="player">The HockeyPlayer to add</param>
        public void AddPlayer(HockeyPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("Player cannot be null.");
            }

            Players.Add(player);
        }

      
        // Removes a player from the roster.
      
        // <param name="player">The HockeyPlayer to remove</param>
        // <exception cref="InvalidOperationException">If the player is not on the team</exception>
        public void RemovePlayer(HockeyPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("Player cannot be null.");
            }

            if (!Players.Contains(player))
            {
                throw new InvalidOperationException($"Player {player} is not on the team.");
            }

            Players.Remove(player);
        }
    }
}
