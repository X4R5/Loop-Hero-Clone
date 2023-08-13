using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public delegate void OnBattleEnd();
    public static OnBattleEnd onBattleEnd;

    [SerializeField] GameObject _battlePanel;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Image _playerHealthBar, _playerAttackBar, _enemyHealthBar, _enemyAttackBar;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (!_battlePanel.activeSelf) return;
        CheckPlayerAttack();
        CheckEnemyAttack();
    }

    private void CheckPlayerAttack()
    {
        if (CharacterBattleController.instance.CanAttack())
        {
            _playerAnimator.SetTrigger("attack");
            _playerAttackBar.fillAmount = 0f;
            CharacterBattleController.instance.ResetAttackDelay();

            Invoke("PlayerAttack", 0.5f);
        }
        else
        {
            _playerAttackBar.fillAmount = CharacterBattleController.instance.GetAttackDelayNormalized();
        }
    }

    private void PlayerAttack()
    {
        _enemyHealthBar.fillAmount = EnemyBattleController.instance.TakeDamage(CharacterBattleController.instance.GetAttackDamage());

        if (EnemyBattleController.instance.GetHealthNormalized() <= 0f)
        {
            Debug.Log("Enemy died!");
            CharacterController.instance.CanMove(true);
            HideBattlePanel();
            CharacterController.instance.GetCurrentTile().RemoveEnemy();

            onBattleEnd?.Invoke();
        }
    }

    private void HideBattlePanel()
    {
        _battlePanel.SetActive(false);
    }

    private void CheckEnemyAttack()
    {
        if (EnemyBattleController.instance.CanAttack())
        {
            EnemyBattleController.instance.ResetAttackDelay();
            _enemyAttackBar.fillAmount = 0f;

            _playerHealthBar.fillAmount = CharacterBattleController.instance.TakeDamage(EnemyBattleController.instance.GetAttackDamage());
            _playerAnimator.SetTrigger("hit");

            if (CharacterBattleController.instance.GetHealthNormalized() <= 0f)
            {
                Debug.Log("Player died!");
            }
        }
        else
        {
            _enemyAttackBar.fillAmount = EnemyBattleController.instance.GetAttackDelayNormalized();
        }
    }

    public void StartBattle()
    {
        _battlePanel.SetActive(true);

        if(GearPotion.instance._currentPotion != null)
        {
            if (GearPotion.instance._currentPotion._health != 0)
            {
                CharacterBattleController.instance.Heal((int)GearPotion.instance._currentPotion._health);
            }
            else
            {
                CharacterBattleController.instance.SetAttackSpeed(GearPotion.instance._currentPotion._attackSpeedPercentage / 100);
            }
        }

        _playerHealthBar.fillAmount = CharacterBattleController.instance.GetHealthNormalized();
        _playerAttackBar.fillAmount = 0f;
        CharacterBattleController.instance.ResetAttackDelay();

        _enemyHealthBar.fillAmount = EnemyBattleController.instance.GetHealthNormalized();
        _enemyAttackBar.fillAmount = 0f;
        EnemyBattleController.instance.ResetStats();
    }

    public void EndBattle()
    {
        _battlePanel.SetActive(false);
    }

    internal void ShowBattlePanel()
    {
        _battlePanel.SetActive(true);
    }
}
