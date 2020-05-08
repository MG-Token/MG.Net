using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Rebex.Net;
using Rebex.Security.Certificates;

namespace MG.Net
{
	// Token: 0x02000007 RID: 7
	public class HttpRequest
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020AC File Offset: 0x000002AC
		public HeaderDictionary Headers { get; set; } = new HeaderDictionary();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000020BD File Offset: 0x000002BD
		public CookieDictionary Cookies { get; set; } = new CookieDictionary(false);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000020CE File Offset: 0x000002CE
		public bool Ssl { get; set; } = true;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000020DF File Offset: 0x000002DF
		public bool UseProxy { get; set; } = false;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000020F0 File Offset: 0x000002F0
		public ProxyClient Proxy { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002101 File Offset: 0x00000301
		public string UserAgent { get; set; } = string.Empty;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002112 File Offset: 0x00000312
		public string Referer { get; set; } = string.Empty;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002123 File Offset: 0x00000323
		public bool KeepAlive { get; set; } = true;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002134 File Offset: 0x00000334
		public bool AllowAutoRedirect { get; set; } = true;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002145 File Offset: 0x00000345
		public bool Expect100 { get; set; } = false;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002156 File Offset: 0x00000356
		public int TimeOut { get; set; } = 60000;

		// Token: 0x06000031 RID: 49 RVA: 0x00002C58 File Offset: 0x00000E58
		public HttpResponse Row(Uri url, HttpMethod method, HttpContent content = null)
		{
			HttpResponse httpResponse = new HttpResponse();
			try
			{
				HttpRequestCreator httpRequestCreator = new HttpRequestCreator();
				bool ssl = this.Ssl;
				if (ssl)
				{
					httpRequestCreator.Settings.SslAcceptAllCertificates = false;
				}
				else
				{
					httpRequestCreator.Settings.SslAcceptAllCertificates = true;
				}
				httpRequestCreator.ValidatingCertificate += this.Creator_ValidatingCertificate;
				httpRequestCreator.Settings.SslAllowedVersions = TlsVersion.Any;
				bool flag = this.UseProxy && this.Proxy != null;
				if (flag)
				{
					Proxy proxy = this.Proxy;
					proxy.SendRetryTimeout = this.TimeOut;
					httpRequestCreator.Proxy = proxy;
				}
				var httpRequest = new Rebex.Net.HttpRequest(url, httpRequestCreator);
				httpRequest.Method = this.Method(method);
				httpRequest.ContinueTimeout = this.TimeOut;
				httpRequest.Timeout = this.TimeOut;
				bool flag2 = this.Headers.Count > 0;
				if (flag2)
				{
					foreach (KeyValuePair<string, string> keyValuePair in this.Headers)
					{
						httpRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				bool flag3 = this.UserAgent != string.Empty;
				if (flag3)
				{
					httpRequest.UserAgent = this.UserAgent;
				}
				bool flag4 = this.Referer != string.Empty;
				if (flag4)
				{
					httpRequest.Referer = this.Referer;
				}
				bool expect = this.Expect100;
				if (expect)
				{
					httpRequest.Expect100Continue = true;
				}
				else
				{
					httpRequest.Expect100Continue = false;
				}
				bool flag5 = this.Cookies.Count > 0;
				if (flag5)
				{
					httpRequest.Headers.Add(HttpRequestHeader.Cookie, this.Cookies.ToString());
				}
				httpRequest.Method = this.Method(method);
				httpRequest.KeepAlive = this.KeepAlive;
				httpRequest.AllowAutoRedirect = this.AllowAutoRedirect;
				bool flag6 = content != null;
				if (flag6)
				{
					byte[] data = content.GetData();
					httpRequest.ContentType = content.ContentType;
					httpRequest.ContentLength = content.GetContentLength();
					using (Stream requestStream = httpRequest.GetRequestStream())
					{
						requestStream.Write(data, 0, data.Length);
					}
				}
				using (var httpResponse2 = (Rebex.Net.HttpResponse)httpRequest.GetResponse())
				{
					httpResponse.ContentEncoding = httpResponse2.ContentEncoding;
					httpResponse.ContentLength = httpResponse2.ContentLength;
					httpResponse.ContentType = httpResponse2.ContentType;
					httpResponse.TlsCipher = httpResponse2.Cipher;
					httpResponse.CharacterSet = httpResponse2.CharacterSet;
					httpResponse.Address = httpResponse2.ResponseUri;
					httpResponse.StatusCode = httpResponse2.StatusCode;
					httpResponse.Headers = new HeaderDictionary(httpResponse2.Headers);
					httpResponse.Cookies = this.Cookies.SetCookie(httpResponse2.Headers);
					using (StreamReader streamReader = new StreamReader(httpResponse2.GetResponseStream()))
					{
						httpResponse.Content = streamReader.ReadToEnd();
					}
				}
			}
			catch (WebException ex)
			{
				var httpResponse3 = (Rebex.Net.HttpResponse)ex.Response;
				bool flag7 = httpResponse3 != null;
				if (!flag7)
				{
					throw new HttpException(ex.Message);
				}
				httpResponse.ContentEncoding = httpResponse3.ContentEncoding;
				httpResponse.ContentLength = httpResponse3.ContentLength;
				httpResponse.ContentType = httpResponse3.ContentType;
				httpResponse.TlsCipher = httpResponse3.Cipher;
				httpResponse.CharacterSet = httpResponse3.CharacterSet;
				httpResponse.Address = httpResponse3.ResponseUri;
				httpResponse.StatusCode = httpResponse3.StatusCode;
				httpResponse.Headers = new HeaderDictionary(httpResponse3.Headers);
				httpResponse.Cookies = this.Cookies.SetCookie(httpResponse3.Headers);
				using (StreamReader streamReader2 = new StreamReader(httpResponse3.GetResponseStream()))
				{
					httpResponse.Content = streamReader2.ReadToEnd();
				}
			}
			return httpResponse;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003104 File Offset: 0x00001304
		public async Task<HttpResponse> RowAsync(Uri url, HttpMethod method, HttpContent content = null)
		{
			HttpResponse res = new HttpResponse();
		
			try
			{
				HttpRequestCreator creator = new HttpRequestCreator();
				bool ssl = this.Ssl;
				if (ssl)
				{
					creator.Settings.SslAcceptAllCertificates = false;
				}
				else
				{
					creator.Settings.SslAcceptAllCertificates = true;
				}
				creator.ValidatingCertificate += this.Creator_ValidatingCertificate;
				creator.Settings.SslAllowedVersions = TlsVersion.Any;
				bool flag = this.UseProxy && this.Proxy != null;
				if (flag)
				{
					Proxy proxy = this.Proxy;
					proxy.SendRetryTimeout = this.TimeOut;
					creator.Proxy = proxy;
					proxy = null;
				}
				var request = new Rebex.Net.HttpRequest(url, creator);
				request.Method = this.Method(method);
				request.ContinueTimeout = this.TimeOut;
				request.Timeout = this.TimeOut;
				bool flag2 = this.Headers.Count > 0;
				if (flag2)
				{
					foreach (KeyValuePair<string, string> item in this.Headers)
					{
						request.Headers.Add(item.Key, item.Value);

					}
				}
				bool flag3 = this.UserAgent != string.Empty;
				if (flag3)
				{
					request.UserAgent = this.UserAgent;
				}
				bool flag4 = this.Referer != string.Empty;
				if (flag4)
				{
					request.Referer = this.Referer;
				}
				bool expect = this.Expect100;
				if (expect)
				{
					request.Expect100Continue = true;
				}
				else
				{
					request.Expect100Continue = false;
				}
				bool flag5 = this.Cookies.Count > 0;
				if (flag5)
				{
					request.Headers.Add(HttpRequestHeader.Cookie, this.Cookies.ToString());
				}
				request.Method = this.Method(method);
				request.KeepAlive = this.KeepAlive;
				request.AllowAutoRedirect = this.AllowAutoRedirect;
				bool flag6 = content != null;
				if (flag6)
				{
					byte[] postData = content.GetData();
					request.ContentType = content.ContentType;
					request.ContentLength = content.GetContentLength();
					Stream stream2 = await request.GetRequestStreamAsync();
					Stream stream = stream2;
					stream2 = null;
					try
					{
						stream.Write(postData, 0, postData.Length);
					}
					finally
					{
						if (stream != null)
						{
							((IDisposable)stream).Dispose();
						}
					}
					stream = null;
					postData = null;
				}
				WebResponse webResponse = await request.GetResponseAsync();
				var response = (Rebex.Net.HttpResponse)webResponse;
				webResponse = null;
				try
				{
					res.ContentEncoding = response.ContentEncoding;
					res.ContentLength = response.ContentLength;
					res.ContentType = response.ContentType;
					res.TlsCipher = response.Cipher;
					res.CharacterSet = response.CharacterSet;
					res.Address = response.ResponseUri;
					res.StatusCode = response.StatusCode;
					res.Headers = new HeaderDictionary(response.Headers);
					res.Cookies = this.Cookies.SetCookie(response.Headers);
					using (StreamReader sr = new StreamReader(response.GetResponseStream()))
					{
						HttpResponse httpResponse = res;
						string content2 = await sr.ReadToEndAsync();
						httpResponse.Content = content2;
						httpResponse = null;
						content2 = null;
					}
					//StreamReader sr = null;
				}
				finally
				{
					if (response != null)
					{
						((IDisposable)response).Dispose();
					}
				}
				response = null;
				creator = null;
				request = null;
			}
			catch (WebException obj)
			{
                WebException ex = obj;
                var response2 = (Rebex.Net.HttpResponse)ex.Response;
                if (response2 == null)
                {
                    throw new HttpException(ex.Message);
                }
                res.ContentEncoding = response2.ContentEncoding;
                res.ContentLength = response2.ContentLength;
                res.ContentType = response2.ContentType;
                res.TlsCipher = response2.Cipher;
                res.CharacterSet = response2.CharacterSet;
                res.Address = response2.ResponseUri;
                res.StatusCode = response2.StatusCode;
                res.Headers = new HeaderDictionary(response2.Headers);
                res.Cookies = this.Cookies.SetCookie(response2.Headers);
                using (StreamReader sr2 = new StreamReader(response2.GetResponseStream()))
                {
                    HttpResponse httpResponse2 = res;
                    string content3 = await sr2.ReadToEndAsync();
                    httpResponse2.Content = content3;
                    httpResponse2 = null;
                    content3 = null;
                }
                //StreamReader sr2 = null;
                response2 = null;
                ex = null;
            }
            
			return res;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000215F File Offset: 0x0000035F
		private string Method(HttpMethod method)
		{
			return method.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003160 File Offset: 0x00001360
		private void Creator_ValidatingCertificate(object sender, SslCertificateValidationEventArgs e)
		{
			Certificate certificate = e.CertificateChain[0];
		}
	}
}
