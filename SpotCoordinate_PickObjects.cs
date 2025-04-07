#region Namespaces
using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace PlanoPilotes
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class SpotCoordinate_PickObject : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                Reference pilote = uidoc.Selection.PickObject(ObjectType.Element);

                if (pilote != null)
                {
                    TaskDialog.Show("SELECCIÓN", "Ha seleccionado el elemento: " + pilote.ElementId.ToString());
                }

                Face topFace = SpotDimensionHelper.ObtenerCaraSuperior(pilote, doc);
                if (topFace == null)
                    throw new Exception("No se encontró la cara superior del pilote.");

                XYZ punto = SpotDimensionHelper.ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null)
                    throw new Exception("El punto no está dentro de la cara superior.");

                View view = doc.ActiveView;
                if (!(view is ViewPlan || view is ViewSection))
                    throw new Exception("Las Spot Dimensions solo pueden crearse en vistas de plano o sección.");

                Reference faceReference = topFace.Reference;
                if (faceReference == null)
                    throw new Exception("La cara superior no tiene una referencia válida para anotaciones.");

                SpotDimensionHelper.CrearSpotDimension(doc, view, faceReference, punto);
            }
            catch (Exception e)
            {
                TaskDialog.Show("ERROR", e.Message);
            }

            return Result.Succeeded;
        }
    }
}