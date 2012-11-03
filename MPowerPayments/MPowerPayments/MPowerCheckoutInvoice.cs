using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPowerPayments
{
	public class MPowerCheckoutInvoice : MPowerCheckout
	{
		private MPowerSetup setup;
		private MPowerStore store;
		private Hashtable invoice = new Hashtable();
		private Hashtable storeData = new Hashtable();
		private Hashtable items = new Hashtable();
		private Hashtable taxes = new Hashtable();
		private Hashtable customData = new Hashtable();
		private Hashtable actions = new Hashtable();
		private MPowerUtility utility;
		private string invoiceUrl { get; set; }
		private string cancelUrl { get; set; }
		private string returnUrl { get; set; }

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
			Hashtable item = new Hashtable();
			item.Add ("name",name);
			item.Add ("quantity", quantity);
			item.Add ("unit_price", price);
			item.Add ("total_price", total_price);
			item.Add ("description", description);
			items.Add ("items_"+items.Count.ToString(), item);
		}

		public void AddTax (string name, double amount)
		{
			Hashtable tax = new Hashtable();
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
			customData.Add(key, value);
		}

		public object GetCustomData (string key)
		{
			return customData[key];
		}

		public string create() 
		{
			Hashtable payload = new Hashtable();
			invoice.Add ("items", items);
			invoice.Add ("taxes", taxes);
			payload.Add ("invoice",invoice);
			payload.Add ("store", storeData);
			payload.Add ("actions", actions);
			payload.Add ("custom_data", customData);
			string jsonData = JsonConvert.SerializeObject(payload);
			JObject result = utility.HttpPostJson(setup.GetInvoiceUrl(),jsonData);
			return result.ToString();
		}

		public string confirm (string token)
		{
			JObject result = utility.HttpGetRequest(setup.GetConfirmUrl()+token);
			return result.ToString();
		}
	}
}

