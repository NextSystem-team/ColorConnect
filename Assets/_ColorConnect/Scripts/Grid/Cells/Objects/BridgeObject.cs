using UnityEngine;

public class BridgeObject : _CellObject
{
    enum Direction { Vertical, Horizontal }
    [SerializeField] Direction direction;

    public override bool OnLineEnter(LineManager line)
    {
        return false;
    }
}
