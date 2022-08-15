using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private PolygonCollider2D _collider;
    private Bounds _bounds;
    private Vector3 _center;

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _bounds = _collider.bounds;
        _center = _bounds.center;
    }

    public GameObject SpawnInCollider(GameObject objectToSpawn)
    {

        float x;
        float y;
        int attempt = 0;
        do
        {
            x = Random.Range(_center.x - _bounds.extents.x, _center.x + _bounds.extents.x);
            y = Random.Range(_center.y - _bounds.extents.y, _center.y + _bounds.extents.y);
            attempt++;
        } while (!_collider.OverlapPoint(new Vector2(x, y)) || attempt <= 100);

        return Instantiate(objectToSpawn, new Vector2(x, y), Quaternion.identity);
    }
}
