using System;
using System.Collections.Generic;
using System.Text;

namespace MG.Net
{
	// Token: 0x02000011 RID: 17
	public class HttpContent : List<KeyValuePair<string, string>>
	{
		// Token: 0x1700001B RID: 27
		public object this[string paramName]
		{
			set
			{
				bool flag = paramName == null;
				if (flag)
				{
					throw new ArgumentNullException("paramName");
				}
				bool flag2 = paramName.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("paramName");
				}
				string value2 = ((value != null) ? value.ToString() : null) ?? string.Empty;
				base.Add(new KeyValuePair<string, string>(paramName, value2));
				this.postdata = this.Encoding.GetBytes(this.ListToQuery());
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002286 File Offset: 0x00000486
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000228E File Offset: 0x0000048E
		public Encoding Encoding { get; set; } = Encoding.UTF8;

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002297 File Offset: 0x00000497
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000229F File Offset: 0x0000049F
		public string ContentType { get; set; } = "application/x-www-form-urlencoded";

		// Token: 0x06000068 RID: 104 RVA: 0x000022A8 File Offset: 0x000004A8
		public HttpContent()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000022C8 File Offset: 0x000004C8
		public HttpContent(string data)
		{
			this.postdata = this.Encoding.GetBytes(data);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000022FA File Offset: 0x000004FA
		public HttpContent(byte[] data)
		{
			this.postdata = data;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000041F8 File Offset: 0x000023F8
		public byte[] GetData()
		{
			return this.postdata;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004210 File Offset: 0x00002410
		public long GetContentLength()
		{
			return (long)this.postdata.Length;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000422C File Offset: 0x0000242C
		public string GetQuery()
		{
			return this.ListToQuery();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004244 File Offset: 0x00002444
		private string ListToQuery()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				bool flag = !string.IsNullOrEmpty(keyValuePair.Key);
				if (flag)
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append('=');
					stringBuilder.Append(keyValuePair.Value);
					stringBuilder.Append('&');
				}
			}
			bool flag2 = stringBuilder.Length != 0;
			if (flag2)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000067 RID: 103
		private byte[] postdata;
	}
}
