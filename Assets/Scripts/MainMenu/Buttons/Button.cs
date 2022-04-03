using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float mouseOverScale = 1.2f;
    private RectTransform rectTransform;
    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.Scale(rectTransform.localScale, new Vector3(1/mouseOverScale, 1/mouseOverScale, 1));
        OnClicked();
    }

    protected virtual void OnClicked()
    {
        throw new System.Exception("OnClicked must be implemented in children");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.Scale(rectTransform.localScale, new Vector3(mouseOverScale, mouseOverScale, 1));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.Scale(rectTransform.localScale, new Vector3(1/mouseOverScale, 1/mouseOverScale, 1));
    }
}
