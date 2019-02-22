using System;
using System.Windows;
using System.Windows.Controls;

namespace DS_PropertyEditor
{
    class PropertyField<ValueType> : StackPanel, IPropertyField<ValueType> where ValueType : IConvertible
    {
        //Constants
        public const double NAMEBOX_WIDTH = 75;
        public const double STACKPANEL_LEFT_MARGIN = 10;
        public const double STACKPANEL_TOP_MARGING = 2;
        public const double VALUEBOX_MIN_WIDTH = 50;

        //Variables
        private OnPropertyFieldValueChange<ValueType> onValueChange;

        private TextBlock tbcName = new TextBlock();

        private string strName;
        private ValueType objValue;

        //Constructor
        public PropertyField(string pName, ValueType pValue, double pWidth)
        {
            Name = pName;
            Value = pValue;

            //this
            this.SetWidth(pWidth);
            this.Orientation = Orientation.Horizontal;
            this.Focusable = false;
            this.ToolTip = " ValueType : " + typeof(ValueType).Name;

            //namebox
            this.Children.Add(tbcName);
        }
        
        //Properties
        public new string Name
        {
            get
            {
                return strName;
            }
            private set
            {
                strName = value;
                if (onValueChange != null)
                {
                    onValueChange(this);
                }
                UpdateGraphics();
            }
        }
        public ValueType Value
        {
            get
            {
                return objValue;
            }
            set
            {
                objValue = value;
                if (onValueChange != null)
                {
                    onValueChange(this);
                }
                UpdateGraphics();
            }
        }
        public Type Type
        {
            get
            {
                return typeof(ValueType);
            }
        }

        //Functions
        public virtual void UpdateGraphics()
        {
            tbcName.Text = strName.ToString();
        }

        public virtual void SetWidth(double pWidth)
        {
            this.Margin = new Thickness(0, STACKPANEL_TOP_MARGING, 0, STACKPANEL_TOP_MARGING);
            
            tbcName.Width = NAMEBOX_WIDTH;
            tbcName.Margin = new Thickness(STACKPANEL_LEFT_MARGIN, 0, 0, 0);
        }
        
        protected double CalculateValueBoxWidth(double pWidth)
        {
            pWidth = pWidth - (STACKPANEL_LEFT_MARGIN * 2) - NAMEBOX_WIDTH;
            if (pWidth < VALUEBOX_MIN_WIDTH)
            {
                pWidth = VALUEBOX_MIN_WIDTH;
            }
            return pWidth;
        }

        //Events
        public event OnPropertyFieldValueChange<ValueType> ValueChanged
        {
            add
            {
                onValueChange += value;
            }
            remove
            {
                onValueChange -= value;
            }
        }
    }
}
