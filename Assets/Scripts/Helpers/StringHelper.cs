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
        
        public static string NumberToString(int number)
        {
            if (number == 0) return "0";
            
            var result = "";
            var currentPart = "";
            
            currentPart = (number % 1000).ToString();
            
            while (number > 0)
            {
                var zerosToAdd = 3 - currentPart.Length;
                var zeros = StringHelper.MuiltiplyString("0", zerosToAdd);
                currentPart = zeros + currentPart;
                result = currentPart + result;
                result = "," + result;
                
                number /= 1000;
                currentPart = (number % 1000).ToString();
            }

            result = result.TrimStart(',', '0');
            
            return result;
        }
    }
}