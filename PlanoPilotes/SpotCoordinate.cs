#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace PlanoPilotes
{
  [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
  public class SpotCoordinate : IExternalCommand
  {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {

                var pilote = ObtenerPilote(doc);
                if (pilote == null)
                    throw new Exception("No se encontr� un pilote con el nombre � 0.60");

                Face topFace = ObtenerCaraSuperior(pilote);
                if (topFace == null)
                    throw new Exception("No se encontr� la cara superior del pilote.");

                BoundingBoxXYZ bbox = pilote.get_BoundingBox(null);
                XYZ puntoSuperior = new XYZ((bbox.Min.X + bbox.Max.X) / 2, (bbox.Min.Y + bbox.Max.Y) / 2, bbox.Max.Z);

                Reference faceReference = topFace.Reference;
                if (faceReference == null)
                    throw new Exception("La cara superior no tiene una referencia v�lida para anotaciones.");

                XYZ punto = ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null)
                    throw new Exception("El punto no est� dentro de la cara superior.");

                View view = doc.ActiveView;
                if (!(view is ViewPlan || view is ViewSection))
                    throw new Exception("Las Spot Dimensions solo pueden crearse en vistas de plano o secci�n.");

                CrearSpotDimension(doc, view, faceReference, punto);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
            return Result.Succeeded;
        }


        private FamilyInstance ObtenerPilote(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFoundation)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>()
                .FirstOrDefault(x => x.Name == "0.60");
        }
        private  Face ObtenerCaraSuperior(FamilyInstance pilote)
        {
            if (pilote == null) return null;

            // �IMPORTANTE! Activar ComputeReferences para obtener las referencias
            Options options = new Options { ComputeReferences = true };
            GeometryElement geometryElement = pilote.get_Geometry(options);

            //Obtener el solido de la geometr�a 
            foreach (GeometryObject geObj in geometryElement)
            {
                if (geObj is Solid solid && solid.Volume > 0)

                {
                    foreach (Face face in solid.Faces)
                    {
                        //Encontrar la Normal que paunta hac�a arriba 
                        XYZ normal = face.ComputeNormal(new UV(0.5, 0.5)); // obtiene la normal en un punto medio de la cara

                        if (Math.Round(normal.Z, 2) == 1)//filtra la cara con normal (0,0,1) (apuntando hacia arriba
                        {
                            return face;
                        }
                    }
                }
            }
            return null;

        }

        private XYZ ObtenerCentroCara(Face topFace)
        {

            BoundingBoxUV bbox = topFace.GetBoundingBox();
            UV centroUV = (bbox.Min + bbox.Max) / 2;
            XYZ centroXYZ = topFace.Evaluate(centroUV);
            return centroXYZ;
        }

        private SpotDimension CrearSpotDimension(Document doc, View view, Reference faceReference, XYZ point)
        {
            using (Transaction tran = new Transaction(doc,"Crear Spot Dimension en Pilote"))
            {
                tran.Start();

                //Definir desplazamineto visual de la cota 
                XYZ offset = new XYZ(2,2,0);
                

                //Crear la Spot Dimension
                SpotDimension spotDim = doc.Create.NewSpotCoordinate(view, faceReference, point, point + offset, point + offset * 2, point + offset * 3, true);

                tran.Commit();

                return spotDim;
            }
        }
  }
}
