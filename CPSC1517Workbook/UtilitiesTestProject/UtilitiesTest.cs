using FluentAssertions;
using Utils;

namespace UtilitiesTestProject
{
    public class UtilitiesTest // rename the class
    {
        
        [Theory]
        //Inline Data to pass in multiple values False for zero or negative and true for positive
        [InlineData(1)] //Test for should be true
        [InlineData(1D)] // test for double values 
        [InlineData("1.0")] //Test for decimal values
        public void Utilities_IsPositive_ReturnsTrueForPositive(object value) //Cant put an int or double but if we put object it will take in any type (int, double, convert string to decimal value)
        {
            bool actual;
            if (value.GetType() == typeof(int))
            {
                actual = Utilities.IsPositive((int)value);
            }
            else if (value.GetType() == typeof(double))
            {
                actual = Utilities.IsPositive((double)value); //we have to ensure in the Utilities class the methods is overloaded with tese types or else we get an error msg
            }
            else
            {
                //In this case the string in the last inline will be converted from string to a decimal
                actual = Utilities.IsPositive(Convert.ToDecimal(value));  
            }
            
            actual.Should().BeTrue();// all of them are positive values so this is expected as true.
        }
        
        

        //DateOnly data generator
        public static IEnumerable<object[]> GenerateIsInTheFutureTestData()
        {
            //can yield deveral data here 
            //Present
            yield return new object[] 
            {
                DateOnly.FromDateTime(DateTime.Now), false 
            };
            //past
            yield return new object[]
            {
                DateOnly.FromDateTime(DateTime.Now).AddDays(-1), false//for a date in the past put the -1
            };
            //future
            yield return new object[]
            {
                DateOnly.FromDateTime(DateTime.Now).AddDays(+1), true // for a date in the futureput the +1
            };
        }
        [Theory]
        /*when dealing with data that is not compatible with INLINE data we have to move to the MemberData Attribute.
        [InlineData(DateOnly.FromDateTime(DateTime.Now), false)]//Present*/
        //If we can yield a value from the irrerator the test can pull those values from it.
        [MemberData(nameof(GenerateIsInTheFutureTestData))]//Memeber that is going to generate the data
        
        public void Utilities_IsInTheFuture_ReturnTrueForFutureFalseOtherwise(DateOnly date, bool expected)//date only date is what we are checking and the bool is what we are saying we expect.
        {
            //Arrange
            bool actual;

            //Act
            //this is what we get from the utilities class where we created this as a method to check on valid dates.
            actual = Utilities.IsInTheFuture(date);

            //Assert
            //so here we would add the expected which we asked for in the method when we created it.
            actual.Should().Be(expected);
        }

        
        [Theory]
        //this is where the const from the [fact] is now added here, We dont have to call the method multiple time sin the test, The test runner does it for us.
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Utilities_IsNullEmptyOrWhiteSpace_ReturnsTrueForEmptyNullOrWhiteSpace(string value)
        {
            //Arrange
            bool actual;

            //Act
            actual = Utilities.IsNullEmptyOrWhiteSpace(value);

            //Assert
            actual.Should().BeTrue();
        }   

        [Fact]
        public void Utilities_IsNullEmptyOrWhiteSpace_ReturnsFalseForNonEmpty() //name the test.
        {
            //Arrange
            bool actual; //checking the actual value (the result)
            const string GOOD_STRING = "x"; //need a string so set up a constant, call the string whatever that is a good value or non-empty

            //Act
            //cal on the utilities, to make sure we get a good value back 
            actual = Utilities.IsNullEmptyOrWhiteSpace(GOOD_STRING);

            //Assert
            actual.Should().BeFalse();

        }
    }
}