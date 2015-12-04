/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System;
using System.Net.Http.Headers;
using System.Text;

namespace Thinktecture.IdentityModel.WebApi.Authentication.Handler
{

	// from: https://github.com/thinktecture/Thinktecture.IdentityModel/blob/master/source/Thinktecture.IdentityModel.WebApi.AuthNHandler/BasicAuthenticationHeaderValue.cs
	public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
	{
		public BasicAuthenticationHeaderValue(string userName, string password)
			: base("Basic", EncodeCredential(userName, password))
		{ }

		private static string EncodeCredential(string userName, string password)
		{
			Encoding encoding = Encoding.UTF8;
			string credential = String.Format("{0}:{1}", userName, password);

			return Convert.ToBase64String(encoding.GetBytes(credential));
		}
	}
}