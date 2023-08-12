using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattleController : MonoBehaviour
{
    public static CharacterBattleController instance;

    [SerializeField] int _minAttack, _maxAttack;
    float _currentAttackDelay, _currentHealth;
    [SerializeField] float _defaultAttackDelay = 1.5f, _maxHealth = 100;
    float ctr = 0f;


    private void Awake()
    {
        instance = this;
        _currentAttackDelay = _defaultAttackDelay;
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (ctr < _currentAttackDelay)
        {
            ctr += Time.deltaTime;
        }
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }

    public float TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
        return GetHealthNormalized();
    }

    public void SetAttackValues(int minAtk, int maxAtk)
    {
        _minAttack = minAtk;
        _maxAttack = maxAtk;
    }

    public int GetAttackDamage()
    {
        return Random.Range(_minAttack, _maxAttack + 1);
    }

    public bool CanAttack()
    {
        return ctr >= _currentAttackDelay;
    }

    public void ResetAttackDelay()
    {
        ctr = 0f;
    }

    internal float GetAttackDelayNormalized()
    {
        return ctr / _currentAttackDelay;
    }
}
