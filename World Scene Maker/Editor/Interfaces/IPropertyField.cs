using System;

namespace DS_PropertyEditor
{
    delegate void OnPropertyFieldValueChange<EventType>(IPropertyField<EventType> pPropertyField) where EventType : IConvertible;

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

        //Called on initialize and when propertyeditor changes size
        void SetWidth(double pWidth);
    }

    interface IPropertyField<ValueType> : IPropertyField where ValueType : IConvertible
    {
        ValueType Value
        {
            get;
            set;
        }
        event OnPropertyFieldValueChange<ValueType> ValueChanged;
    }
}
