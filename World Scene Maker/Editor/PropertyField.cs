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
    class PropertyField<ValueType> : StackPanel, IPropertyField<ValueType> where ValueType : IConvertible
    {
        //Constants
        protected const double NAMEBOX_WIDTH = 75;
        protected const double STACKPANEL_LEFT_MARGIN = 10;
        protected const double STACKPANEL_TOP_MARGING = 2;

        //Variables
        private OnValueChange<ValueType> onValueChange;

        private TextBlock tbcName = new TextBlock();

        private string strName;
        private ValueType objValue;

        //Constructor
        public PropertyField(string pName, ValueType pValue, double pWidth)
        {
            Name = pName;
            Value = pValue;

            //this
            this.Orientation = Orientation.Horizontal;
            this.Focusable = false;
            this.Margin = new Thickness(0, STACKPANEL_TOP_MARGING, 0, STACKPANEL_TOP_MARGING);
            this.ToolTip = " ValueType : " + typeof(ValueType).Name;
            if (typeof(ValueType).IsEnum)
            {
                foreach (ValueType pEnum in Enum.GetValues(typeof(ValueType)))
                {
                    this.ToolTip += "\n   " + pEnum;
                }
            }

            //namebox
            tbcName.Width = NAMEBOX_WIDTH;
            tbcName.Margin = new Thickness(STACKPANEL_LEFT_MARGIN, 0, 0, 0);
            this.Children.Add(tbcName);

            //valuebox
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

        //Methods

        //Events
        public event OnValueChange<ValueType> ValueChanged
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
