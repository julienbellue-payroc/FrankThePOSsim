using System.Collections.Generic;

namespace FrankThePOSsim.Models;

internal class Endpoint
{
    public string? Uri { get; init; }

    public List<RequestFields>? RequiredFields { get; init; }
}