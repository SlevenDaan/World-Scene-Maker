using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            cbxValue.SelectionChanged += EnterStopEdit;
            cbxValue.LostFocus += LoseFocus;

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
        private void LoseFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbxValue.SelectedItem;
            Value = (ValueType)Enum.Parse(typeof(ValueType), item.Content.ToString());
            UpdateGraphics();
        }
        private void EnterStopEdit(object sender, SelectionChangedEventArgs e)
        {
            LoseFocus(sender, null);
        }

        //Events
    }
}
