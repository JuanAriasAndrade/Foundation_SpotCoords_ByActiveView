using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]

public class CodigoPruebas: IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uidoc = commandData.Application.ActiveUIDocument;
        Document doc = uidoc.Document;

        try
        {


            var pilote = ObtenerPilote(doc);
            Face topFace = ObtenerCaraSuperior(pilote);
            // Solo muestra mensaje si es una cara cilíndrica
            
            
            if (topFace is PlanarFace)
            {

                BoundingBoxUV bbox = topFace.GetBoundingBox();
                UV midPoint = (bbox.Min + bbox.Max) / 2;
                XYZ puntoMedio = topFace.Evaluate(midPoint);

                TaskDialog.Show("Éxito", puntoMedio.ToString());
            }
            else
            {
                TaskDialog.Show("b", "b");

            }
        }
        catch (Exception e)
        {
            TaskDialog.Show("ERROR", e.Message);
        }

        return Result.Succeeded;
    }

    private FamilyInstance ObtenerPilote(Document doc)
    {
        return new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_StructuralColumns)
            .WhereElementIsNotElementType()
            .Cast<FamilyInstance>()
            .FirstOrDefault(x => x.Name == "( Ø 0.60 )");
    }

    private Face ObtenerCaraSuperior(FamilyInstance pilote)
    {
        if (pilote == null) return null;

        Options options = new Options();
        GeometryElement geometryElement = pilote.get_Geometry(options);

        foreach (GeometryObject geObj in geometryElement)
        {
            if (geObj is Solid solid && solid.Volume > 0)
            {
                foreach (Face face in solid.Faces)
                {
                    XYZ normal = face.ComputeNormal(new UV(0.5, 0.5));
                    if (Math.Round(normal.Z, 2) == 1) // Filtra caras con normal hacia arriba (0,0,1)
                    {
                        return face;
                    }
                }
            }
        }
        return null;
    }
}