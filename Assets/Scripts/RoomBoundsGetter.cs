using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoundsGetter : MonoBehaviour
{
    public void GetRoomBounds()
    {
        if (OVRManager.boundary.GetConfigured())
        {
            Vector3[] boundaryPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
            ExportBoundaryToSVG(boundaryPoints);
        }
    }

    void ExportBoundaryToSVG(Vector3[] points)
    {
        // First, convert the 3D points to 2D points
        List<Vector2> points2D = new List<Vector2>();
        foreach (Vector3 point in points)
        {
            points2D.Add(new Vector2(point.x, point.z));
        }

        // Then, create an SVG string
        string svg = "<svg width=\"100%\" height=\"100%\" viewBox=\"0 0 100 100\" xmlns=\"http://www.w3.org/2000/svg\">";
        svg += "<polygon points=\"";
        foreach (Vector2 point in points2D)
        {
            svg += point.x + "," + point.y + " ";
        }
        svg += "\"/>";
        svg += "</svg>";

        // Finally, save the SVG string to a file
        System.IO.File.WriteAllText("boundary.svg", svg);

        // Now open the file in default application
        System.Diagnostics.Process.Start("boundary.svg");
    }

}
