using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scrap : MonoBehaviour
{
    public UnityEvent scrapCollected;
    private int _value;
    private bool disabled;
    [SerializeField] private float _throwForce = 25f;

    public void ThrowOnFloor(int value)
    {
        _value = value;
        disabled = true;
        Invoke("DelayedPickup", 2f); // We don't want a dead beetle's scrap to instantly be picked up by the same beetle, so add a delay.
        float randomAngle = Random.Range(0f, 6.28319f);
        float randomAngle2 = Random.Range(0f, 6.28319f);
        GetComponent<Rigidbody2D>().AddForce(_throwForce * new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle2)));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (disabled) return;
        collider.TryGetComponent(out Inventory inventory);
        if (inventory == null) return;

        inventory.AddScrap(_value);
        Destroy(gameObject);
    }

    private void DelayedPickup()
    {
        disabled = false;
    }
}
