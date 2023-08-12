using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    EnemyScriptableObject _enemyInTile;
    GameObject _enemyGameObject;
    PathTile _nextTile;

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetEnemy(EnemyScriptableObject enemy)
    {
        _enemyInTile = enemy;
    }

    public EnemyScriptableObject GetEnemy()
    {
        return _enemyInTile;
    }

    public void SetNextTile(PathTile tile)
    {
        _nextTile = tile;
    }

    public PathTile GetNextTile()
    {
        return _nextTile;
    }

    public void SetEnemyGameObject(GameObject enemy)
    {
        _enemyGameObject = enemy;
    }

    public GameObject GetEnemyGameObject()
    {
        return _enemyGameObject;
    }

    public void RemoveEnemy()
    {
        Destroy(_enemyGameObject);
        _enemyInTile = null;
        _enemyGameObject = null;
    }
}
