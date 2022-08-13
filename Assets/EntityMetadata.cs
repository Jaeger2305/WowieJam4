using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    EnemyRobot,
    AlliedRobot,
    Player,
}

public class EntityMetadata : MonoBehaviour
{
    [SerializeField] private EntityType _entityType;
    public EntityType entityType { get { return _entityType; } private set { _entityType = value; } }

    [SerializeField] private string _label;
    public string label { get { return _label; } private set { _label = value; } }
}
