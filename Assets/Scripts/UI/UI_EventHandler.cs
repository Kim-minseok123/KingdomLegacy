using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
	public Action OnPointerDownHandler = null;
	public Action OnPointerUpHandler = null;
	public Action<GameObject> OnPointerEnterHandler = null;
	public Action OnPointerExitHandler = null;
	public Action<GameObject> OnDragHandler = null;
	public Action<GameObject> OnEndDragHandler = null;

	bool _pressed = false;

    private void Update()
	{
		if (_pressed)
			OnPressedHandler?.Invoke();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClickHandler?.Invoke();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
		OnPointerDownHandler?.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
		OnPointerUpHandler?.Invoke();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        OnPointerEnterHandler?.Invoke(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		OnPointerExitHandler?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragHandler?.Invoke(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		OnEndDragHandler?.Invoke(gameObject);
    }
}
