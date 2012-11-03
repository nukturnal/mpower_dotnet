using System;
using Mono.CSharp;
using Microsoft.CSharp;

namespace MPowerPayments
{
	public class MPowerSetup
	{
		private const string LIVE_CHECKOUT_INVOICE_BASE_URL = "http://localhost:3000/api/v1/checkout-invoice/create";
		private const string TEST_CHECKOUT_INVOICE_BASE_URL = "http://localhost:3000/sandbox-api/v1/checkout-invoice/create";
		private const string LIVE_CHECKOUT_CONFIRM_BASE_URL = "http://localhost:3000/api/v1/checkout-invoice/confirm/";
		private const string TEST_CHECKOUT_CONFIRM_BASE_URL = "http://localhost:3000/sandbox-api/v1/checkout-invoice/confirm/";

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
				return LIVE_CHECKOUT_CONFIRM_BASE_URL;
			} else {
				return TEST_CHECKOUT_CONFIRM_BASE_URL;
			}
		}

		public string GetInvoiceUrl ()
		{
			if (this.Mode == "live") {
				return LIVE_CHECKOUT_INVOICE_BASE_URL;
			} else {
				return TEST_CHECKOUT_INVOICE_BASE_URL;
			}
		}
	}
}

