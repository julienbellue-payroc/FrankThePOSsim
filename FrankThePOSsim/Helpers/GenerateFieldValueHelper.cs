using System;

namespace FrankThePOSsim.Helpers;

public static class GenerateFieldValueHelper
{
    public static string GenerateRefId()
    {
        return Guid.NewGuid().ToString();
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