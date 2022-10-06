using System;

namespace FrankThePOSsim.Helpers;

public static class GenerateFieldValueHelper
{
    public static string GenerateRefId()
    {
        const int maxRefIdLength = 32;
        return Guid.NewGuid().ToString()[..maxRefIdLength];
    }
    public static string GenerateDate()
    {
        return DateTime.Now.ToString("MMddyyyy");
    }
    public static string GenerateAmount()
    {
        return "1";
    }
}