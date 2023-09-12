namespace Hockey.Data
{
    /// <summary>
    /// An instance of this class will hold data about hockey player.
    /// </summary>
    public class HockeyPlayer
    {
        //Data fields
        private string _birthPlace;
        private string _firstName;
        private string _lastName;
        private int _heightInInches;
        private int _weightsInPounds;
        private DateOnly _dateOfBirth;
        /* 
        private Position _position; //LeftWing, RightWing, Center, Defense, Goalie/ Created Enum in another file
        private Shot _shot; //created emun in another file
        */

        //Properties
        public string BrithPlace
        {
            get
            {
                return _birthPlace;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Birth place cannot be null or empty.");
                }

                _birthPlace = value;
            }
        }
        public int HeightInInches
        {
            get
            {
                return _heightInInches;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height must be positive.");
                }

                _heightInInches = value;
            }
        }

        public DateOnly DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }

            set
            {
                _dateOfBirth = value;
            }
        }

        public Position Position { get; set; }

        public Shot Shot { get; set; }

        //Default Constructor
        public HockeyPlayer()
        {
            _firstName = string.Empty;
            _lastName = string.Empty;
            _birthPlace= string.Empty;
            _dateOfBirth = new DateOnly();
            _weightsInPounds = 0;
            _heightInInches = 0;
            Position = Position.Center;
            Shot = Shot.Left;
        }

        //Greedy Constructor
        public HockeyPlayer(string firtName, string lastName, String birthPlace, DateOnly birthDate,
            int weightInPOunds, int heightInInches, 
            Position position = Position.Center, Shot shot = Shot.Left)
        {
            //ToDo: implement and use the remaining properties
            birthPlace = birthPlace;
            HeightInInches = heightInInches;
            DateOfBirth = birthDate;
            Shot = shot;
            Position = position;
        }

        //
    }
}