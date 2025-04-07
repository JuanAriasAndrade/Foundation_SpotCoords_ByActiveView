#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Media;
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
                Face topFace = ObtenerCaraSuperior(pilote,doc);
                if (topFace == null)
                    throw new Exception("No se encontró la cara superior del pilote.");

                Element piloteElem = doc.GetElement(pilote);
                BoundingBoxXYZ bbox = piloteElem.get_BoundingBox(null);
                XYZ puntoSuperior = new XYZ((bbox.Min.X + bbox.Max.X) / 2, (bbox.Min.Y + bbox.Max.Y) / 2, bbox.Max.Z);

                Reference faceReference = topFace.Reference;
                if (faceReference == null)
                    throw new Exception("La cara superior no tiene una referencia válida para anotaciones.");

                XYZ punto = ObtenerCentroCara(topFace);
                if (topFace.Project(punto) == null)
                    throw new Exception("El punto no está dentro de la cara superior.");

                View view = doc.ActiveView;
                if (!(view is ViewPlan || view is ViewSection))
                    throw new Exception("Las Spot Dimensions solo pueden crearse en vistas de plano o sección.");

                CrearSpotDimension(doc, view, faceReference, punto);
            }
            catch (Exception e)
            {
                TaskDialog.Show("ERROR", e.Message);
               
            }
            return Result.Succeeded;
        }
        private Face ObtenerCaraSuperior(Reference refe, Document doc)
        {
            if (refe == null) return null;

            //convierte la referencia en elemento
            Element pilote = doc.GetElement(refe);

            // ¡IMPORTANTE! Activar ComputeReferences para obtener las referencias
            Options options = new Options { ComputeReferences = true };
            GeometryElement geometryElement = pilote.get_Geometry(options);

            //Obtener el solido de la geometría 
            foreach (GeometryObject geObj in geometryElement)
            {
                if (geObj is Solid solid && solid.Volume > 0)

                {
                    foreach (Face face in solid.Faces)
                    {
                        //Encontrar la Normal que paunta hacía arriba 
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
            using (Transaction tran = new Transaction(doc, "Crear Spot Dimension en Pilote"))
            {
                tran.Start();

                //Definir desplazamineto visual de la cota 
                XYZ offset = new XYZ(2, 2, 0);


                //Crear la Spot Dimension
                SpotDimension spotDim = doc.Create.NewSpotCoordinate(view, faceReference, point, point + offset, point + offset * 2, point + offset * 3, true);

                tran.Commit();

                return spotDim;
            }
        }
    }
}

