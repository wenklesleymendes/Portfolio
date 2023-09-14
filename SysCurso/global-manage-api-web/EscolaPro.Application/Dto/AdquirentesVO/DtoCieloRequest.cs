using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AdquirentesVO
{
    public class DtoCieloRequest
    {
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identity { get; set; }
        public string IdentityType { get; set; }
    }

    public class CardOnFile
    {
        public string Usage { get; set; }
        public string Reason { get; set; }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Brand { get; set; }
        public bool SaveCard { get; set; }
        public CardOnFile CardOnFile { get; set; }
    }

    public class Payment
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Installments { get; set; }
        public string SoftDescriptor { get; set; }
        public CreditCard CreditCard { get; set; }
        public bool IsCryptoCurrencyNegotiation { get; set; }
        public string ReturnMessage { get; set; }
        public string ReturnCode { get; set; }
        public string AuthorizationCode { get; set; }
        public string Tid { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }
        public int ServiceTaxAmount { get; set; }
        public int Interest { get; set; }
        public bool Recurrent { get; set; }
        public string Provider { get; set; }
    }
}
