internal class Line
{
    private int[] pos0, pos1;

    public int[] Pos0 { get {  return pos0; } }

    public int[] Pos1 { get { return pos1; } }

    public Line(int[] _pos0, int[] _pos1)
    {
        pos0 = _pos0;
        pos1 = _pos1;
    }
}