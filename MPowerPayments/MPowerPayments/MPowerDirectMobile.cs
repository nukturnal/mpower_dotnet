using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPowerPayments
{
    public class MPowerDirectMobile
    {
        public string Status { get; set; }
        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public string Description { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string MobileInvoiceNumber { get; set; }
        public string TransactionStatus { get; set; }

        public const string FAIL = "fail";
        public const string SUCCESS = "success";

        protected MPowerUtility Utility;
        private readonly MPowerSetup _setup;
        private readonly MPowerStore _store;

        public MPowerDirectMobile(MPowerSetup setup, MPowerStore store)
        {
            _setup = setup;
            _store = store;
            Utility = new MPowerUtility(setup);
        }

        public bool Charge(string customerName, string customerPhone
            , string customerEmail, string walletProvider, double amount)
        {
            var payload = new JObject
            {
                {"customer_name", customerName},
                {"customer_phone", customerPhone},
                {"customer_email", customerEmail},
                {"merchant_name", _store.Name},
                {"wallet_provider", walletProvider},
                {"amount", amount}
            };
            var jsonData = payload.ToString();
            var jsonResult = Utility.HttpPostJson(_setup.GetDirectMobileChargeUrl(), jsonData);
            ResponseCode = jsonResult.Value<string>("response_code");
            if (ResponseCode != "00")
            {
                Status = FAIL;
                return false;
            }
            Status = SUCCESS;
            ResponseText = jsonResult.Value<string>("response_text");
            Description = jsonResult.Value<string>("description");
            TransactionId = jsonResult.Value<string>("transaction_id");
            Token = jsonResult.Value<string>("token");
            MobileInvoiceNumber = jsonResult.Value<string>("mobile_invoice_no");
            return true;
        }

        public bool Confirm(string token)
        {
            var payload = new JObject
            {
                {"token", token}
            };
            var jsonData = payload.ToString();
            var jsonResult = Utility.HttpPostJson(_setup.GetDirectMobileStatusUrl(), jsonData);
            ResponseCode = jsonResult.Value<string>("response_code");
            TransactionStatus = jsonResult.Value<string>("tx_status");
            if (!(ResponseCode == "00" && TransactionStatus == "complete"))
            {
                Status = FAIL;
                ResponseText = jsonResult.Value<string>("cancel_reason");
                return false;
            }
            Status = SUCCESS;
            ResponseText = jsonResult.Value<string>("response_text");
            Description = jsonResult.Value<string>("description");
            TransactionId = jsonResult.Value<string>("transaction_id");
            MobileInvoiceNumber = jsonResult.Value<string>("mobile_invoice_no");
            return true;
        }
    }
}
