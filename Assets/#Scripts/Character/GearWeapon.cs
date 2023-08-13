using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GearWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GearWeapon instance;

    public delegate void OnWeaponEquip();
    public OnWeaponEquip onWeaponEquip;

    [SerializeField] GameObject _gearInfo;
    [SerializeField] TMPro.TextMeshProUGUI _gearInfoText;
    [SerializeField] Image _weaponGearImage;

    [SerializeField] float _defaultMinDamage, _defaultMaxDamage;
    WeaponInventoryObject _currentWeapon;

    private void Awake()
    {
        instance = this;
    }

    public void EquipWeapon(WeaponInventoryObject weaponInventoryObject)
    {
        _currentWeapon = weaponInventoryObject;

        _weaponGearImage.sprite = _currentWeapon._sprite;
        _weaponGearImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentWeapon._width - 10, 90);

        _weaponGearImage.color = Color.white;

        onWeaponEquip?.Invoke();
    }

    public void UnequipWeapon()
    {
        _currentWeapon = null;
    }

    public float GetMinAttack()
    {
        if (_currentWeapon == null) return _defaultMinDamage;

        return _currentWeapon._minDamage;
    }

    public float GetMaxAttack()
    {
        if (_currentWeapon == null) return _defaultMaxDamage;

        return _currentWeapon._maxDamage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_currentWeapon == null) {
            _gearInfoText.text = "ATK: " + _defaultMinDamage.ToString() + "-" + _defaultMaxDamage.ToString();
            _gearInfo.SetActive(true);
            return;
        }


        _gearInfoText.text = "ATK: " + _currentWeapon._minDamage.ToString() + "-" + _currentWeapon._maxDamage.ToString();
        _gearInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gearInfo.SetActive(false);
    }
}
