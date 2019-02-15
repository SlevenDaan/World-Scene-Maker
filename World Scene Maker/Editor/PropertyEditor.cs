using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DS_PropertyEditor
{
    class PropertyEditor : StackPanel
    {
        //Variables
        private const double PROPERTY_WIDTH = 350;

        //Constructor
        public PropertyEditor()
        {
            this.Orientation = Orientation.Vertical;
            this.Background = new SolidColorBrush(Colors.LightGray);
        }

        //Properties

        //Functions
        public void AddProperty<Type>(string pPropertyName, Type pValue) where Type : IConvertible
        {
            if (typeof(Type) == typeof(bool))
            {
                PropertyField_Boolean PropertyField = new PropertyField_Boolean(pPropertyName, (bool)((IConvertible)pValue), PROPERTY_WIDTH);
                this.Children.Add(PropertyField);
            }
            else if (typeof(Type).IsEnum)
            {
                PropertyField_Enum<Type> PropertyField = new PropertyField_Enum<Type>(pPropertyName, pValue, PROPERTY_WIDTH);
                this.Children.Add(PropertyField);
            }
            else
            {
                PropertyField_Default<Type> PropertyField = new PropertyField_Default<Type>(pPropertyName, pValue, PROPERTY_WIDTH);
                this.Children.Add(PropertyField);
            }
        }
        public void RemoveProperty<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    PropertyField_Default<Type> PropertyField = (PropertyField_Default<Type>)this.Children[intC];
                    if (PropertyField.Name == pPropertyName)
                    {
                        this.Children.RemoveAt(intC);
                    }
                }
                catch { }
            }

            throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
        }

        public IPropertyField<Type> GetProperty<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    IPropertyField<Type> PropertyField = (IPropertyField<Type>)this.Children[intC];
                    if (PropertyField.Name == pPropertyName)
                    {
                        return PropertyField;
                    }
                }
                catch { }
            }

            throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
        }
        
        public Dictionary<string, Type> GetAllProperties()
        {
            Dictionary<string, Type> dicProperties = new Dictionary<string, Type>();

            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    IPropertyField PropertyField = (IPropertyField)this.Children[intC];
                    dicProperties.Add(PropertyField.Name, PropertyField.Type);
                }
                catch { }
            }

            return dicProperties;
        }

        /*
        public Type GetPropertyValue<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    PropertyField_Default<Type> PropertyField_Default = (PropertyField_Default<Type>)this.Children[intC];
                    if (PropertyField_Default.Name == pPropertyName)
                    {
                        return PropertyField_Default.Value;
                    }
                }
                catch { }
            }

            throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
        }
        public void SetPropertyValue<Type>(string pPropertyName,Type pValue) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    PropertyField_Default<Type> PropertyField_Default = (PropertyField_Default<Type>)this.Children[intC];
                    if (PropertyField_Default.Name == pPropertyName)
                    {
                        PropertyField_Default.Value = pValue;
                        return;
                    }
                }
                catch { }
            }

            throw new UnknownPropertyException("Property \""+pPropertyName+"\" does not exist.");
        }
        */
    }
}
