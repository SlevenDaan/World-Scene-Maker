using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_PropertyEditor
{
    interface IPropertyEditorSearchable
    {
        string Name
        {
            get;
        }
        Type Type
        {
            get;
        }
    }
}
