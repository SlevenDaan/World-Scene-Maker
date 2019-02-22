using System;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyField_Enum<ValueType> : PropertyField<ValueType>, IPropertyField<ValueType> where ValueType : IConvertible
    {
        //Constants

        //Variables
        private ComboBox cbxValue = new ComboBox();

        //Constructor
        public PropertyField_Enum(string pName, ValueType pValue, double pWidth) : base(pName, pValue, pWidth)
        {
            if (!typeof(ValueType).IsEnum)
            {
                throw new ArgumentException("ValueType must be an enumerated type");
            }

            //valuebox
            cbxValue.SelectionChanged += EnterStopEdit;

            foreach(ValueType value in Enum.GetValues(typeof(ValueType)))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem
                {
                    Content = value.ToString()
                };
                cbxValue.Items.Add(comboBoxItem);
            }

            this.Children.Add(cbxValue);
        }

        //Properties

        //Functions
        public override void UpdateGraphics()
        {
            base.UpdateGraphics();
            if (Value != null)
            {
                cbxValue.SelectedIndex = (int)(IConvertible)Value;
            }
        }

        public override void SetWidth(double pWidth)
        {
            cbxValue.Width = CalculateValueBoxWidth(pWidth);
            base.SetWidth(pWidth);
        }

        //Methods
        private void UpdateValue()
        {
            ComboBoxItem item = (ComboBoxItem)cbxValue.SelectedItem;
            Value = (ValueType)Enum.Parse(typeof(ValueType), item.Content.ToString());
            UpdateGraphics();
        }

        private void EnterStopEdit(object sender, SelectionChangedEventArgs e)
        {
            UpdateValue();
        }

        //Events
    }
}
