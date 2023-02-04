using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCanvasItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);/// transform.lossyScale.x;
    }


    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);/// transform.lossyScale.x;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        return;
    }
}
