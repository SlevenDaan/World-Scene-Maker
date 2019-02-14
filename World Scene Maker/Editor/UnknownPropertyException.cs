using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_PropertyEditor
{
    class UnknownPropertyException : Exception
    {
        public UnknownPropertyException(string pMessage) : base(pMessage)
        {

        }
    }
}
