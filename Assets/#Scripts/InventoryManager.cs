using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] List<InventorySlot> _allSlots = new List<InventorySlot>();
    int _currentItemCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddItemToInventory(WeaponInventoryObject weaponInventoryObject)
    {
        IsFull();

        HideAllSlots();

        MoveObjectsNextSlot();

        foreach (var slot in _allSlots)
        {
            if (slot._hasItem) continue;

            slot.AddItem(weaponInventoryObject);
            break;
        }

        _currentItemCount++;
    }

    public void AddItemToInventory(ShieldInventoryObject shieldInventoryObject)
    {
        IsFull();

        HideAllSlots();

        MoveObjectsNextSlot();

        foreach (var slot in _allSlots)
        {
            if (slot._hasItem) continue;

            slot.AddItem(shieldInventoryObject);
            break;
        }

        _currentItemCount++;
    }

    public void AddItemToInventory(ConsumableInventoryObject consumableInventoryObject)
    {
        IsFull();

        HideAllSlots();

        MoveObjectsNextSlot();

        foreach (var slot in _allSlots)
        {
            if (slot._hasItem) continue;

            slot.AddItem(consumableInventoryObject);
            break;
        }

        _currentItemCount++;
    }

    private void IsFull()
    {
        if (_currentItemCount == _allSlots.Count)
        {
            _allSlots[_allSlots.Count - 1].RemoveItem();
            _currentItemCount--;
        }
    }

    private void MoveObjectsNextSlot()
    {
        for (int i = _currentItemCount - 1; i >= 0; i--)
        {
            if (!_allSlots[i]._hasItem) continue;

            if (_allSlots[i]._isLocked) continue;

            var nextAvailableIndex = FindNextAvailableIndex(i + 1);

            switch (_allSlots[nextAvailableIndex]._currentItemType)
            {
                case InventorySlot.ItemType.Weapon:
                    _allSlots[nextAvailableIndex].AddItem(_allSlots[i]._weapon);
                    break;
                case InventorySlot.ItemType.Shield:
                    _allSlots[nextAvailableIndex].AddItem(_allSlots[i]._shield);
                    break;
                case InventorySlot.ItemType.Consumable:
                    _allSlots[nextAvailableIndex].AddItem(_allSlots[i]._consumable);
                    break;
                default:
                    break;
            }
        }
    }

    int FindNextAvailableIndex(int startIndex)
    {
        int nextAvailableIndex = -1;
        for (int i = startIndex; i < _allSlots.Count; i++)
        {
            if (!_allSlots[i]._hasItem && !_allSlots[i]._isLocked)
            {
                nextAvailableIndex = i;
                break;
            }
        }

        return nextAvailableIndex;
    }

    private void HideAllSlots()
    {
        foreach (var slot in _allSlots)
        {
            if (!slot._hasItem) continue;
            if (slot._isLocked) continue;
            slot.HideItem();
        }
    }

}
