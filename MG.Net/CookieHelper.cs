using System;
using System.Net;
using System.Text.RegularExpressions;

namespace MG.Net
{
	// Token: 0x02000003 RID: 3
	internal class CookieHelper
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000253C File Offset: 0x0000073C
		public static CookieCollection GetCookiesByHeader(string setCookie)
		{
			CookieCollection cookieCollection = new CookieCollection();
			setCookie += ",T";
			MatchCollection matchCollection = CookieHelper.RegexSplitCookie2.Matches(setCookie);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string[] array = match.Value.Split(new char[]
				{
					';'
				});
				Cookie cookie = new Cookie();
				int i = 0;
				while (i < array.Length)
				{
					string text = array[i];
					bool flag = text.Contains("=");
					if (flag)
					{
						int num = text.IndexOf('=');
						string text2 = text.Substring(0, num).Trim();
						string text3 = text.Substring(num + 1);
						bool flag2 = i == 0;
						if (flag2)
						{
							cookie.Name = text2;
							cookie.Value = text3;
						}
						else
						{
							bool flag3 = text2.Equals("Domain", StringComparison.OrdinalIgnoreCase);
							if (flag3)
							{
								cookie.Domain = text3;
							}
							else
							{
								bool flag4 = text2.Equals("Expires", StringComparison.OrdinalIgnoreCase);
								if (flag4)
								{
									DateTime expires;
									DateTime.TryParse(text3, out expires);
									cookie.Expires = expires;
								}
								else
								{
									bool flag5 = text2.Equals("Path", StringComparison.OrdinalIgnoreCase);
									if (flag5)
									{
										cookie.Path = text3;
									}
									else
									{
										bool flag6 = text2.Equals("Version", StringComparison.OrdinalIgnoreCase);
										if (flag6)
										{
											cookie.Version = Convert.ToInt32(text3);
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag7 = text.Trim().Equals("HttpOnly", StringComparison.OrdinalIgnoreCase);
						if (flag7)
						{
							cookie.HttpOnly = true;
						}
					}
				
					i++;
					continue;
					
				}
				cookieCollection.Add(cookie);
			}
			return cookieCollection;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002738 File Offset: 0x00000938
		public static string GetCookies(string setCookie, Uri uri)
		{
			string text = string.Empty;
			CookieCollection cookiesByHeader = CookieHelper.GetCookiesByHeader(setCookie);
			foreach (object obj in cookiesByHeader)
			{
				Cookie cookie = (Cookie)obj;
				bool flag = cookie.Expires < DateTime.Now && cookie.Expires != DateTime.MinValue;
				if (!flag)
				{
					bool flag2 = uri.Host.Contains(cookie.Domain);
					if (flag2)
					{
						text += string.Format("{0}={1}; ", cookie.Name, cookie.Value);
					}
				}
			}
			return text;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002808 File Offset: 0x00000A08
		public static string GetCookieValueByName(string setCookie, string name)
		{
			Regex regex = new Regex(string.Format("(?<={0}=).*?(?=; )", name));
			return regex.IsMatch(setCookie) ? regex.Match(setCookie).Value : string.Empty;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002848 File Offset: 0x00000A48
		public static string SetCookieValueByName(string setCookie, string name, string value)
		{
			Regex regex = new Regex(string.Format("(?<={0}=).*?(?=; )", name));
			bool flag = regex.IsMatch(setCookie);
			if (flag)
			{
				setCookie = regex.Replace(setCookie, value);
			}
			return setCookie;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002884 File Offset: 0x00000A84
		public static string UpdateCookieValueByName(string oldCookie, string newCookie, string name)
		{
			Regex regex = new Regex(string.Format("(?<={0}=).*?[(?=; )|$]", name));
			bool flag = regex.IsMatch(oldCookie) && regex.IsMatch(newCookie);
			if (flag)
			{
				oldCookie = regex.Replace(oldCookie, regex.Match(newCookie).Value);
			}
			return oldCookie;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static string UpdateCookieValue(string oldCookie, string newCookie)
		{
			CookieCollection cookiesByHeader = CookieHelper.GetCookiesByHeader(newCookie);
			foreach (object obj in cookiesByHeader)
			{
				Cookie cookie = (Cookie)obj;
				Regex regex = new Regex(string.Format("(?<={0}=).*?[(?=; )|$]", cookie.Name));
				oldCookie = (regex.IsMatch(oldCookie) ? regex.Replace(oldCookie, cookie.Value) : string.Format("{0}={1}; {2}", cookie.Name, cookie.Value, oldCookie));
			}
			return oldCookie;
		}

		// Token: 0x04000002 RID: 2
		private static readonly Regex RegexSplitCookie2 = new Regex("[^,][\\S\\s]+?;+[\\S\\s]+?(?=,\\S)");
	}
}
