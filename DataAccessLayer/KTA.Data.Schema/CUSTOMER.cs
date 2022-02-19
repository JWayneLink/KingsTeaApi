using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Schema
{
	public partial class CUSTOMER
	{
		public const string TableName = "CUSTOMER";

		public class Columns
		{
			public const string Id = "Id";
			public const string CustId = "CustId";
			public const string Name = "Name";
			public const string Title = "Title";
			public const string Address = "Address";
			public const string Phone = "Phone";			
			public const string Cdt = "Cdt";
			public const string Udt = "Udt";
		}
	}
}
