using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrankThePOSsum;

public class Transaction
{
    public string? Key { get; set; }
    public string? Password { get; set; }
    public string? TerminalId { get; set; }
    public string? Command { get; set; }
    public string? RefId { get; set; }
    public string? Date { get; set; }
    public string? MerchantId { get; set; }
    public string? PaymentType { get; set; }
    public string? Prompt { get; set; }
    public string? Country { get; set; }
    public string? BusinessName { get; set; }
    public string? ContactName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? ResellerName { get; set; }
    public string? ReferenceId { get; set; }
    public string? TerminalSerialNumber { get; set; }
    public string? Amount { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? Token { get; set; }
    public string? ExpDate { get; set; }
    public string? Type { get; set; }
    public string? Url { get; set; }
    public string? IsDefault { get; set; }
    public string? OptionName { get; set; }
    public string? OptionValue { get; set; }
    public string? Title { get; set; }
    public string? MaxLength { get; set; }
    public string? Options { get; set; }
    
    
    public string ToQueryString()
    {
        var result = new List<string>();
        var props = this.GetType().GetProperties().Where(p => p.GetValue(this, null) != null);
        foreach (var p in props)
        {
            var value = p.GetValue(this, null);
            var enumerable = value as ICollection;
            if (enumerable != null)
            {
                result.AddRange(from object v in enumerable select $"{p.Name}={HttpUtility.UrlEncode(v.ToString())}");
            }
            else
            {
                result.Add(string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(value?.ToString())));
            }
        }

        return string.Join("&", result.ToArray());
    }
}