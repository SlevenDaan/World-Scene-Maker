using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_PropertyEditor
{
    delegate void OnValueChange<EventType>(IPropertyField<EventType> pPropertyField_Default) where EventType : IConvertible;

    interface IPropertyField
    {
        string Name
        {
            get;
        }
        Type Type
        {
            get;
        }
    }

    interface IPropertyField<ValueType> : IPropertyField where ValueType : IConvertible
    {
        ValueType Value
        {
            get;
            set;
        }
        event OnValueChange<ValueType> ValueChanged;
    }
}
