using System.Collections.Generic;

namespace FrankThePOSsim
{
    internal class Endpoints
    {
        public List<Endpoint> All { get; }
        public Endpoints()
        {
            All = new List<Endpoint>
            {
                new()
                {
                    Uri = "checkStatus",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Command,
                        RequestFields.RefId,
                        RequestFields.Date
                    }
                },
                new()
                {
                    Uri = "checkStatus2",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Command,
                        RequestFields.RefId,
                        RequestFields.Date,
                        RequestFields.MerchantId
                    }
                },
                new()
                {
                    Uri = "checkStatusList",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Command,
                        RequestFields.RefId,
                        RequestFields.Date
                    }
                },
                new()
                { 
                    Uri = "closeBatch", 
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.RefId,
                        RequestFields.MerchantId
                    }
                },
                new()
                {
                    Uri = "getSignature",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId
                    }
                },
                new()
                {
                    Uri = "getSwipe",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Prompt
                    }
                },
                new()
                {
                    Uri = "getUserSelection",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Title,
                        RequestFields.Options
                    }
                },
                new()
                {
                    Uri = "getUserInput",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Title,
                        RequestFields.Type,
                        RequestFields.MaxLength
                    }
                },
                new()
                {
                    Uri = "pairTerminal",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.BusinessName,
                        RequestFields.ContactName,
                        RequestFields.Address,
                        RequestFields.City,
                        RequestFields.State,
                        RequestFields.ZipCode,
                        RequestFields.Email,
                        RequestFields.Phone,
                        RequestFields.ResellerName,
                        RequestFields.ReferenceId,
                        RequestFields.TerminalSerialNumber
                    }
                },
                new()
                {
                    Uri = "pairTerminalWithCountry",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.BusinessName,
                        RequestFields.ContactName,
                        RequestFields.Address,
                        RequestFields.City,
                        RequestFields.State,
                        RequestFields.ZipCode,
                        RequestFields.Country,
                        RequestFields.Email,
                        RequestFields.Phone,
                        RequestFields.ResellerName,
                        RequestFields.ReferenceId,
                        RequestFields.TerminalSerialNumber
                    }
                },
                new()
                {
                    Uri = "runTransaction",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Command,
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.Amount,
                        RequestFields.TerminalId,
                        RequestFields.RefId
                    }
                },
                new()
                {
                    Uri = "runTransaction2",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Command,
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.Amount,
                        RequestFields.TerminalId,
                        RequestFields.RefId,
                        RequestFields.InvoiceNumber,
                        RequestFields.MerchantId,
                        RequestFields.Token,
                        RequestFields.ExpDate
                    }
                },
                new()
                {
                    Uri = "showUrl",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.Url,
                        RequestFields.IsDefault
                    }
                },
                new()
                {
                    Uri = "getTerminalFeatures",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId
                    }
                },
                new()
                {
                    Uri = "setTerminalConfiguration",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId,
                        RequestFields.OptionName,
                        RequestFields.OptionValue
                    }
                },
                new()
                {
                    Uri = "getTerminalConfiguration",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId
                    }
                },
                new()
                {
                    Uri = "unPairTerminal",
                    RequiredFields = new List<RequestFields>()
                    {
                        RequestFields.Key,
                        RequestFields.Password,
                        RequestFields.TerminalId
                    }
                }
            };
        }
    }
}
