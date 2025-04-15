 /* ## 📜 Licencia
*Este script está disponible solo para fines educativos, de investigación o de experimentación personal.*
*Su uso con fines comerciales está estrictamente prohibido bajo los términos de la licencia CC BY-NC 4.0.* */

using Autodesk.Revit.DB;
using System;
using Autodesk.Revit.UI;

namespace PlanoPilotes.Helpers
{
    public static class SpotDimensionHelper
    {
        public static Face ObtenerCaraSuperior(Reference refe, Document doc)
        {
            if (refe == null) return null;

            Element pilote = doc.GetElement(refe);
            Options options = new Options { ComputeReferences = true };
            GeometryElement geometryElement = pilote.get_Geometry(options);

            foreach (GeometryObject geObj in geometryElement)
            {
                if (geObj is Solid solid && solid.Volume > 0)
                {
                    foreach (Face face in solid.Faces)
                    {
                        if (face is PlanarFace planarFace)
                        {
                            XYZ normal = planarFace.FaceNormal.Normalize();
                            if (normal.IsAlmostEqualTo(XYZ.BasisZ, 0.01))
                                return planarFace;
                        }
                    }
                }
            }

            return null;
        }

        public static XYZ ObtenerCentroCara(Face topFace)
        {
            BoundingBoxUV bbox = topFace.GetBoundingBox();
            UV centroUV = (bbox.Min + bbox.Max) / 2;
            return topFace.Evaluate(centroUV);
        }

        public static SpotDimension CrearSpotDimension(Document doc, View view, Reference faceReference, XYZ point)
        {
            using (Transaction tran = new Transaction(doc, "Crear Spot Dimension en Pilote"))
            {
                tran.Start();
                XYZ offset = new XYZ(2, 2, 0);

                SpotDimension spotDim = doc.Create.NewSpotCoordinate(
                    view,
                    faceReference,
                    point,
                    point + offset,
                    point + offset * 2,
                    point + offset * 3,
                    true
                );

                tran.Commit();
                return spotDim;
            }
        }
    }
}

