using System;

namespace MPowerPayments
{
	public class MPowerCheckout
	{
		public string Status { get; set; }
		public string ResponseText { get; set; }
		public string ResponseCode { get; set; }
		public string Token { get; set; }

		public const string FAIL = "fail";
		public const string SUCCESS = "success";


		public MPowerCheckout ()
		{
		}
	}
}

