using System;
using System.Windows;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyField_DateTime : PropertyField<DateTime>, IPropertyField<DateTime>
    {
        //Constants

        //Variables
        private DatePicker dpcValue = new DatePicker();

        //Constructor
        public PropertyField_DateTime(string pName, DateTime pValue, double pWidth) : base(pName, pValue, pWidth)
        {
            //valuebox
            dpcValue.Width = pWidth - (STACKPANEL_LEFT_MARGIN * 2) - NAMEBOX_WIDTH;

            dpcValue.SelectedDateChanged += DateSelected;

            this.Children.Add(dpcValue);
        }

        //Properties

        //Functions
        public override void UpdateGraphics()
        {
            base.UpdateGraphics();
            dpcValue.SelectedDate = Value;
        }

        public override void SetWidth(double pWidth)
        {
            dpcValue.Width = CalculateValueBoxWidth(pWidth);
            base.SetWidth(pWidth);
        }

        //Methods
        private void UpdateValue()
        {
            Value = (DateTime)dpcValue.SelectedDate;
            UpdateGraphics();
        }

        private void DateSelected(object sender, RoutedEventArgs e)
        {
            UpdateValue();
        }

        //Events
    }
}
