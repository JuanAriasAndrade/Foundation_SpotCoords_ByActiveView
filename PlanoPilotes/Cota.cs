#region Namespaces
using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

[Transaction(TransactionMode.Manual)]
public class Cota : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uidoc = commandData.Application.ActiveUIDocument;
        Document doc = uidoc.Document;


        //Se debe validar la ubicación de la cota porque no esta llegando al centro 







        try
        {
            // Selección de un punto en un elemento
            Reference pickedRef = uidoc.Selection.PickObject(ObjectType.PointOnElement, "Seleccione un midpoint del elemento.");
            XYZ selectedPoint = pickedRef.GlobalPoint;

            using (Transaction tran = new Transaction(doc, "Crear Spot Coordinate"))
            {
                tran.Start();
                doc.Create.NewSpotCoordinate(doc.ActiveView, pickedRef, selectedPoint, selectedPoint + XYZ.BasisZ * 0.5, selectedPoint + XYZ.BasisZ, selectedPoint + XYZ.BasisZ * 1.5, true);
                tran.Commit();
            }

            TaskDialog.Show("Éxito", "Spot Coordinate creada en el midpoint seleccionado.");
            return Result.Succeeded;
        }
        catch (Autodesk.Revit.Exceptions.OperationCanceledException)
        {
            return Result.Cancelled;
        }
        catch (Exception e)
        {
            TaskDialog.Show("Error", e.Message);
            return Result.Failed;
        }
    }
}
