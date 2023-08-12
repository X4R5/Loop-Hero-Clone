using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleController : MonoBehaviour
{
    public static EnemyBattleController instance;

    [SerializeField] EnemyScriptableObject _currentEnemy;
    float _currentHealth, _maxHealth, _minAttack, _maxAttack, _attackDelay;
    float ctr = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetEnemy(_currentEnemy);
    }

    private void Update()
    {
        if (ctr < _attackDelay)
        {
            ctr += Time.deltaTime;
        }
    }

    public void SetEnemy(EnemyScriptableObject enemy)
    {
        _currentEnemy = enemy;
        _maxHealth = enemy._maxHealth;
        _currentHealth = _maxHealth;
        _minAttack = enemy._minAttack;
        _maxAttack = enemy._maxAttack;
        GetComponent<Image>().sprite = enemy._sprite;
        _attackDelay = enemy._attackDelay;
    }

    public EnemyScriptableObject GetEnemy()
    {
        return _currentEnemy;
    }

    public bool CanAttack()
    {
        return ctr >= _attackDelay;
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

    public int GetAttackDamage()
    {
        return Random.Range((int)_minAttack, (int)_maxAttack + 1);
    }

    public float GetAttackDelayNormalized()
    {
        return ctr / _attackDelay;
    }

    public void ResetAttackDelay()
    {
        ctr = 0f;
    }

    public void ResetStats()
    {
        _currentHealth = _maxHealth;
        ctr = 0f;
    }

}
