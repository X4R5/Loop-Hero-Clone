using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory Object/Shield")]
public class ShieldInventoryObject : ScriptableObject
{
    public Sprite _sprite;
    public int _minDefense, _maxDefense;
    public int _width = 100;
}
