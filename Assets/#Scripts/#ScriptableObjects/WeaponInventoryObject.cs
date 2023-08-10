using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory Object/Weapon")]
public class WeaponInventoryObject : ScriptableObject
{
    public Sprite _sprite;
    public int _minDamage, _maxDamage;
    public int _width = 50;
}
