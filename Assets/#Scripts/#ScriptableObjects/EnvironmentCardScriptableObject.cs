using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnvironmentCard", menuName = "EnvironmentCard", order = 1)]
public class EnvironmentCardScriptableObject : ScriptableObject
{
    public Sprite _sprite;
    [Header("Effected Player Stats")]
    public float _health;
    public float _attack;
    public float _defense;
    public float _speed;
}
