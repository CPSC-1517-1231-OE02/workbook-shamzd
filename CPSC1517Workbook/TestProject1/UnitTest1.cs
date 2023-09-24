using FluentAssertions;
using Hockey.Data;

namespace Hockey.Test
{
    public class HockeyPlayerTest

    {
        //method to return a new hockey player 
        public HockeyPlayer GenerateTestPlayer()
        {
            return new HockeyPlayer();
        }


        [Fact]
        public void HockeyPlayer_FirstName_ReturnsGoodFirstName()//name the test 
        {
            //arrange
            HockeyPlayer player = GenerateTestPlayer();
            const string NAME = "test";
            player.FirstName = NAME;

            //act
            string actual = player.FirstName;

            //assert
            actual.Should().Be(NAME);
        }

        [Fact]
        public void HockeyPlayer_FirstName_ThrowsExceptionForEmptyArg()
        {
            //Arrange
            HockeyPlayer player = GenerateTestPlayer();
            const String EMPTY_NAME = "";

            Action act = () => player.FirstName = EMPTY_NAME;

            //This message has to match what is in the Argument Exception from the hockey player class. 
            act.Should().Throw<ArgumentException>().WithMessage("First Name cannot be null or empty.");
        }
    }
}