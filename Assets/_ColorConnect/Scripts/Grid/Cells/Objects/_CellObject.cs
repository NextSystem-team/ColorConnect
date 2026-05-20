using UnityEngine;

public abstract class _CellObject : MonoBehaviour
{
    public CellData cell;

    public virtual bool OnLineEnter(LineManager line)
    {
        bool canContinueLine;


        canContinueLine = true;
        return canContinueLine;
    }
}
