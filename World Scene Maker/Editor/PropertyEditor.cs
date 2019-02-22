using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DS_PropertyEditor
{
    /// <summary>
    /// An object that makes it possible to easily add/remove properties and change their values.
    /// </summary>
    class PropertyEditor : StackPanel
    {
        //Variables
        private const double PROPERTY_MIN_WIDTH = PropertyField<string>.NAMEBOX_WIDTH + PropertyField<string>.VALUEBOX_MIN_WIDTH + 2 * PropertyField<string>.STACKPANEL_LEFT_MARGIN;

        public delegate void OnPropertyEditorValueChange(PropertyEditor propertyEditor);
        private OnPropertyEditorValueChange onValueChange;

        private Dictionary<string, IPropertyField> dicEditorFields = new Dictionary<string, IPropertyField>();

        //Constructor
        public PropertyEditor()
        {
            this.Width = CalculateWidth();
            this.Orientation = Orientation.Vertical;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.SizeChanged += PropertyEditorSizeChanged;
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
                    PropertyField_Boolean PropertyField = new PropertyField_Boolean(pPropertyName, (bool)((IConvertible)pValue), CalculateWidth());
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }
                else if (typeof(ValueType) == typeof(DateTime))
                {
                    PropertyField_DateTime PropertyField = new PropertyField_DateTime(pPropertyName, (DateTime)((IConvertible)pValue), CalculateWidth());
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }
                else if (typeof(ValueType).IsEnum)
                {
                    PropertyField_Enum<ValueType> PropertyField = new PropertyField_Enum<ValueType>(pPropertyName, pValue, CalculateWidth());
                    PropertyField.ValueChanged += this.PropertyFieldValueChanged;
                    this.Children.Add(PropertyField);
                    iPropertyField = PropertyField;
                }
                else
                {
                    PropertyField_Default<ValueType> PropertyField = new PropertyField_Default<ValueType>(pPropertyName, pValue, CalculateWidth());
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

        private void PropertyEditorSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Correct width of all properties
            this.Width = CalculateWidth();
            foreach (string key in dicEditorFields.Keys)
            {
                dicEditorFields[key].SetWidth(this.Width);
            }
        }

        private double CalculateWidth()
        { 
            if (this.ActualWidth < PROPERTY_MIN_WIDTH)
            {
                return PROPERTY_MIN_WIDTH;
            }
            return this.ActualWidth;

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
