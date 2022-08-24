using System.Net.Http;

namespace FrankThePOSsum;

public interface IGenerateRequests
{
    public string GenerateSoapRequest();
    public Transaction GenerateRestRequestBody();
}