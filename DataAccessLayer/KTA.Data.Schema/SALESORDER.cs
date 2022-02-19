using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Schema
{
	public partial class SALESORDER
	{
		public const string TableName = "SALESORDER";

		public class Columns
		{
			public const string Id = "Id";
			public const string SO = "SO";
			public const string Pn = "Pn";
			public const string CustId = "CustId";
			public const string Qty = "Qty";
			public const string Status = "Status";
			public const string Creator = "Creator";
			public const string Cdt = "Cdt";
			public const string Udt = "Udt";
		}
	}
}
