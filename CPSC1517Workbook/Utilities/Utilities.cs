//the namespace and the class cannot have the same name. name them different
namespace Utils
{
    public static class Utilities
    {
        //Not instantiated so it will be static
        public static bool IsNullEmptyOrWhiteSpace(string value)
        {
            return String.IsNullOrWhiteSpace(value);

            /*
             * Expression bodied method
             public static bool IsNullEmptyOrWhiteSpace(string value) => 
            String.IsNullOrWhiteSpace(value);
             */
        }

        public static bool IsZeroOrNegative(int value)
        {
            return value <= 0;

            /* Explicit method --> easier to read for multiple people working on the program
            
            bool result;

            if (value <= 0) 
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;

             Simple explict using ternary
            return value <= 0 ? true : false;
            */
        }

        public static bool IsPositive(int value)
        {
            return value  > 0;
            //Can use 

            /*
             * Ternary Operator -->
                public static bool IsPositive(int value) => value > 0 ? true : false;

             */
        }


        public static bool IsInTheFuture(DateOnly value)
        {
            return value > DateOnly.FromDateTime(DateTime.Now);
            /*  
                //Expression bodied method
                public static bool IsInTheFuture(DateOnly value) => value > DateOnly.FromDateTime(DateTime.Now);
         */
        }


    }




}