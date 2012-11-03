using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPowerPayments
{
	public class MPowerCheckoutInvoice : MPowerCheckout
	{
		private MPowerSetup setup;
		private MPowerStore store;
		private JObject invoice = new JObject();
		private JObject storeData = new JObject();
		private JObject items = new JObject();
		private JObject taxes = new JObject();
		private JObject customData = new JObject();
		private JObject customer = new JObject();
		private JObject actions = new JObject();
		private MPowerUtility utility;
		private string invoiceUrl { get; set; }
		private string cancelUrl { get; set; }
		private string returnUrl { get; set; }
		private string receiptUrl { get; set; }

		public MPowerCheckoutInvoice (MPowerSetup setup, MPowerStore store)
		{
			this.setup = setup;
			this.store = store;
			this.utility = new MPowerUtility(setup);

			storeData.Add("name",this.store.Name);
			storeData.Add("tagline",this.store.Tagline);
			storeData.Add("postal_address",this.store.PostalAddress);
			storeData.Add("website_url",this.store.WebsiteUrl);
			storeData.Add("phone_number",this.store.PhoneNumber);
			storeData.Add("logo_url",this.store.LogoUrl);

			actions.Add("cancel_url",this.store.CancelUrl);
			actions.Add("return_url",this.store.ReturnUrl);

		}

		public void AddItem (string name, int quantity, double price, double total_price, string description = "")
		{
			JObject item = new JObject();
			item.Add ("name",name);
			item.Add ("quantity", quantity);
			item.Add ("unit_price", price);
			item.Add ("total_price", total_price);
			item.Add ("description", description);
			items.Add ("items_"+items.Count.ToString(), item);
		}

		public void AddTax (string name, double amount)
		{
			JObject tax = new JObject();
			tax.Add ("name", name);
			tax.Add ("amount", amount);
			taxes.Add ("taxes_"+(string)taxes.Count.ToString(), tax);
		}

		public void SetTotalAmount (double amount)
		{
			invoice.Add("total_amount", amount);
		}

		public void SetDescription (string description)
		{
			invoice.Add("description", description);
		}

		public void SetCancelUrl (string url) 
		{
			actions.Add("cancel_url", url);
		}

		public void SetReturnUrl (string url) 
		{
			actions.Add("return_url", url);
		}

		public void SetInvoiceUrl(string url) 
		{
			invoiceUrl = url;
		}

		public string GetInvoiceUrl () 
		{
			return invoiceUrl;
		}

		public string GetReceiptUrl () 
		{
			return receiptUrl;
		}

		public string GetCancelUrl () 
		{
			return (string)actions["cancel_url"];
		}

		public string GetReturnUrl () 
		{
			return (string)actions["return_url"];
		}

		public void SetCustomData (string key, object value)
		{
			customData.Add(key, JToken.FromObject(value));
		}

		public object GetCustomData (string key)
		{
			return customData[key];
		}

		public object GetCustomerInfo (string key)
		{
			return customer[key];
		}

		public bool Create ()
		{
			bool result = false;
			JObject payload = new JObject ();
			invoice.Add ("items", items);
			invoice.Add ("taxes", taxes);
			payload.Add ("invoice", invoice);
			payload.Add ("store", storeData);
			payload.Add ("actions", actions);
			payload.Add ("custom_data", customData);
			string jsonData = JsonConvert.SerializeObject (payload);
			JObject jsonResult = utility.HttpPostJson (setup.GetInvoiceUrl (), jsonData);
			ResponseText = jsonResult ["response_text"].ToString ();
			ResponseCode = jsonResult ["response_code"].ToString ();
			if (jsonResult ["response_code"].ToString () == "00") {
				Status = MPowerCheckout.SUCCESS;
				ResponseText = jsonResult ["description"].ToString ();
				result = true;
			} else {
				Status = MPowerCheckout.FAIL;
			}
			return result;
		}

		public bool Confirm (string token)
		{
			JObject jsonData = utility.HttpGetRequest (setup.GetConfirmUrl () + token);
			bool result = false;

			if (jsonData.Count > 0) {
				Status = jsonData["status"].ToString();
				invoice = utility.ParseJSON (jsonData ["invoice"]);
				taxes = utility.ParseJSON (jsonData ["taxes"]);
				customData = utility.ParseJSON (jsonData ["custom_data"]);
				storeData = utility.ParseJSON (jsonData ["store"]);

				if(jsonData["status"].ToString() == "completed"){
					ResponseText = "Checkout Invoice has been paid";
					ResponseCode = "00";
					result = true;
					customer = utility.ParseJSON (jsonData ["customer"]);
					receiptUrl = (string)jsonData["receipt_url"];
				}else{
					ResponseText = "Checkout Invoice has not been paid";
					ResponseCode = "1003";
				} 
			} else {
				Status = MPowerCheckout.FAIL;
				ResponseCode = "1002";
				ResponseText = "Invoice Not Found";
			}

			return result;
		}
	}
}

