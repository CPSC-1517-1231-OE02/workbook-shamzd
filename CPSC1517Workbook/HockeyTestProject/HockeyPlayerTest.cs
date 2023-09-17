using FluentAssertions;
using Hockey.Data;

namespace Hockey.Test
{
    public class HockeyPlayerTest
        
    {
        public HockeyPlayer GenerateTestPlayer()
        {
            return new HockeyPlayer();
        }


        [Fact]
        public void HockeyPlayer_FirstName_ReturnsGoodFirstName()
        {
            //Arrange
            HockeyPlayer player = GenerateTestPlayer();
            const string NAME = "test";
            player.FirstName = NAME;

            //Act
            string actual = player.FirstName;
            

            //Assert
            actual.Should().Be(NAME);
            
        }

        [Fact]
        public void HockeyPlayer_FirstName_ThrowsExceptionForEmptyArg()
        {
            HockeyPlayer player = GenerateTestPlayer();
            const string EMPTY_NAME = "";

            Action act = () => player.FirstName = EMPTY_NAME;

            act.Should().Throw<ArgumentException>().WithMessage("First Name cannot be null or empty");
        }
    }
}