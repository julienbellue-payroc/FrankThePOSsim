using System.Collections.Generic;

namespace FrankThePOSsim;

internal class Endpoint
{
    public string? Uri { get; init; }

    public List<RequestFields>? RequiredFields { get; init; }
}