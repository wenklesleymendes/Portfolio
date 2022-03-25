using EscolaPro.ControleBoleto.ItauResponse;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EscolaPro.ControleBoleto
{
    public class AuthItauService : IAuthItauService
    {
        public async Task<AuthTokenItau> AutenticarItau()
        {
			try
			{
				var client = new RestClient("https://oauth.itau.com.br/identity/connect/token");
				client.Timeout = -1;
				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				request.AddHeader("Authorization", "Basic b0F0Y1hjV1p2Q2Q5MDozTXRwTFBhdVpieVk3YUxZbXQ4NFZDanFuTTNLZTUzR0FCSjNJYUg4M21odnZ2RnlwQmxmbzN5cE9Cd3J4bUhlbGlSdzU3alpYM0U4WXdVRWpXT25HdzI=");
				request.AddHeader("Cookie", "TS01f0d093=01ca9250c72c41382164c072afc39a41403a8a4c43cb6d620b21d8be6cbc593e7a394e418d");
				request.AddParameter("scope", "readonly");
				request.AddParameter("grant_type", "client_credentials");
				IRestResponse response = client.Execute(request);
				Console.WriteLine(response.Content);

				var authToken = JsonConvert.DeserializeObject<AuthTokenItau>(response.Content);

				return authToken;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}
