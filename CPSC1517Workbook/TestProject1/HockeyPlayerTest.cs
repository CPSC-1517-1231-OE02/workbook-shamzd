using FluentAssertions;
using Hockey.Data;
using System.Collections;
using System.Globalization;
using Xunit.Sdk;

namespace Hockey.Test
{
    public class HockeyPlayerTest

    {
        // Constants for a test player
        const string FirstName = "Connor"; 
        const string LastName = "Brown";
        const string BirthPlace = "Toronto-ON-CAN";
        const int HeightInInches = 72;
        const int WeightInPounds = 188;
        const int JerseyNumber = 28;
        const Position PlayerPosition = Position.Center;
        const Shot PlayerShot = Shot.Left;
        static readonly DateOnly DateOfBirth = new DateOnly(1994, 01, 14);
        string ToStringValue = $"{FirstName},{LastName},{JerseyNumber},{PlayerPosition},{PlayerShot},{HeightInInches},{WeightInPounds},{DateOfBirth.ToString("MMM-dd-yyyy", CultureInfo.InvariantCulture)},{BirthPlace}";
        readonly int Age = (DateOnly.FromDateTime(DateTime.Now).DayNumber - DateOfBirth.DayNumber) / 365;

        //[Fact]
        //public void Age_IsCorrect()
        //{
        //  Age.Should().Be(29);
        //}

        public HockeyPlayer CreateTestHockeyPlayer()
        {
            return new HockeyPlayer(FirstName, LastName, BirthPlace, DateOfBirth, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot);
        }

        //Create a test for the constructors, We cannot test it by passing values, the reason being the Date.Only params. Date.Only values cannot be passed in by InlineData
        // Test data generateor for class data (see line 85 below)
        private class BadHockeyPlayerTestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                // First Name tests
                new object[]{"", LastName, BirthPlace, DateOfBirth, HeightInInches, WeightInPounds, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty." },
                new object[]{" ", LastName, BirthPlace, DateOfBirth, HeightInInches, WeightInPounds, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty." },
                new object[]{null, LastName, BirthPlace, DateOfBirth, HeightInInches, WeightInPounds, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty." },

                // TODO: complete remaining private set tests
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        //This Test is to make sure given good values the constructor will generate the hockey player 
        // Alternative test data generator for member data (see line 97 below)
        public static IEnumerable<object[]> GoodHockeyPlayerTestDataGenerator()
        {
            // Yield as many test objects as desired/required
            yield return new object[]
            {
               FirstName, LastName, BirthPlace, DateOfBirth, HeightInInches, WeightInPounds, JerseyNumber, PlayerPosition, PlayerShot,
            };
        }

        [Theory]
        [MemberData(nameof(GoodHockeyPlayerTestDataGenerator))]// this will reflect what ever the data generator for memeber data is called. 
        public void HockeyPlayer_GreedyConstructor_ReturnHockeyPlayer(string firstName, string lastName, string birthPlace,
            DateOnly dateOfBirth, int heighInInces, int weightInPounds, int jerseryNumber, Position position, Shot shot)//The params here must reflect the yeild return in the new object array on line 57
        {
            HockeyPlayer actual;

            actual = new HockeyPlayer(firstName, lastName, birthPlace, dateOfBirth, heighInInces, weightInPounds,
                jerseryNumber, position, shot);//these need to reflect the constructor in the HockeyPlayer class 
            //Testing to check if the constructor will create an hockey player instance
            actual.Should().NotBeNull();
        }

        //Bad values for the jersey numbers will be set and tested for with this Theory.
        [Theory]
        [InlineData(0)]//if the number is set to zero will check for any value 0 or less 
        [InlineData(99)]//if set for 99 will check for values 99 or more
        public void HockeyPlayer_JerseyNumber_BadSetThrows(int value)
        {
            HockeyPlayer player = CreateTestHockeyPlayer();

            Action act = () => player.JerseyNumber = value;

            //This will not be but instead throw an exception
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Jersey number should be between 1 and 98.");
        }

        //Theory test with Inline Params that get passed in the method 
        //This test is for getting and setting a jersey number between the values of 1&98 >1 & <98 are bad values
        [Theory]
        [InlineData(1)]
        [InlineData(98)]
        public void HockeyPlayer_JerseyNumber_GoodSetAndGet(int value )
        {
            HockeyPlayer player = CreateTestHockeyPlayer();
            int actual;

            player.JerseyNumber = value;

            actual = player.JerseyNumber;

            actual.Should().Be( value );
        }

        [Fact]
        public void HockeyPlayer_Age_ReturnsCorrectValue()
        {
            HockeyPlayer player = CreateTestHockeyPlayer();
            int actual;

            actual = player.Age;

            actual.Should().Be(Age);
        }

        [Fact]
        public void HockeyPlayer_ToString_ReturnsCorrectValue()
        {
            HockeyPlayer player = CreateTestHockeyPlayer();
            string actual;

            actual = player.ToString();

            actual.Should().Be(ToStringValue);
        }


        [Fact]
        public void HockeyPlayer_Parse_ParsesCorretly()
        {
            HockeyPlayer actual;
            string line = $"{FirstName},{LastName},{JerseyNumber},{PlayerPosition},{PlayerShot},{HeightInInches},{WeightInPounds},Jan-04-1994,{BirthPlace}";

            actual = HockeyPlayer.Parse(line);

            actual.Should().BeOfType<HockeyPlayer>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void HockeyPlayer_Parse_ThrowsForNullEmptyOrWhiteSpace(string line)
        {
            Action act = () => HockeyPlayer.Parse(line);

            act.Should().Throw<ArgumentNullException>().WithMessage("Line cannot be null or empty.");
        }

        [Fact]
        public void HockeyPlayer_Parse_ThrowsForInvalidNumberOfFields()
        {
            string line = "one";

            Action act = () => HockeyPlayer.Parse(line);

            act.Should().Throw<InvalidDataException>().WithMessage("Incorrect number of fields.");
        }

        [Fact]
        public void HockeyPlayer_Parse_ThrowsForFormatError()
        {
            string line = "one,two,three,four,five,six,seven,eight,nine";

            Action act = () => HockeyPlayer.Parse(line);

            act.Should().Throw<FormatException>().WithMessage("*Error parsing line*");
        }

        [Fact]
        public void HockeyPlayer_TryParse_ParsesSuccessfully()
        {
            HockeyPlayer? actual = null;
            bool result;

            result = HockeyPlayer.TryParse(ToStringValue, out actual);

            result.Should().BeTrue();
            actual.Should().NotBeNull();
        }

		//ToDo: create a false test
		[Fact]
		public void HockeyPlayer_TryParse_ParsesUnsuccessfully()
		{
			HockeyPlayer? actual = null;
			bool result;

			result = HockeyPlayer.TryParse(ToStringValue, out actual);

			result.Should().BeFalse();
			actual.Should().BeNull();
		}

	}
}