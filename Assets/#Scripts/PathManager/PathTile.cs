using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    GameObject _enemyInTile;
    PathTile _nextTile;

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetEnemy(GameObject enemy)
    {
        _enemyInTile = enemy;
    }

    public GameObject GetEnemy()
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
}
