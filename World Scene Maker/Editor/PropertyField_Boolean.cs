using System.Windows;
using System.Windows.Controls;

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
            cbxValue.Width = pWidth - (STACKPANEL_LEFT_MARGIN * 2) - NAMEBOX_WIDTH - SCROLLWHEEL_WIDTH;

            cbxValue.Checked += Checked;
            cbxValue.Unchecked += Checked;

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
        private void UpdateValue()
        {
            Value = (bool)cbxValue.IsChecked;
            UpdateGraphics();
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            UpdateValue();
        }

        //Events
    }
}
