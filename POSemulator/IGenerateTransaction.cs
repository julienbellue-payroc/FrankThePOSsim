using System;

namespace FrankThePOSsum;

public interface IGenerateTransaction
{
    public Transaction GenerateTransaction();
    public string? GetUri();
}
