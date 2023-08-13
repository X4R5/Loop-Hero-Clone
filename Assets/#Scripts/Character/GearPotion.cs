using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearPotion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GearPotion instance;

    [SerializeField] GameObject _gearInfo;
    [SerializeField] TMPro.TextMeshProUGUI _gearInfoText;
    [SerializeField] Image _potionGearImage;
    Color _defaultColor;

    public ConsumableInventoryObject _currentPotion;

    private void Awake()
    {
        instance = this;
        _defaultColor = _potionGearImage.color;
    }

    public void EquipPotion(ConsumableInventoryObject consumableInventoryObject)
    {
        _currentPotion = consumableInventoryObject;

        _potionGearImage.sprite = _currentPotion._sprite;

        _potionGearImage.color = Color.white;

        _potionGearImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentPotion._width - 10, 90);
    }

    public void UnEquipPotion()
    {
        _potionGearImage.color = _defaultColor;
        _currentPotion = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_currentPotion == null)
        {
            _gearInfoText.text = "Potion: None";
            _gearInfo.SetActive(true);
            return;
        }

        if(_currentPotion._health != 0)
        {
            _gearInfoText.text = "Potion: Health\n" + _currentPotion._health + "HP";
            _gearInfoText.text += "\nWill be used in next battle.";
            _gearInfo.SetActive(true);
            return;
        }
        else
        {
            _gearInfoText.text = "Potion: Attack Speed\n" + _currentPotion._attackSpeedPercentage + "%";
            _gearInfoText.text += "\nWill be used in next battle.";
            _gearInfo.SetActive(true);
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gearInfo.SetActive(false);
    }

    //public void UsePotion()
    //{
    //    if (_currentPotion == null) return;

    //    if (_currentPotion._health != 0)
    //    {
    //        PlayerHealth.instance.HealPlayer(_currentPotion._health);
    //        _currentPotion = null;
    //        _potionGearImage.sprite = null;
    //        _potionGearImage.color = Color.clear;
    //        return;
    //    }
    //    else
    //    {
    //        PlayerAttack.instance.UsePotion(_currentPotion._attackSpeedPercentage);
    //        _currentPotion = null;
    //        _potionGearImage.sprite = null;
    //        _potionGearImage.color = Color.clear;
    //        return;
    //    }
    //}
}
