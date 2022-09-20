using System.Collections.Generic;

namespace FrankThePOSsim;

public class Config
{
    public bool? DarkMode { get; set; }
    public List<Environment>? Environments { get; set; }

    public Config SetDefault()
    {
        DarkMode = true;
        Environments = new List<Environment>
        {
            new()
            {
                Name = "Environment 1",
                RestUrl = "rest url",
                SoapUrl = "soap url",
                Terminals = new List<Terminal>
                {
                    new()
                    {
                        ApiKey = "api key",
                        ApiPassword = "api password",
                        Id = 0,
                        SerialNumber = "Serial number"
                    }
                }

            }
        };
        return this;
    }
}
