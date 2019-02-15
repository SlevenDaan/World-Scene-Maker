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
    class PropertyField_Boolean : PropertyField<bool>, IPropertyField<bool>
    {
        //Constants
        private const double SCROLLWHEEL_WIDTH = 10;

        //Variables
        private CheckBox cbxValue = new CheckBox();

        //Constructor
        public PropertyField_Boolean(string pName, bool pValue, double pWidth) : base(pName, pValue, pWidth)
        {
            //valuebox
            cbxValue.KeyUp += EnterStopEdit;
            cbxValue.LostFocus += LoseFocus;
            this.Children.Add(cbxValue);
        }

        //Properties

        //Functions
        public override void UpdateGraphics()
        {
            base.UpdateGraphics();
            cbxValue.IsChecked = (bool)Value;
        }

        //Methods
        private void LoseFocus(object sender, RoutedEventArgs e)
        {
            Value = (bool)cbxValue.IsChecked;
            UpdateGraphics();
        }
        private void EnterStopEdit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoseFocus(sender, null);
            }
        }

        //Events
    }
}
