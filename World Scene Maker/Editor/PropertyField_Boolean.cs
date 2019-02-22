using System.Windows;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyField_Boolean : PropertyField<bool>, IPropertyField<bool>
    {
        //Constants

        //Variables
        private CheckBox cbxValue = new CheckBox();

        //Constructor
        public PropertyField_Boolean(string pName, bool pValue, double pWidth) : base(pName, pValue, pWidth)
        {
            //valuebox
            cbxValue.Checked += Checked;
            cbxValue.Unchecked += Checked;
            cbxValue.KeyDown += PressedEnter;

            this.Children.Add(cbxValue);
        }

        //Properties

        //Functions
        public override void UpdateGraphics()
        {
            base.UpdateGraphics();
            cbxValue.IsChecked = (bool)Value;
        }

        public override void SetWidth(double pWidth)
        {
            cbxValue.Width = CalculateValueBoxWidth(pWidth);
            base.SetWidth(pWidth);
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

        private void PressedEnter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                cbxValue.IsChecked = !Value;
            }
        }

        //Events
    }
}
