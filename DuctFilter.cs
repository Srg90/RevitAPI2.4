using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI2._4
{
    public class DuctFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Duct;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
