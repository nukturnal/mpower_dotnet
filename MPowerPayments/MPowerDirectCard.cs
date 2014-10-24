using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPowerPayments
{
	public class MPowerDirectCard
	{
		public string Status { get; set; }
		public string ResponseText { get; set; }
		public string ResponseCode { get; set; }
		public string Description { get; set; }
		public string TransactionId { get; set; }
		public string UnityTransactionId { get; set; }

		public const string FAIL = "fail";
		public const string SUCCESS = "success";
		
		protected MPowerUtility utility;
		
		private MPowerSetup setup;

		public MPowerDirectCard (MPowerSetup setup)
		{
			this.setup = setup;
			this.utility = new MPowerUtility(setup);
		}

		public bool Charge (double Amount, string CardName, string CardNumber, string CardCVC, string ExpMonth, string ExpYear)
		{
			bool result = false;
			JObject payload = new JObject ();
			
			payload.Add("amount",Amount);
			payload.Add("card_name",CardName);
			payload.Add("card_number",CardNumber);
			payload.Add("card_cvc",CardCVC);
			payload.Add("exp_month",ExpMonth);
			payload.Add("exp_year",ExpYear);

			string jsonData = JsonConvert.SerializeObject (payload);
			
			JObject JsonResult = utility.HttpPostJson (setup.GetDirectCreditcardChargeUrl(), jsonData);

			ResponseCode = JsonResult ["response_code"].ToString ();
			if (ResponseCode == "00") {
				Status = SUCCESS;
				ResponseText = JsonResult ["response_text"].ToString ();
				Description = JsonResult ["description"].ToString ();
				TransactionId = JsonResult ["transaction_id"].ToString();
				UnityTransactionId = JsonResult ["unity_transaction_id"].ToString();
				result = true;
			} else {
				ResponseText = JsonResult ["response_text"].ToString ();
				Status = FAIL;
			}
			return result;
		}
	}
}

