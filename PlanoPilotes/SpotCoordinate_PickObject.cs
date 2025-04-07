using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using PlanoPilotes;
using System.Collections.Generic;
using System;
using Autodesk.Revit.Attributes;


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
            // Opción para seleccionar uno o varios elementos
            IList<Reference> referenciasSeleccionadas;
            try
            {
                // Intentar selección múltiple
                referenciasSeleccionadas = uidoc.Selection.PickObjects(ObjectType.Element, "Selecciona uno o varios pilotes");
            }
            catch
            {
                // Si no selecciona múltiples, seleccionar solo uno
                Reference r = uidoc.Selection.PickObject(ObjectType.Element, "Selecciona un pilote");
                referenciasSeleccionadas = new List<Reference> { r };
            }

            foreach (Reference piloteRef in referenciasSeleccionadas)
            {
                if (piloteRef == null) continue;

                Face topFace = SpotDimensionHelper.ObtenerCaraSuperior(piloteRef, doc);
                if (topFace == null)
                    throw new Exception("No se encontró la cara superior del pilote.");

                XYZ punto = SpotDimensionHelper.ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null)
                    throw new Exception("El punto no está dentro de la cara superior.");

                Reference faceReference = topFace.Reference;
                if (faceReference == null)
                    throw new Exception("La cara superior no tiene una referencia válida para anotaciones.");

                View view = doc.ActiveView;
                if (!(view is ViewPlan || view is ViewSection))
                    throw new Exception("Las Spot Dimensions solo pueden crearse en vistas de plano o sección.");

                SpotDimensionHelper.CrearSpotDimension(doc, view, faceReference, punto);
            }
        }
        catch (Exception e)
        {
            TaskDialog.Show("ERROR", e.Message);
        }

        return Result.Succeeded;
    }
}
