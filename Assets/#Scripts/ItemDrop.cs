using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] List<WeaponInventoryObject> _allWeapons = new List<WeaponInventoryObject>();
    [SerializeField] List<ShieldInventoryObject> _allShields = new List<ShieldInventoryObject>();
    [SerializeField] List<ConsumableInventoryObject> _allConsumables = new List<ConsumableInventoryObject>();

    private void Awake()
    {
        _allWeapons = Resources.LoadAll<WeaponInventoryObject>("Weapons").ToList();
        _allShields = Resources.LoadAll<ShieldInventoryObject>("Shields").ToList();
        _allConsumables = Resources.LoadAll<ConsumableInventoryObject>("Consumables").ToList();
    }

    private void OnEnable()
    {
        BattleManager.onBattleEnd += DropRandomItem;
    }

    private void OnDisable()
    {
        BattleManager.onBattleEnd -= DropRandomItem;
    }

    private void DropRandomItem()
    {
        var randIndex = UnityEngine.Random.Range(0, 3);

        switch (randIndex)
        {
            case 0:
                var randWeapon = UnityEngine.Random.Range(0, _allWeapons.Count);
                InventoryManager.instance.AddItemToInventory(_allWeapons[randWeapon]);
                break;
            case 1:
                var randShield = UnityEngine.Random.Range(0, _allShields.Count);
                InventoryManager.instance.AddItemToInventory(_allShields[randShield]);
                break;
            case 2:
                var randConsumable = UnityEngine.Random.Range(0, _allConsumables.Count);
                InventoryManager.instance.AddItemToInventory(_allConsumables[randConsumable]);
                break;
            default:
                break;
        }
    }
}
