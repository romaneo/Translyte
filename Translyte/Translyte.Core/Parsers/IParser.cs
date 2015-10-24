using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Translyte.Core.Parsers
{
    public interface IParser
    {
        void Parse(ref Book book);
    }
}
