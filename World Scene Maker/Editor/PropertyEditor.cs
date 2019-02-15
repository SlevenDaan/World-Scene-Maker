using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DS_PropertyEditor
{
    class PropertyEditor : StackPanel
    {
        //Variables
        private const double PROPERTY_WIDTH = 350;

        public delegate void OnPropertyEditorValueChange(PropertyEditor propertyEditor);
        private OnPropertyEditorValueChange onValueChange;

        private Dictionary<string, IPropertyField> dicEditorFields = new Dictionary<string, IPropertyField>();

        //Constructor
        public PropertyEditor()
        {
            this.Orientation = Orientation.Vertical;
            this.Background = new SolidColorBrush(Colors.LightGray);
        }

        //Properties

        //Functions
        public void AddProperty<ValueType>(string pPropertyName, ValueType pValue) where ValueType : IConvertible
        {
            if (!dicEditorFields.ContainsKey(pPropertyName))
            {
                IPropertyField iPropertyField = null;

                if (typeof(ValueType) == typeof(bool))
                {
                    PropertyField_Boolean PropertyField = new PropertyField_Boolean(pPropertyName, (bool)((IConvertible)pValue), PROPERTY_WIDTH);
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }
                else if (typeof(ValueType).IsEnum)
                {
                    PropertyField_Enum<ValueType> PropertyField = new PropertyField_Enum<ValueType>(pPropertyName, pValue, PROPERTY_WIDTH);
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }
                else
                {
                    PropertyField_Default<ValueType> PropertyField = new PropertyField_Default<ValueType>(pPropertyName, pValue, PROPERTY_WIDTH);
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }

                dicEditorFields.Add(pPropertyName, iPropertyField);
            }
            else
            {
                throw new DuplicatePropertyException("Property \"" + pPropertyName + "\" already exist.");
            }
        }
        public void RemoveProperty(string pPropertyName)
        {
            if (dicEditorFields.ContainsKey(pPropertyName))
            {
                this.Children.Remove((UIElement)dicEditorFields[pPropertyName]);
                dicEditorFields.Remove(pPropertyName);
            }
            else
            {
                throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
            }
        }
        
        public bool PropertyExists(string pPropertyName)
        {
            return dicEditorFields.ContainsKey(pPropertyName);
        }

        public IPropertyField<ValueType> GetProperty<ValueType>(string pPropertyName) where ValueType : IConvertible
        {
            if (dicEditorFields.ContainsKey(pPropertyName))
            {
                return (IPropertyField<ValueType>)dicEditorFields[pPropertyName];
            }
            else
            {
                throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
            }
        }
        public Dictionary<string, Type> GetAllProperties()
        {
            Dictionary<string, Type> dicReturn = new Dictionary<string, Type>();
            foreach(string key in dicEditorFields.Keys)
            {
                dicReturn.Add(key, dicEditorFields[key].Type);
            }
            return dicReturn;
        }

        //Methods
        private void PropertyFieldValueChanged<ValueType>(IPropertyField<ValueType> pPropertyField) where ValueType : IConvertible
        {
            if (onValueChange != null)
            {
                onValueChange(this);
            }
        }

        //Events
        public event OnPropertyEditorValueChange ValueChanged
        {
            add
            {
                onValueChange += value;
            }
            remove
            {
                onValueChange -= value;
            }
        }

    }
}
