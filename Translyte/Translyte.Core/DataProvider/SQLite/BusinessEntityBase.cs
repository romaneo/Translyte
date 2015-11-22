using System;
using Translyte.Core.DataProvider.SQLite;

namespace Translyte.Core.DataProvider.SQLite
{
	/// <summary>
	/// Business entity base class. Provides the ID property.
	/// </summary>
	public abstract class BusinessEntityBase : IBusinessEntity {
		public BusinessEntityBase ()
		{
		}
		
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
	}
}
