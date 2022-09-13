using System.Collections.Generic;

namespace FrankThePOSsim;

public class Environment
{
    public string? Name { get; init;  }
    public string? RestUrl { get; init; }
    public string? SoapUrl { get; init; }
    public List<Terminal>? Terminals { get; init; }
}
