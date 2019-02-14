using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyEditor : ListView
    {
        //Variables
        private const double PROPERTY_WIDTH = 350;

        //Constructor

        //Properties

        //Functions
        public void AddProperty<Type> (string pPropertyName, Type pValue) where Type:IConvertible
        {
            PropertyField<Type> propertyField = new PropertyField<Type>(pPropertyName, pValue, PROPERTY_WIDTH);

            this.Items.Add(propertyField);
        }

        public PropertyField<Type> GetProperty<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Items.Count; intC++)
            {
                try
                {
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Items[intC];
                    if (propertyField.Name == pPropertyName)
                    {
                        return propertyField;
                    }
                }
                catch { }
            }

            throw new UnknownPropertyException("Property \"" + pPropertyName + "\" does not exist.");
        }

        /*
        public Type GetPropertyValue<Type>(string pPropertyName) where Type : IConvertible
        {
            for (int intC = 0; intC < this.Items.Count; intC++)
            {
                try
                {
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Items[intC];
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
            for (int intC = 0; intC < this.Items.Count; intC++)
            {
                try
                {
                    PropertyField<Type> propertyField = (PropertyField<Type>)this.Items[intC];
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
