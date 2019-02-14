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
    class PropertyField<ValueType> : StackPanel, IPropertyEditorSearchable where ValueType : IConvertible
    {
        //Constants
        private const double NAMEBOX_WIDTH = 75;
        private const double STACKPANEL_LEFT_MARGIN = 10;
        private const double STACKPANEL_TOP_MARGING = 2;

        //Variables
        public delegate void OnValueChange(PropertyField<ValueType> pPropertyField);
        private OnValueChange onValueChange;

        private TextBlock tbcName = new TextBlock();
        private TextBox tbxValue = new TextBox();

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
            tbxValue.Width = pWidth - (STACKPANEL_LEFT_MARGIN * 2) - NAMEBOX_WIDTH;
            tbxValue.KeyUp += EnterStopEdit;
            tbxValue.LostFocus += LoseFocus;
            this.Children.Add(tbxValue);
        }

        public PropertyField(string pName, ValueType pValue, double pWidth, string pTooltip) : this(pName, pValue, pWidth)
        {
            tbcName.ToolTip = pTooltip;
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

        private TextBlock NameBox
        {
            get
            {
                return tbcName;
            }
        }
        private TextBox ValueBox
        {
            get
            {
                return tbxValue;
            }
        }

        //Function
        private void UpdateGraphics()
        {
            tbcName.Text = strName;
            if (objValue != null)
            {
                tbxValue.Text = objValue.ToString();
            }
        }

        private void LoseFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ValueType pValue;
                if (typeof(ValueType).IsEnum)
                {
                    pValue = (ValueType)Enum.Parse(typeof(ValueType), tbxValue.Text);
                }
                else
                {
                    pValue = (ValueType)Convert.ChangeType(tbxValue.Text, typeof(ValueType));
                }

                Value = pValue;
            }
            catch
            {
                tbxValue.SelectionStart = tbxValue.Text.Length;
                tbxValue.SelectionLength = 0;
            }
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
        public event OnValueChange ValueChanged
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
