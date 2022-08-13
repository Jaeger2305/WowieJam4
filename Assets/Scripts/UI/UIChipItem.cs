using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIChipItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform OriginalParent { get; private set; }

    Canvas _parentCanvas;
    CanvasGroup _canvasGroup;

    RectTransform _rect;

    Vector2 _basePosition;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();

        //Get canvas reference for handling canvas scaling
        _parentCanvas = GetComponentInParent<Canvas>();

        //Get canvas group reference for handling interaction blocking
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _basePosition = _rect.anchoredPosition;
        OriginalParent = _rect.parent.GetComponent<RectTransform>();
        _rect.SetParent(_parentCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        //Reset pos here if not dropped on slot
        if (_rect.parent == _parentCanvas.transform) {
            _rect.SetParent(OriginalParent);
            _rect.anchoredPosition = _basePosition;
        }
    }
}
