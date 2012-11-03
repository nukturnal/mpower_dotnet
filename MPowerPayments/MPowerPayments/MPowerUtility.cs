using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MPowerPayments
{
	public class MPowerUtility
	{
		private MPowerSetup setup;

		public MPowerUtility (MPowerSetup setup)
		{
			this.setup = setup;
		}

		public JObject HttpPostJson (string url, string payload)
		{
			var bytes = Encoding.Default.GetBytes(payload);

			WebClient client = new WebClient();
			client.Headers.Add(HttpRequestHeader.UserAgent,
			       "MPower Checkout API .NET client v1 aka Don Nigalon");

			client.Headers.Add("mp-master-key", setup.MasterKey);
			client.Headers.Add("mp-private-key", setup.PrivateKey);
			client.Headers.Add("mp-public-key", setup.PublicKey);
			client.Headers.Add("mp-token", setup.Token);
			client.Headers.Add("mp-mode", setup.Mode);
			client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

			var response = client.UploadData(url, "POST", bytes);
			
			return JObject.Parse(Encoding.Default.GetString(response));
		}

		public JObject HttpGetRequest(string url)
		{
			WebClient client = new WebClient();
			client.Headers.Add(HttpRequestHeader.UserAgent,
			       "MPower Checkout API .NET client v1 aka Don Nigalon");
			
			client.Headers.Add("mp-master-key", setup.MasterKey);
			client.Headers.Add("mp-private-key", setup.PrivateKey);
			client.Headers.Add("mp-public-key", setup.PublicKey);
			client.Headers.Add("mp-token", setup.Token);
			client.Headers.Add("mp-mode", setup.Mode);

			var response = client.DownloadString(url);

			return JObject.Parse(response);
		}

		public JObject ParseJSON(object jsontext)
		{
			string JsonString = "{}";

			try{
				JsonString = jsontext.ToString();
			}catch(NullReferenceException){
			}

			return JObject.Parse(JsonString);
		}

	}
}

