using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Translyte.Core
{
    public class Translator
    {
        public Translator(string langFrom, string langTo) {
            LanguageFrom = langFrom;
            LanguageTo = langTo;
        }

        public string LanguageFrom { get; set; }
        public string LanguageTo { get; set; }
        //TODO: public uint TranslationsNum { get; set; }

        private const string CLIENT_ID = "tra_nsl_yte";
        private const string CLIENT_SECRET = "fe5d271ommfz25BEPQCLORQtLWg1/eItEoXn5dv9B6M=";
        private const string ACCESS_URI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        
        //This function takes a text and returns its translation on predefined language.
        public async Task<string> Translate(string text)
        {
            AccessToken token = await GetAccessToken();
            var authToken = "Bearer " + token.access_token;
            string request = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + 
                        text + 
                        "&from=" + LanguageFrom +
                        "&to=" + LanguageTo;
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", authToken);
				var result = client.GetAsync(request).GetAwaiter().GetResult();
                var xmlString = await result.Content.ReadAsStringAsync();
                var xdoc = XDocument.Parse(xmlString);
                return xdoc.Root.Value;
            }
        }
        //This function gets access token to send translation request.
        private async Task<AccessToken> GetAccessToken()
        {
            using(var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", CLIENT_ID),
                    new KeyValuePair<string, string>("client_secret", CLIENT_SECRET),
                    new KeyValuePair<string, string>("scope", "http://api.microsofttranslator.com")
                });
				var result = client.PostAsync(ACCESS_URI, content).GetAwaiter().GetResult();
                var serializer = new DataContractJsonSerializer(typeof(AccessToken));
				Stream stream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
                return (AccessToken)serializer.ReadObject(stream);
            }
        }
        //Data structure of access token.
        [DataContract]
        private class AccessToken
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
