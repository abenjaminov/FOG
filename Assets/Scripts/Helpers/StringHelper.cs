namespace Helpers
{
    public static class StringHelper
    {
        public static string MuiltiplyString(string str, int multiplier)
        {
            var result = "";
            
            for (int i = 0; i < multiplier; i++)
            {
                result += str;
            }

            return result;
        }
    }
}