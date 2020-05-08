using System;
using System.Linq;
using Rebex.Net;

namespace MG.Net
{
	// Token: 0x0200000F RID: 15
	public class ProxyClient : Proxy
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000040E4 File Offset: 0x000022E4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000221A File Offset: 0x0000041A
		public new ProxyType ProxyType
		{
			get
			{
				return (ProxyType)base.ProxyType;
			}
			set
			{
				base.ProxyType = (Rebex.Net.ProxyType)value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000040FC File Offset: 0x000022FC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002225 File Offset: 0x00000425
		public ProxyAuthentication ProxyAuthentication
		{
			get
			{
				return (ProxyAuthentication)base.AuthenticationMethod;
			}
			set
			{
				base.AuthenticationMethod = (Rebex.Net.ProxyAuthentication)value;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004114 File Offset: 0x00002314
		public ProxyClient(ProxyType Type, string Address)
		{
			this.ProxyType = Type;
			string[] array = Address.Split(new char[]
			{
				':'
			});
			base.Host = array[0];
			base.Port = Convert.ToInt32(array[1]);
			bool flag = array.Count<string>() > 2;
			if (flag)
			{
				base.UserName = array[2];
				base.Password = array[3];
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002230 File Offset: 0x00000430
		public ProxyClient(ProxyType Type, string Host, int Port)
		{
			this.ProxyType = Type;
			base.Host = Host;
			base.Port = Port;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002252 File Offset: 0x00000452
		public ProxyClient(ProxyType Type, string Host, int Port, string Username, string Password)
		{
			this.ProxyType = Type;
			base.Host = Host;
			base.Port = Port;
			base.UserName = Username;
			base.Password = Password;
		}
	}
}
