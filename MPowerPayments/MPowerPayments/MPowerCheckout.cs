using System;
using RestSharp;

namespace MPowerPayments
{
	public class MPowerCheckout
	{
		public string Status { get; set; }
		public string ResponseText { get; set; }
		public string ResponseCode { get; set; }

		public MPowerCheckout ()
		{
		}
	}
}

