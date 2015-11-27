using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translyte.Core.DataProvider.SQLite;

namespace Translyte.Core.Models
{
    public class BookLocal : IBusinessEntity
    {
        public int Position { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string BookPath { get; set; }
        public bool IsCurrent { get; set; }
    }
}
