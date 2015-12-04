using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZU
{
	public static class RegexUtilities
	{
		static Regex ValidEmailRegex = CreateValidEmailRegex();

		static Regex ValidDomainRegex = CreateValidDomainRegex();

		/// <summary>
		/// Taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
		/// </summary>
		/// <returns></returns>
		private static Regex CreateValidEmailRegex()
		{
			string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
				+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
				+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			//string validEmailPattern = "^(?!\.)(\"([^\"\r\\]|\\[\"\r\\])\"|([-a-z0-9!#$%&'+/=?^_`{|}~]|(?<!\.)\.))(?<!\.)@[a-z0-9][\w\.-][a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
		/// </summary>
		/// <returns></returns>
		private static Regex CreateValidDomainRegex()
		{
			string validDomainPattern = @"[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			return new Regex(validDomainPattern, RegexOptions.IgnoreCase);
		}

		public static bool EmailIsValid(string emailAddress)
		{
			bool isValid = ValidEmailRegex.IsMatch(emailAddress);

			return isValid;
		}

		public static bool TenantNameIsValid(string tenantName)
		{
			bool isValid = ValidDomainRegex.IsMatch(tenantName);

			return isValid;
		}
	} // class
} // namespace
