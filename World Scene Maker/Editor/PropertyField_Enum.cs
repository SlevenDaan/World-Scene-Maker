using System;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyField_Enum<ValueType> : PropertyField<ValueType>, IPropertyField<ValueType> where ValueType : IConvertible
    {
        //Constants
        private const double SCROLLWHEEL_WIDTH = 10;

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
            cbxValue.Width = pWidth - (STACKPANEL_LEFT_MARGIN * 2) - NAMEBOX_WIDTH - SCROLLWHEEL_WIDTH;

            cbxValue.SelectionChanged += EnterStopEdit;

            foreach(ValueType value in Enum.GetValues(typeof(ValueType)))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = value.ToString();
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
