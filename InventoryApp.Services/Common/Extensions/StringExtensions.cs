﻿namespace InventoryApp.Services.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                char[] charArray = inputString.ToCharArray();
                charArray[0] = char.ToUpper(charArray[0]);

                return new string(charArray);
            }
            return inputString;
        }
    }
}
