using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Generic Component used to allow for hoverable UI text.
 */
public class ShowTextOnHoverComponent : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public CanvasGroup TextGroup;


    public void OnPointerEnter(PointerEventData eventData)
    {
        TextGroup.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextGroup.alpha = 0;
    }
}
