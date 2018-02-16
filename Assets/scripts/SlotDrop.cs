using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlotDrop : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("on drop fired");
        if (!item)
        {
            Debug.Log("snap to empty card slot");
            DragCard.itemBeingDragged.transform.SetParent(transform);
            DragCard.itemBeingDragged.transform.position = transform.position;
        }
    }
    #endregion
}