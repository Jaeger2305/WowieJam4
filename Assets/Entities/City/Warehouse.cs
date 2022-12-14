using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Warehouse : MonoBehaviour
{
    public UnityEvent suppliesEmpty;
    public UnityEvent sufficientBeetles;
    public UnityEvent<string> outstandingBeetles;
    [SerializeField] private int _supplies = 100;
    [SerializeField] private int _maxSupplies = 100;
    [SerializeField] private int _requiredBeetles = 5;
    private WarehouseUI ui;

    void Start()
    {
        ui = GetComponent<WarehouseUI>();
        ui.SetSupplies(_supplies);
    }

    public int supplies
    {
        get { return _supplies; }
        private set { _supplies = value; }
    }
    [SerializeField] private int _supplyConsumption = 10;

    public void ConfigureWarehouse(int supplies, int maxSupplies, int consumptionRate, int requiredBeetles)
    {
        _supplies = supplies;
        _maxSupplies = maxSupplies;
        _supplyConsumption = consumptionRate;
        _requiredBeetles = requiredBeetles;
        outstandingBeetles.Invoke($"{_requiredBeetles}"); // event this as text so it's super easy to hook some UI into this.
    }

    /** Consume the standard amount from the class' config- useful for consuming overtime on a game tick. */
    public void ConsumeSupplies() { ConsumeSupplies(_supplyConsumption); }
    
    /** Consume a specific amount - useful for triggering on a specific event */
    public void ConsumeSupplies(int amount)
    {
        int before = _supplies;
        _supplies = System.Math.Clamp(_supplies -amount, 0, _maxSupplies);
        if (_supplies <= 0 && _supplies != before) suppliesEmpty.Invoke();
        ui.SetSupplies(_supplies);
    }

    public void DeliverSupplies(int amount)
    {
        _supplies = System.Math.Clamp(_supplies + amount, 0, _maxSupplies);
        ui.SetSupplies(_supplies);

        if (_requiredBeetles > 0)
        {
            _requiredBeetles -= 1;
            if (_requiredBeetles == 0) sufficientBeetles.Invoke();

            outstandingBeetles.Invoke($"{_requiredBeetles}"); // event this as text so it's super easy to hook some UI into this.
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.TryGetComponent(out Inventory inventory);
        if (inventory == null) return;

        //Prevent player from turning in scrap/supplies
        //  Added to keep player reliant on collaborating with AI to win!
        inventory.TryGetComponent(out EntityMetadata eData);
        if (eData.entityType == EntityType.Player) return;

        DeliverSupplies(inventory.EmptyInventory());
        if (eData.entityType == EntityType.AlliedRobot) Destroy(collider.gameObject); // Probably we want to keep the robots, but this is simpelr for now.
        //Changed this from comparetag since I'd already grabbed EnityMetadata
    }
}
