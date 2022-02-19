using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Schema
{
	public partial class PRODUCT
	{
		public const string TableName = "PRODUCT";

		public class Columns
		{
			public const string Id = "Id";
			public const string Pn = "Pn";
			public const string Name = "Name";
			public const string Category = "Category";
			public const string Size = "Size";
			public const string Sugar = "Sugar";
			public const string Ice = "Ice";
			public const string Price = "Price";
			public const string Cdt = "Cdt";
			public const string Udt = "Udt";
		}
	}
}
