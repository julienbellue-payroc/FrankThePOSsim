using System.Collections.Generic;

namespace FrankThePOSsim;

public class Environment
{
    public string? Name { get; set;  }
    public string? RestUrl { get; set; }
    public string? SoapUrl { get; set; }
    public List<Terminal>? Terminals { get; set; }
}
