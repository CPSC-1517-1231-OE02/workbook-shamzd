﻿using System.Globalization;
using Utils;

namespace Hockey.Data
{
    /// <summary>
    /// An instance of this class will hold data about hockey player.
    /// /// The characteristics (data) of the class are:
    ///     first name, last name, weight, height, date of birth, position, shot, birthplace
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
        private int _jerseyNumber;
        

        /* 
         * Since the shot and position have their own cs file with auto implemented properties and did not need any validations they do not need to be part of the data fields. 
         * They will have their own properties.
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
            //This is now a read only, where once the value is initially in the users cannot change it. It is only going to be accessable from within this class 
            private set
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
            private set
            {
                //calling the method IsNullEmptyOrWhiteSpace using the "." ext from the utilities class 
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
            private set
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
            private set
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
            private set
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

            private set
            {
                //Can't be in th future
                //if(value > DateOnly.FromDateTime(DateTime.Now)) ==> Instead used the utilities class from utilities.cs file
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

        
         //* No Need for the default constructor
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
            int weightInPounds, int heightInInches, int jerseyNumber,
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
            JerseyNumber = jerseyNumber;
            Shot = shot;
            Position = position;
        }

		public HockeyPlayer(string v1, string v2, string v3, DateOnly dateOnly, int v4, int v5, Position defense, Shot right)
		{
		}

		//This is the hockey player age that we got from the test class that was created. It is only going to need a get since there is no setting of this property.  
		public int Age => (DateOnly.FromDateTime(DateTime.Now).DayNumber - DateOfBirth.DayNumber) / 365;

        public int JerseyNumber
        {
             
            get
            {
                return _jerseyNumber;
            }
            //Set the exceptions here to make sure it will pass the test bad jersey numbers
            set
            {
                if(value < 1 || value >98)
                {
                    throw new ArgumentOutOfRangeException("Jersey number should be between 1 and 98.", new ArgumentException());//in this case there is an argument exception, but we can add the correct type argumentoutofrangeexception with the message but the underlining issue is the argumentexception
                }

                _jerseyNumber = value;
            }
        }

		//Override the ToString
		public override string ToString()
		{
			return $"{FirstName},{LastName},{JerseyNumber},{Position},{Shot},{HeightInInches},{WeightInPounds},{DateOfBirth.ToString("MMM-dd-yyyy", CultureInfo.InvariantCulture)},{BirthPlace}";
		}

       
        

        public static HockeyPlayer Parse(string line)
        {
            //Connor,Brown,28,RightWing,Right,72,184,Jan-14-1994,Toronto-ON-CAN
            //0        1    2  3         4     5   6     7          8

            //Validation
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new ArgumentNullException("Line cannot be null or empty.", new ArgumentException());
            }

            //split on commas
            string[] fields = line.Split(',');
            if(fields.Length != 9)
            {
                throw new InvalidDataException("Incorrect number of fields.");
            }

            HockeyPlayer player;

            try
            {
                player = new HockeyPlayer(fields[0], fields[1], fields[8], DateOnly.ParseExact(fields[7], "MMM-dd-yyyy"), int.Parse(fields[5]), int.Parse(fields[6]), int.Parse(fields[2]), Enum.Parse<Position>(fields[3]), Enum.Parse<Shot>(fields[4]));
            }
            catch
            {
                throw new FormatException($"Error parsing line {line}");
            }
            

            return player;
		}
        
        public static bool TryParse(string line, out HockeyPlayer? player)
        {
            bool isParsed;

            try
            {
                player = HockeyPlayer.Parse(line);
                isParsed = true;
            }
            catch
            {
                player = null;
                isParsed = false;
            }

            return isParsed;
        }

	}
}