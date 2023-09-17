namespace Utils
{
    public static class Utilities
    {
        public static bool IsNullEmptyOrWhiteSpace(string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsZeroOrNegative(int value)
        {
            return value <= 0;
        }

        public static bool IsPositive(int value)
        {
            return value  > 0;
        }

        public static bool IsInTheFuture(DateOnly value)
        {
            return value > DateOnly.FromDateTime(DateTime.Now);
        }
    }

    
}