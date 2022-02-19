using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI2._4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var ductsList = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            var ductFirstLevel = new List<Duct>();
            var ductSecondLevel = new List<Duct>();
            var ductThirdLevel = new List<Duct>();
            var ductRoofLevel = new List<Duct>();

            string Level = string.Empty;
            foreach (Duct duct in ductsList)
            {
                Parameter DuctLevel = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM);
                string L = DuctLevel.AsValueString().ToString();
                if (L == "Level 1")
                {
                    ductFirstLevel.Add(duct);
                }
                if (L == "Level 2")
                {
                    ductSecondLevel.Add(duct);
                }
                if (L == "Level 3")
                {
                    ductThirdLevel.Add(duct);
                }
                if (L == "Roof Level")
                {
                    ductRoofLevel.Add(duct);
                }
            }

            Level += $"Количество на 1 этаже: {ductFirstLevel.Count}{Environment.NewLine}Количество на 2 этаже: {ductSecondLevel.Count}{Environment.NewLine}" +
                $"Количество на 3 этаже: {ductThirdLevel.Count}{Environment.NewLine}Количество на крыше: {ductRoofLevel.Count}{Environment.NewLine}";

            Level += $"Общее количество воздуховодов: {ductsList.Count}"; 

            TaskDialog.Show("Duct count", Level);

            return Result.Succeeded;
        }
    }
}
