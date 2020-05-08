using System;
using System.Net;
using Rebex.Net;

namespace MG.Net
{
	// Token: 0x0200000D RID: 13
	public class HttpResponse
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002178 File Offset: 0x00000378
		public HeaderDictionary Headers { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002189 File Offset: 0x00000389
		public CookieDictionary Cookies { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000219A File Offset: 0x0000039A
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000021AB File Offset: 0x000003AB
		public string Content { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000021BC File Offset: 0x000003BC
		public Uri Address { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000021CD File Offset: 0x000003CD
		public string ContentEncoding { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000021DE File Offset: 0x000003DE
		public long ContentLength { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000021EF File Offset: 0x000003EF
		public string ContentType { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002200 File Offset: 0x00000400
		public TlsCipher TlsCipher { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002209 File Offset: 0x00000409
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002211 File Offset: 0x00000411
		public string CharacterSet { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004058 File Offset: 0x00002258
		public bool IsOk
		{
			get
			{
				return this.StatusCode == HttpStatusCode.OK;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004078 File Offset: 0x00002278
		public bool HasRedirect
		{
			get
			{
				int statusCode = (int)this.StatusCode;
				bool flag = statusCode >= 300 && statusCode < 400;
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = this.Headers.ContainsKey("Location");
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = this.Headers.ContainsKey("Redirect-Location");
						result = flag3;
					}
				}
				return result;
			}
		}
	}
}
