using System;
using System.Collections.Generic;
using System.Net;

namespace MG.Net
{
	// Token: 0x02000004 RID: 4
	public class HeaderDictionary : Dictionary<string, string>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000208F File Offset: 0x0000028F
		public HeaderDictionary()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002984 File Offset: 0x00000B84
		public HeaderDictionary(WebHeaderCollection Headers)
		{
			foreach (string text in Headers.AllKeys)
			{
				base.Add(text.ToLower(), Headers.Get(text.ToLower()));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029D0 File Offset: 0x00000BD0
		public List<string> Headers()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				list.Add(keyValuePair.Key + ": " + keyValuePair.Value);
			}
			return list;
		}
	}
}
