using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    private int _scrap = 100;
    public GameObject scrapPrefab;
    public UnityEvent scrapCollected;

    public void AddScrap(int amount) {
        scrapCollected.Invoke();
        _scrap += amount;
    }


    public void DropScrap()
    {
        GameObject scrapOnFloor = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        scrapOnFloor.GetComponent<Scrap>().ThrowOnFloor(_scrap);
        _scrap = 0;
    }
    public int EmptyInventory()
    {
        int startingScrap = _scrap;
        _scrap = 0;
        return startingScrap;
    }
}
