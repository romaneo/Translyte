using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Translyte.Core
{
    public class Translator
    {
        public Translator(string langFrom = "en", string langTo = "ru", uint num = 3) {
            LanguageFrom = langFrom;
            LanguageTo = langTo;
            TranslationsNum = num;
        }
        public string LanguageFrom { get; set; }
        public string LanguageTo { get; set; }
        public uint TranslationsNum { get; set; }

        private const string CLIENT_ID = "tra_nsl_yte";
        private const string CLIENT_SECRET = "fe5d271ommfz25BEPQCLORQtLWg1/eItEoXn5dv9B6M=";
        private const string ACEESS_URI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private HttpWebResponse response;
       

        public string Translate(string text)
        {
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/GetTranslations?text=" + 
                text + 
                "&from=" + LanguageFrom + 
                "&to=" + LanguageTo + 
                "&maxTranslations=" + TranslationsNum;

            return GetTranslations(uri);
            
        }

        private string GetTranslations(string url)
        {
            // Create an HTTP web request using the URL:
            var token = GetAccessToken();
            var authToken = "Bearer " + token.access_token;

            var request = HttpWebRequest.Create(url);
            request.Headers["Authorization"] = authToken;
            request.ContentType = "text/xml";
            request.Method = "POST";

            string res = "";
            request.BeginGetResponse(new AsyncCallback(FinishWebRequest), request);
            // Get a stream representation of the HTTP web response:
            using (Stream stream = response.GetResponseStream())
            {
                res = new StreamReader(stream).ReadToEnd();
                XDocument doc = XDocument.Parse(@res);
                XNamespace ns = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2";
                int i = 1;
                res="";
                foreach (XElement xe in doc.Descendants(ns + "TranslationMatch"))
                {
                    res += string.Format("{0}Result {1}", Environment.NewLine, i++);
                    foreach (var node in xe.Elements())
                    {
                        res += string.Format("{0} = {1}", node.Name.LocalName, node.Value);
                    }
                }
            }
            return res;
        }

        private void FinishWebRequest(IAsyncResult result)
        {
            response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
        }

        Stream outputStream;
        private AdmAccessToken GetAccessToken()
        {
            var tokRequest = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", CLIENT_ID, CLIENT_SECRET);
            //Prepare OAuth request 
            var webRequest = WebRequest.Create(ACEESS_URI);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.Unicode.GetBytes(tokRequest);
            webRequest.BeginGetRequestStream(new AsyncCallback(FinishStreamRequest),webRequest);
            
            outputStream.Write(bytes, 0, bytes.Length);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
            //Get deserialized object from JSON stream
            AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(response.GetResponseStream());
            return token;
        }

        private void FinishStreamRequest(IAsyncResult result)
        {
 	        outputStream = (result.AsyncState as HttpWebRequest).EndGetRequestStream(result) as Stream;
        }
        

        [DataContract]
        public class AdmAccessToken
        {
            [DataMember]
            public string access_token { get; set; }
            [DataMember]
            public string token_type { get; set; }
            [DataMember]
            public string expires_in { get; set; }
            [DataMember]
            public string scope { get; set; }
        }
    }
}
