/* ## 📜 Licencia
*Este script está disponible solo para fines educativos, de investigación o de experimentación personal.*
*Su uso con fines comerciales está estrictamente prohibido bajo los términos de la licencia CC BY-NC 4.0.* */

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using PlanoPilotes.Helpers;
using PlanoPilotes.UI;
using System.Collections.Generic;
using System;


[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class SpotCoordinate_PickObject : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uidoc = commandData.Application.ActiveUIDocument;
        Document doc = uidoc.Document;

        IList<Reference> referenciasSeleccionadas = new List<Reference>();

        try
        {
            Form1 form = new Form1();
            form.ShowDialog();

            OpcionSeleccion opcion = form.OpcionElegida;

            if (opcion == OpcionSeleccion.Ninguna)
            {
                TaskDialog.Show("Info", "No se seleccionó ninguna opción.");
                return Result.Cancelled;
            }

            switch (opcion)
            {
                case OpcionSeleccion.Individual:
                    Reference r = uidoc.Selection.PickObject(ObjectType.Element, "Selecciona un pilote");
                    if (r != null) referenciasSeleccionadas.Add(r);
                    break;

                case OpcionSeleccion.Multiple:
                    referenciasSeleccionadas = uidoc.Selection.PickObjects(ObjectType.Element, "Selecciona uno o varios pilotes");
                    break;

                case OpcionSeleccion.VistaActual:
                    FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id)
                        .WhereElementIsNotElementType()
                        .OfClass(typeof(FamilyInstance));

                    foreach (Element el in collector)
                    {
                        if (el is FamilyInstance inst)
                        {
                            try
                            {
                                Reference reference = new Reference(inst);
                                if (reference != null)
                                    referenciasSeleccionadas.Add(reference);
                            }
                            catch { continue; }
                        }
                    }
                    break;
            }

            if (referenciasSeleccionadas.Count == 0)
            {
                TaskDialog.Show("Info", "No se seleccionaron elementos.");
                return Result.Cancelled;
            }

            int errores = 0;
            View view = doc.ActiveView;

            if (!(view is ViewPlan || view is ViewSection))
            {
                TaskDialog.Show("Error", "Las Spot Dimensions solo pueden crearse en vistas de plano o sección.");
                return Result.Failed;
            }

            foreach (Reference piloteRef in referenciasSeleccionadas)
            {
                if (piloteRef == null) continue;

                Face topFace = SpotDimensionHelper.ObtenerCaraSuperior(piloteRef, doc);
                if (topFace == null) { errores++; continue; }

                XYZ punto = SpotDimensionHelper.ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null) { errores++; continue; }

                Reference faceReference = topFace.Reference;
                if (faceReference == null) { errores++; continue; }

                SpotDimensionHelper.CrearSpotDimension(doc, view, faceReference, punto);
            }

            if (errores > 0)
            {
                TaskDialog.Show("Proceso Completado", $"Se omitieron {errores} pilotes sin cara superior válida.");
            }
        }
        catch (Exception e)
        {
            TaskDialog.Show("ERROR", e.Message);
            return Result.Failed;
        }

        return Result.Succeeded;
    }
}

