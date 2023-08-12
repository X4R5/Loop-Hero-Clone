using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public Sprite _sprite;
    public float _maxHealth, _minAttack, _maxAttack, _attackDelay;
}
