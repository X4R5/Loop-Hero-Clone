using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearShield : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GearShield instance;

    public delegate void OnShieldEquip();
    public OnShieldEquip onShieldEquip;

    [SerializeField] GameObject _gearInfo;
    [SerializeField] TMPro.TextMeshProUGUI _gearInfoText;
    [SerializeField] Image _shieldGearImage;

    [SerializeField] float _defaultMinDef, _defaultMaxDef;
    ShieldInventoryObject _currentShield;

    private void Awake()
    {
        instance = this;
    }

    public void EquipShield(ShieldInventoryObject shieldInventoryObject)
    {
        _currentShield = shieldInventoryObject;
        _shieldGearImage.sprite = _currentShield._sprite;
        _shieldGearImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentShield._width - 10, 90);

        _shieldGearImage.color = Color.white;

        onShieldEquip?.Invoke();
    }

    public void UnequipWeapon()
    {
        _currentShield = null;
    }

    public float GetMinDef()
    {
        if (_currentShield == null) return _defaultMinDef;

        return _currentShield._minDefense;
    }

    public float GetMaxDef()
    {
        if (_currentShield == null) return _defaultMaxDef;

        return _currentShield._maxDefense;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_currentShield == null)
        {
            _gearInfoText.text = "DEF: " + _defaultMinDef.ToString() + "-" + _defaultMaxDef.ToString();
            _gearInfo.SetActive(true);
            return;
        }


        _gearInfoText.text = "DEF: " + _currentShield._minDefense.ToString() + "-" + _currentShield._maxDefense.ToString();
        _gearInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gearInfo.SetActive(false);
    }
}
