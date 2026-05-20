using UnityEngine;

public abstract class _CellObject : MonoBehaviour
{
    public virtual bool OnLineEnter(LineManager line)
    {
        return true;
    }
}
