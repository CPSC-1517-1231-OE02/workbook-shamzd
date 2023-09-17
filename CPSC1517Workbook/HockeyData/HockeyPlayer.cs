using Utils;

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
        private int _weightInPounds;
        private DateOnly _dateOfBirth; //type reperensents a specific structure for a date variable.

        /* 
        private Position _position; //LeftWing, RightWing, Center, Defense, Goalie/ Created Enum in another file
        private Shot _shot; //created emun in another file
        */

        //Properties
        //A member that provides a flexible mechanism to read, write or computer the value of a private field.
        public string BirthPlace
        {
            get
            {
                return _birthPlace;
            }
            set
            {
                
                if (Utilities.IsNullEmptyOrWhiteSpace(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("Birth place cannot be null or empty.");
                }

                _birthPlace = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (Utilities.IsNullEmptyOrWhiteSpace(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("First Name cannot be null or empty.");
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (Utilities.IsNullEmptyOrWhiteSpace(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("Last Name cannot be null or empty.");
                }

                _lastName = value;
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
                if (Utilities.IsZeroOrNegative(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("Height must be positive.");
                }

                _heightInInches = value;
            }
        }

        public int WeightInPounds
        {
            get
            {
                return _weightInPounds;
            }
            set
            {
                if (!Utilities.IsPositive(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("Weight must be positive.");
                }

                _weightInPounds = value;
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
                //Can't be in th future
                if(Utilities.IsInTheFuture(value))//doing the check to make sure we dont get bad values
                {
                    throw new ArgumentException("Date of birth cannot be in the future.");
                }
                
                _dateOfBirth = value;
            }
        }

        //Shot and position dont need any logic for set or get as it is already been set when enum was created.
        public Position Position { get; set; }

        public Shot Shot { get; set; }

        //Default Constructor
        public HockeyPlayer()
        {
            _firstName = string.Empty;
            _lastName = string.Empty;
            _birthPlace= string.Empty;
            _dateOfBirth = new DateOnly();
            _weightInPounds = 0;
            _heightInInches = 0;
            Position = Position.Center; //this is the default position from the enum 
            Shot = Shot.Left; //default position from the enum
        }

        //Greedy Constructor
        public HockeyPlayer(string firstName, string lastName, string birthPlace, DateOnly dateOfBirth,
            int weightInPounds, int heightInInches, 
            Position position = Position.Center,
            Shot shot = Shot.Left)
        {
            //values set using the properties since the validation is in it.
            FirstName = firstName;
            LastName = lastName;
            BirthPlace = birthPlace;
            HeightInInches = heightInInches;
            WeightInPounds = weightInPounds;
            DateOfBirth = dateOfBirth;
            Shot = shot;
            Position = position;
        } 
    }
}