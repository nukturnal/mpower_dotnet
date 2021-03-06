using System;

namespace MPowerPayments
{
	public class MPowerSetup
	{
		private const string ROOT_URL_BASE = "https://app.mpowerpayments.com";
		private const string LIVE_CHECKOUT_INVOICE_BASE_URL = "/api/v1/checkout-invoice/create";
		private const string TEST_CHECKOUT_INVOICE_BASE_URL = "/sandbox-api/v1/checkout-invoice/create";
		private const string LIVE_CHECKOUT_CONFIRM_BASE_URL = "/api/v1/checkout-invoice/confirm/";
		private const string TEST_CHECKOUT_CONFIRM_BASE_URL = "/sandbox-api/v1/checkout-invoice/confirm/";
		private const string LIVE_OPR_BASE_URL = "/api/v1/opr/create";
		private const string TEST_OPR_BASE_URL = "/sandbox-api/v1/opr/create";
		private const string LIVE_OPR_CHARGE_BASE_URL = "/api/v1/opr/charge";
		private const string TEST_OPR_CHARGE_BASE_URL = "/sandbox-api/v1/opr/charge";
		private const string LIVE_DIRECT_PAY_CREDIT_URL = "/api/v1/direct-pay/credit-account";
		private const string TEST_DIRECT_PAY_CREDIT_URL = "/sandbox-api/v1/direct-pay/credit-account";
		private const string LIVE_DIRECT_CREDITCARD_CHARGE_URL = "/api/v1/direct-card/processcard";
		private const string TEST_DIRECT_CREDITCARD_CHARGE_URL = "/sandbox-api/v1/direct-card/processcard";
	    private const string LIVE_DIRECT_MOBILE_CHARGE_URL = "/api/v1/direct-mobile/charge";
	    private const string LIVE_DIRECT_MOBILE_STATUS_URL = "/api/v1/direct-mobile/status";


		public string MasterKey { get; set; }
		public string PublicKey { get; set; }
		public string PrivateKey { get; set; }
		public string Token { get; set; }
		public string Mode { get; set; }

		public MPowerSetup ()
		{
		}

		public MPowerSetup (string MasterKey, string PublicKey, string PrivateKey, string Token, string Mode) 
		{
			this.MasterKey = MasterKey;
			this.PublicKey = PublicKey;
			this.PrivateKey = PrivateKey;
			this.Token = Token;
			this.Mode = Mode;
		}

		public string GetConfirmUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_CHECKOUT_CONFIRM_BASE_URL;
			} else {
				return ROOT_URL_BASE + TEST_CHECKOUT_CONFIRM_BASE_URL;
			}
		}

		public string GetInvoiceUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_CHECKOUT_INVOICE_BASE_URL;
			} else {
				return ROOT_URL_BASE + TEST_CHECKOUT_INVOICE_BASE_URL;
			}
		}

		public string GetOPRInvoiceUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_OPR_BASE_URL;
			} else {
				return ROOT_URL_BASE + TEST_OPR_BASE_URL;
			}
		}

		public string GetOPRChargeUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_OPR_CHARGE_BASE_URL;
			} else {
				return ROOT_URL_BASE + TEST_OPR_CHARGE_BASE_URL;
			}
		}

		public string GetDirectPayCreditUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_DIRECT_PAY_CREDIT_URL;
			} else {
				return ROOT_URL_BASE + TEST_DIRECT_PAY_CREDIT_URL;
			}
		}

		public string GetDirectCreditcardChargeUrl ()
		{
			if (this.Mode == "live") {
				return ROOT_URL_BASE + LIVE_DIRECT_CREDITCARD_CHARGE_URL;
			} else {
				return ROOT_URL_BASE + TEST_DIRECT_CREDITCARD_CHARGE_URL;
			}
		}

	    public string GetDirectMobileChargeUrl()
	    {
	        return this.Mode == "live"
	            ? ROOT_URL_BASE + LIVE_DIRECT_MOBILE_CHARGE_URL
	            : null;
	    }

        public string GetDirectMobileStatusUrl()
        {
            return this.Mode == "live"
                ? ROOT_URL_BASE + LIVE_DIRECT_MOBILE_STATUS_URL
                : null;
        }
	}
}

