using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Schema
{
	public partial class ACCOUNT 
	{
		public const string TableName = "ACCOUNT";

		public class Columns
		{
			public const string Id = "Id";
			public const string Account = "Account";
			public const string Name = "Name";
			public const string Pwd = "Pwd";
			public const string Email = "Email";
			public const string Phone = "Phone";
			public const string Cdt = "Cdt";
			public const string Udt = "Udt";
		}
	}
}
