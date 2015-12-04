using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Constants;
using Thinktecture.IdentityModel.WebApi.Authentication.Handler;

namespace System.Net.Http
{
	// from: https://github.com/thinktecture/Thinktecture.IdentityModel.45/blob/master/IdentityModel/Thinktecture.IdentityModel/Extensions/HttpClientExtensions.cs
	public static class HttpClientExtensions
	{
		public static void SetBasicAuthentication(this HttpClient client, string userName, string password)
		{
			client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, password);
		}

		public static void SetToken(this HttpClient client, string scheme, string token)
		{
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
		}

		public static void SetBearerToken(this HttpClient client, string token)
		{
			client.SetToken(JwtConstants.Bearer, token);
		}
	} // class
} // namespace
