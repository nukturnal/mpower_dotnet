using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPowerPayments
{
	public class MPowerDirectPay
	{
		public string Status { get; set; }
		public string ResponseText { get; set; }
		public string ResponseCode { get; set; }
		public string Description { get; set; }
		public string TransactionId { get; set; }
		
		public const string FAIL = "fail";
		public const string SUCCESS = "success";

		protected MPowerUtility utility;

		private MPowerSetup setup;

		public MPowerDirectPay (MPowerSetup setup)
		{
			this.setup = setup;
			this.utility = new MPowerUtility(setup);
		}

		public bool CreditAccount (string MPowerAccount, double Amount)
		{
			bool result = false;
			JObject payload = new JObject ();

			payload.Add("account_alias",MPowerAccount);
			payload.Add("amount",Amount);
			string jsonData = JsonConvert.SerializeObject (payload);
			
			JObject JsonResult = utility.HttpPostJson (setup.GetDirectPayCreditUrl(), jsonData);

			ResponseCode = JsonResult ["response_code"].ToString ();
			if (ResponseCode == "00") {
				Status = SUCCESS;
				ResponseText = JsonResult ["response_text"].ToString ();
				Description = JsonResult ["description"].ToString ();
				TransactionId = JsonResult ["transaction_id"].ToString();
				result = true;
			} else {
				ResponseText = JsonResult ["response_text"].ToString ();
				Status = FAIL;
			}
			return result;
		}
	}
}

