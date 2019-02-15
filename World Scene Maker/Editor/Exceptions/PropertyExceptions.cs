using System;

namespace DS_PropertyEditor
{
    class UnknownPropertyException : Exception
    {
        public UnknownPropertyException(string pMessage) : base(pMessage)
        {

        }
    }

    class DuplicatePropertyException : Exception
    {
        public DuplicatePropertyException(string pMessage) : base(pMessage)
        {

        }
    }
}
