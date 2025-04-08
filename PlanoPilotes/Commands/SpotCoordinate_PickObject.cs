using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System;
using Autodesk.Revit.Attributes;
using PlanoPilotes.Helpers;
using PlanoPilotes.UI;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class SpotCoordinate_PickObject : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uidoc = commandData.Application.ActiveUIDocument;
        Document doc = uidoc.Document;

        // Lista de referencias a procesar
        IList<Reference> referenciasSeleccionadas = new List<Reference>();

        try
        {
            // Mostrar el formulario y capturar la opción
            Form1 form = new Form1();
            form.ShowDialog();

            OpcionSeleccion opcion = form.OpcionElegida;

            if (opcion == OpcionSeleccion.Ninguna)
            {
                TaskDialog.Show("Info", "No se seleccionó ninguna opción.");
                return Result.Cancelled;
            }

            // Obtener referencias según la opción seleccionada
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
                            catch
                            {
                                // evitar elementos mal referenciados
                                continue;
                            }
                        }
                    }
                    break;

            }

            if (referenciasSeleccionadas.Count == 0)
            {
                TaskDialog.Show("Info", "No se seleccionaron elementos.");
                return Result.Cancelled;
            }

            int errores = 0; // contador de elementos omitidos
            View view = doc.ActiveView;

            // Validar tipo de vista
            if (!(view is ViewPlan || view is ViewSection))
            {
                TaskDialog.Show("Error", "Las Spot Dimensions solo pueden crearse en vistas de plano o sección.");
                return Result.Failed;
            }

            // Procesar cada referencia
            foreach (Reference piloteRef in referenciasSeleccionadas)
            {
                if (piloteRef == null) continue;

                Face topFace = SpotDimensionHelper.ObtenerCaraSuperior(piloteRef, doc);
                if (topFace == null)
                {
                    errores++;
                    continue;
                }

                XYZ punto = SpotDimensionHelper.ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null)
                {
                    errores++;
                    continue;
                }

                Reference faceReference = topFace.Reference;
                if (faceReference == null)
                {
                    errores++;
                    continue;
                }

                // Crear spot
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
