using System;
using System.Linq;

namespace FrankThePOSsim.Helpers;

public static class GenerateFieldValueHelper
{
    public static string GenerateRefId()
    {
        const int maxRefIdLength = 32;
        var chars = AlternateCase(Guid.NewGuid().ToString());
        return chars[..Math.Min(chars.Length, maxRefIdLength)];
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

    public static string ChangeRefIdCase(string text)
    {
        if (text.All(c => char.IsUpper(c) || char.IsDigit(c) || c == '-'))
        {
            return text.ToLower();
        }
        if (text.All(c => char.IsLower(c) || char.IsDigit(c) || c == '-'))
        {
            return AlternateCase(text);
        }
        return text.ToUpper();
    }

    private static string AlternateCase(string input)
    {
        var chars = input.ToCharArray();
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
        return new string(chars);
    }
}