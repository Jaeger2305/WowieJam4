using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIActiveChipSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] RectTransform _chipParentRect;

    UIChipItem _currentChipItem;

    private void Awake()
    {
        _currentChipItem = GetComponentInChildren<UIChipItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) {
            RectTransform chip = eventData.pointerDrag.GetComponent<RectTransform>();
            
            //Handle current chip
            if (_currentChipItem != null) {
                _currentChipItem.GetComponent<RectTransform>().SetParent(chip.GetComponent<UIChipItem>().OriginalParent);
            }

            chip.SetParent(_chipParentRect);
            chip.anchoredPosition = Vector2.zero;
            _currentChipItem = chip.GetComponent<UIChipItem>();
        }

        
    }
}
