using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    GameObject GameObject { get; }
    void OnBeginDrag();
    void OnDrag();
    void OnEndDrag();
}
