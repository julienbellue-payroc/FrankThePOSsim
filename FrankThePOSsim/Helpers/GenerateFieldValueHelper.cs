using System;

namespace FrankThePOSsim.Helpers;

public static class GenerateFieldValueHelper
{
    public static string GenerateRefId()
    {
        const int maxRefIdLength = 32;
        var guid = Guid.NewGuid().ToString();
        var chars = guid.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (i % 2 == 0)
            {
                chars[i] = char.ToUpper(chars[i]);
            }
            else
            {
                chars[i] = char.ToLower(chars[i]);
            }
        }
        return new string(chars)[..maxRefIdLength];
    }
    public static string GenerateDate()
    {
        return DateTime.Now.ToString("MMddyyyy");
    }
    public static string GenerateAmount()
    {
        const int upperLimit = 2000;
        const int lowerLimit = 1;
        var randomNumber = new Random().Next(lowerLimit, upperLimit);
        var amount = (decimal)randomNumber / 100;
        return amount.ToString("F2");
    }
}