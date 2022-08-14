using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public int Scrap { get; private set; } = 100;
    public GameObject scrapPrefab;
    public UnityEvent scrapCollected;
    public UnityEvent scrapSpent;

    public void AddScrap(int amount) {
        scrapCollected.Invoke();
        Scrap += amount;
    }

    public void SpendScrap(int amount)
    {
        scrapSpent.Invoke();
        Scrap -= amount;
    }

    public void DropScrap()
    {
        GameObject scrapOnFloor = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        scrapOnFloor.GetComponent<Scrap>().ThrowOnFloor(Scrap);
        Scrap = 0;
    }

    public int EmptyInventory()
    {
        int startingScrap = Scrap;
        Scrap = 0;
        return startingScrap;
    }
}
