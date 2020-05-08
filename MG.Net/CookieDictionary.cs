using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MG.Net
{
	// Token: 0x02000002 RID: 2
	public class CookieDictionary : Dictionary<string, string>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public bool IsLocked { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public CookieDictionary(bool isLocked = false) : base(StringComparer.OrdinalIgnoreCase)
		{
			this.IsLocked = isLocked;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002324 File Offset: 0x00000524
		public CookieDictionary(WebHeaderCollection Headers, bool isLocked = false) : base(StringComparer.OrdinalIgnoreCase)
		{
			this.IsLocked = isLocked;
			foreach (string text in Headers.AllKeys)
			{
				if (text.ToLower().Contains("set-cookie"))
				{
					foreach (object obj in CookieHelper.GetCookiesByHeader(Headers.Get(text)))
					{
						Cookie cookie = (Cookie)obj;
						if (base.ContainsKey(cookie.Name))
						{
							base.Remove(cookie.Name);
						}
						base.Add(cookie.Name, cookie.Value);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023F4 File Offset: 0x000005F4
		public CookieDictionary SetCookie(WebHeaderCollection Headers)
		{
			foreach (string text in Headers.AllKeys)
			{
				if (text.ToLower().Contains("set-cookie"))
				{
					foreach (object obj in CookieHelper.GetCookiesByHeader(Headers.Get(text)))
					{
						Cookie cookie = (Cookie)obj;
						if (base.ContainsKey(cookie.Name))
						{
							base.Remove(cookie.Name);
						}
						base.Add(cookie.Name, cookie.Value);
					}
				}
			}
			return this;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024B4 File Offset: 0x000006B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				stringBuilder.AppendFormat("{0}={1}; ", keyValuePair.Key, keyValuePair.Value);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}
	}
}
