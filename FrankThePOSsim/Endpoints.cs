using System.Collections.Generic;

namespace FrankThePOSsim;

internal class Endpoints
{
    public List<Endpoint> All { get; } = new()
    {
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
        {
            Uri = "getSignature",
            RequiredFields = new List<RequestFields>()
            {
                RequestFields.Key,
                RequestFields.Password,
                RequestFields.TerminalId
            }
        },
        new Endpoint
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
        new Endpoint
        {
            Uri = "getTerminalConfiguration",
            RequiredFields = new List<RequestFields>()
            {
                RequestFields.Key,
                RequestFields.Password,
                RequestFields.TerminalId
            }
        },
        new Endpoint
        {
            Uri = "getTerminalFeatures",
            RequiredFields = new List<RequestFields>()
            {
                RequestFields.Key,
                RequestFields.Password,
                RequestFields.TerminalId
            }
        },
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
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
        new Endpoint
        {
            Uri = "showLineItems",
            RequiredFields = new List<RequestFields>()
            {
                RequestFields.Key,
                RequestFields.Password,
                RequestFields.TerminalId,
                RequestFields.Data,
                RequestFields.Type
            }
        },
        new Endpoint
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
        new Endpoint
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