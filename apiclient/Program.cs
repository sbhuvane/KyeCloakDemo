using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

//using IdentityModel.Client;

namespace apiclient
{

    public class AccessTok
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int not_before_policy { get; set; }
        public string scope { get; set; }

    }
    class Program
    {
        private static async Task Main(string[] args)
        {
           // Console.WriteLine("Hello World!");
        //    var client = new HttpClient();
        //    var response = await client.GetAsync("http://localhost:8080/auth/realms/Demo/protocol/openid-connect/auth?client_id=demo-app&redirect_uri=http://localhost:3000&response_type=code&state=1234");
           
        //    Console.WriteLine(response);
            // var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest 
            //     {
            //         Address = "http://localhost:8080/auth/realms/Demo/protocol/openid-connect/token",
            //         ClientId= "demo-app",
            //         ClientSecret = "3c608747-1c55-4004-b3e7-f08f911e678f"
            //     });
            
            // if (tokenResponse.IsError)
            // {
            //     Console.WriteLine(tokenResponse.Error);
            //     return;
            // }

            // Console.WriteLine(tokenResponse.Json);
            var bearerTotken = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJyTVBsR2sxVmdyRk4tOGdpWWFSdW1tbnIxaThRV21vRWlScmRKanZydE9ZIn0.eyJleHAiOjE2MTA3MzU2MjMsImlhdCI6MTYxMDczNTMyMywianRpIjoiN2FkNTBkNzktODY4Ny00YTY0LTg3OTktODE2ZjBjY2FhNmNhIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL2F1dGgvcmVhbG1zL0RlbW8iLCJhdWQiOlsiZGVtby13ZWIiLCJhY2NvdW50Il0sInN1YiI6IjJkZTZiZjBlLWZhY2EtNDJjYS05MjEwLTZhNzlkYTQwMTg0OSIsInR5cCI6IkJlYXJlciIsImF6cCI6ImRlbW8tYXBwIiwiYWNyIjoiMSIsInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIl19LCJyZXNvdXJjZV9hY2Nlc3MiOnsiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19fSwic2NvcGUiOiJwcm9maWxlIGdvb2Qtc2VydmljZSBlbWFpbCIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiY2xpZW50SG9zdCI6IjE3Mi4xNy4wLjEiLCJjbGllbnRJZCI6ImRlbW8tYXBwIiwicHJlZmVycmVkX3VzZXJuYW1lIjoic2VydmljZS1hY2NvdW50LWRlbW8tYXBwIiwiY2xpZW50QWRkcmVzcyI6IjE3Mi4xNy4wLjEifQ.siFSPtxN9BHdgTk5v-JzQkL99I6zdwJJZhAAG08hBaIha6O1RmHAYsU2RqEDaIWeuIx81MRFEpGIu4yqwA2isZBT6vFhYWbK1aLWme4EEN_PaRvzXeI7RpJ01C6syQY_0jQxAV0SoR5-TBP1X-E177GyCFCoskZJDGdx4fxZcOFV99UuqJieOjZ_sb_JkT-m6sKssDeN6W0WYuaHmKRbgmN1GxFRdQfT8oulP_7-s8WgBB7D3Lv0iPNK6WhxW1rhrAeioSP3rJELfmx3F5ICAd89hb7vU9TROWVPb0HRiPfKYzmf4oUm75cszdQq5DjDBKYGAQ-amy4MtmAKr8Pfqg";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerTotken);
            
            var response = await client.GetAsync("https://localhost:6001/weatherforecast");

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            Console.WriteLine("\n\n");

            var client2 = new HttpClient();
            var uri = new Uri("http://localhost:8080/auth/realms/Demo/protocol/openid-connect/token");
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var values = new Dictionary<string, string>
            {
                {"client_id", "demo-app"},
                {"client_secret", "3c608747-1c55-4004-b3e7-f08f911e678f"},
                {"grant_type", "client_credentials"}

            };
            var content = new FormUrlEncodedContent(values);
            var token = await client2.PostAsync(uri, content);

            var res = await token.Content.ReadAsStringAsync();

         //   Console.WriteLine(res);
            Console.WriteLine("\n\n");
            Console.WriteLine("Before options: ");
            Console.WriteLine(token.Content.GetType());
            Console.WriteLine(res.GetType());
            //var acc = await token.Content.ReadFromJsonAsync<AccessTok>();
            //var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            // {
            //      PropertyNameCaseInsensitive = true
            // };
           // var readOnlySpan = new ReadOnlySpan<byte>(res);
           Console.WriteLine("Before Deserialize");
         //   var accessToken = JsonSerializer.Deserialize<AccessTok>(res);
            Console.WriteLine("After Deserialize");
         //   Console.WriteLine(accessToken);
            Console.WriteLine("\n\n");
            var contentobj = await token.Content.ReadFromJsonAsync<Object>();
          Console.WriteLine(contentobj.GetType());
          // Console.WriteLine($"token: {accessToken.access_token}");
            

        }
    } 
}
//session_state=4dcd5b93-0426-4cb5-a3f6-f544d057d70e&code=903ed626-d1f8-4712-80c1-f7bd62ae7026.4dcd5b93-0426-4cb5-a3f6-f544d057d70e.0b3012f4-3dcd-4397-a012-6698f7c13f4b