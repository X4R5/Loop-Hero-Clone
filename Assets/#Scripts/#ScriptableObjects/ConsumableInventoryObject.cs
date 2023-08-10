using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory Object/Consumable")]
public class ConsumableInventoryObject : ScriptableObject
{
    public Sprite _sprite;
    public int _health, _attackSpeedPercentage;
    public int _width = 50;
}
