using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    public WeaponInventoryObject _weapon;
    public ShieldInventoryObject _shield;
    public ConsumableInventoryObject _consumable;

    public ItemType _currentItemType;
    float _width = 50;

    public enum ItemType
    {
        Weapon,
        Shield,
        Consumable
    }

    Image _slotImage;

    [SerializeField] Image _lockedImage;

    public bool _hasItem = false, _isLocked = false;

    private void Awake()
    {
        _slotImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (!_hasItem) return;

            if (_isLocked)
            {
                _lockedImage.gameObject.SetActive(false);
                _isLocked = false;
            }
            else
            {
                _lockedImage.gameObject.SetActive(true);
                _isLocked = true;
            }
        }

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (!_hasItem) return;

            if (_isLocked)
            {
                _lockedImage.gameObject.SetActive(false);
                _isLocked = false;
            }

            if (_currentItemType == ItemType.Weapon)
            {
                GearWeapon.instance.EquipWeapon(_weapon);
                RemoveItem();
            }
            else if (_currentItemType == ItemType.Shield)
            {
                GearShield.instance.EquipShield(_shield);
                RemoveItem();
            }
            else if (_currentItemType == ItemType.Consumable)
            {
                GearPotion.instance.EquipPotion(_consumable);
                RemoveItem();
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse over");
    }

    public void AddItem(WeaponInventoryObject weaponInventoryObject)
    {
        _slotImage.gameObject.SetActive(true);

        _weapon = weaponInventoryObject;
        _hasItem = true;
        _currentItemType = ItemType.Weapon;
        _slotImage.sprite = _weapon._sprite;

        _width = _weapon._width;

        _shield = null;
        _consumable = null;

        SetSlotWidth();
    }

    public void AddItem(ShieldInventoryObject shieldInventoryObject)
    {
        _slotImage.gameObject.SetActive(true);

        _shield = shieldInventoryObject;
        _hasItem = true;
        _currentItemType = ItemType.Shield;
        _slotImage.sprite = _shield._sprite;

        _width = _shield._width;

        _weapon = null;
        _consumable = null;

        SetSlotWidth();
    }

    public void AddItem(ConsumableInventoryObject consumableInventoryObject)
    {
        if(_slotImage.gameObject.activeSelf) HideItem();

        _slotImage.gameObject.SetActive(true);

        _consumable = consumableInventoryObject;
        _hasItem = true;
        _currentItemType = ItemType.Consumable;
        _slotImage.sprite = _consumable._sprite;

        _width = consumableInventoryObject._width;

        _weapon = null;
        _shield = null;

        SetSlotWidth();
    }

    public void RemoveItem()
    {
        _weapon = null;
        _shield = null;
        _consumable = null;
        _hasItem = false;
        _isLocked = false;
        _slotImage.sprite = null;
        _slotImage.gameObject.SetActive(false);
    }

    public void HideItem()
    {
        _slotImage.gameObject.SetActive(false);
    }

    public void SetSlotWidth()
    {
        _slotImage.rectTransform.sizeDelta = new Vector2(_width, 100f);
    }
}
