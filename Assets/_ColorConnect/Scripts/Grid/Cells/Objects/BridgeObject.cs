using UnityEngine;

public class BridgeObject : _CellObject
{
    public enum Direction { Vertical, Horizontal }
    public Direction direction;

    public override bool OnLineEnter(LineManager line)
    {
        bool canContinueLine;

        line.EnterBridge(this);

        canContinueLine = false;
        return canContinueLine;
    }
}
