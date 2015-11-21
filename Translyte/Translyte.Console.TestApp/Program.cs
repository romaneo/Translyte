using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translyte.Core.Parse;

namespace Translyte.Console.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseAdapter.InitializeApp();
            var parse = new ParseAdapter();
            var str2 = parse.GetBookByLocalId("111");
            var str = parse.GetBooks();
            System.Console.WriteLine(str);
        }
    }
}
