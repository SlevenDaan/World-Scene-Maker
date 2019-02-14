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
    class PropertyField<Type> : ListViewItem where Type : IConvertible
    {
        //Constants
        private const double NAMEBOX_WIDTH = 75;
        private const double STACKPANEL_MARGIN = 10;

        //Variables
        public delegate void OnValueChange(PropertyField<Type> pPropertyField);
        private OnValueChange onValueChange;

        private StackPanel stackPanel = new StackPanel();
        private TextBlock tbcName = new TextBlock();
        private TextBox tbxValue = new TextBox();

        private string strName;
        private Type objValue;
        
        //Constructor
        public PropertyField(string pName, Type pValue, double pWidth)
        {
            Name = pName;
            Value = pValue;

            //stackpanel
            stackPanel.Margin = new Thickness(STACKPANEL_MARGIN, 0, 0, 0);
            stackPanel.Orientation = Orientation.Horizontal;
            this.Content = stackPanel;
            this.IsTabStop = false;
            this.GotFocus += ItemGotFocus;

            this.ToolTip = "Type: " + typeof(Type).Name;
            if (typeof(Type).IsEnum)
            {
                foreach (Type pEnum in Enum.GetValues(typeof(Type)))
                {
                    this.ToolTip += "\n   " + pEnum;
                }
            }

            //namebox
            tbcName.Width = NAMEBOX_WIDTH;
            stackPanel.Children.Add(tbcName);

            //valuebox
            tbxValue.Width = pWidth - (STACKPANEL_MARGIN*3) - NAMEBOX_WIDTH;
            stackPanel.Children.Add(tbxValue);
            tbxValue.KeyUp += EnterStopEdit;
            tbxValue.LostFocus += LoseFocus;
        }

        private void ItemGotFocus(object sender, RoutedEventArgs e)
        {
            tbxValue.Focus();
        }

        public PropertyField(string pName, Type pValue, double pWidth, string pTooltip) : this(pName, pValue, pWidth)
        {
            tbcName.ToolTip = pTooltip;
        }

        private void LoseFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Type pValue;
                if (typeof(Type).IsEnum)
                {
                    pValue = (Type)Enum.Parse(typeof(Type), tbxValue.Text);
                }
                else
                {
                    pValue = (Type)Convert.ChangeType(tbxValue.Text, typeof(Type));
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
        public Type Value
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

        public TextBlock NameBox
        {
            get
            {
                return tbcName;
            }
        }
        public TextBox ValueBox
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
