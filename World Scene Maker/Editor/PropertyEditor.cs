using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddProperty<Type> (string pPropertyName, Type pValue) where Type:IConvertible
        {
            PropertyField<Type> propertyField = new PropertyField<Type>(pPropertyName, pValue, PROPERTY_WIDTH);

            this.Children.Add(propertyField);
        }

        public PropertyField<Type> GetProperty<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Children.Count; intC++)
            {
                try
                {
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Children[intC];
                    if (propertyField.Name == pPropertyName)
                    {
                        return propertyField;
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
                    IPropertyEditorSearchable thisPropertyField = (IPropertyEditorSearchable)this.Children[intC];
                    dicProperties.Add(thisPropertyField.Name, thisPropertyField.Type);
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
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Children[intC];
                    if (propertyField.Name == pPropertyName)
                    {
                        return propertyField.Value;
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
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Children[intC];
                    if (propertyField.Name == pPropertyName)
                    {
                        propertyField.Value = pValue;
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
