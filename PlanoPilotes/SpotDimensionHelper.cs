﻿using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PlanoPilotes
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
                        XYZ normal = face.ComputeNormal(new UV(0.5, 0.5));
                        if (Math.Round(normal.Z, 2) == 1)
                        {
                            return face;
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
