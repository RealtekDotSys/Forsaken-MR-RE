namespace SRDebugger.Internal
{
	public static class SRDebugApiUtil
	{
		public static string ParseErrorException(global::System.Net.WebException ex)
		{
			if (ex.Response == null)
			{
				return ex.Message;
			}
			try
			{
				return ParseErrorResponse(ReadResponseStream(ex.Response));
			}
			catch
			{
				return ex.Message;
			}
		}

		public static string ParseErrorResponse(string response, string fallback = "Unexpected Response")
		{
			try
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::SRF.Json.Deserialize(response);
				string text = "";
				text += dictionary["errorMessage"];
				if (dictionary.TryGetValue("errors", out var value) && value is global::System.Collections.Generic.IList<object>)
				{
					foreach (object item in value as global::System.Collections.Generic.IList<object>)
					{
						text += "\n";
						text += item;
					}
				}
				return text;
			}
			catch
			{
				if (response.Contains("<html>"))
				{
					return fallback;
				}
				return response;
			}
		}

		public static bool ReadResponse(global::System.Net.HttpWebRequest request, out string result)
		{
			try
			{
				global::System.Net.WebResponse response = request.GetResponse();
				result = ReadResponseStream(response);
				return true;
			}
			catch (global::System.Net.WebException ex)
			{
				result = ParseErrorException(ex);
				return false;
			}
		}

		public static string ReadResponseStream(global::System.Net.WebResponse stream)
		{
			using global::System.IO.Stream stream2 = stream.GetResponseStream();
			using global::System.IO.StreamReader streamReader = new global::System.IO.StreamReader(stream2);
			return streamReader.ReadToEnd();
		}
	}
}
