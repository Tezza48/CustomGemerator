using UnityEngine;

internal class Line
{
    private Vector2 origin1, origin2;

    public Vector2 Origin1 { get {  return origin1; } }

    public Vector2 Origin2 { get { return origin2; } }

    public Vector3 Origin1v3 { get { return new Vector3(origin1.x, 0, origin1.y); } }

    public Vector3 Origin2v3 { get { return new Vector3(origin2.x, 0, origin2.y); } }

    public Line(Vector2 origin1, Vector2 origin2)
    {
        this.origin1 = origin1;
        this.origin2 = origin2;
    }
    public bool PointOnLine(int _x, int _y)
    {
        // return whether this line intersects the point#
        // make line segments for edges of the cell
        // see if it intersects any of the segments
        return false;
    }
}