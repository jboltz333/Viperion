using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<Image>().color = Color.cyan;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<Image>().color = new Color(0, 220f/255f, 1);
    }

}
