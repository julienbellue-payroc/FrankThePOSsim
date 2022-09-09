using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FrankThePOSsim;

internal static class Commands
{
    public static readonly IEnumerable<string> GetCommands = new ReadOnlyCollection<string>(new List<string>
    {
        "sale",
        "refund",
        "auth",
        "capture",
        "void",
        "tipadjust",
        "giftactivate",
        "giftrefund",
        "giftsale",
        "giftaddvalue",
        "giftbalance"
    });
}