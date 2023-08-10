using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        PathTile randomTile = GetRandomTile();
        GameObject enemy = Instantiate(_enemyPrefab, randomTile.transform.position, Quaternion.identity);
    }

    private PathTile GetRandomTile()
    {
        var allTiles = PathManager.instance.GetPathTiles();

        if (allTiles.Values.Count == 0)
        {
            throw new System.Exception("Sözlük boş!");
        }

        int randomIndex = UnityEngine.Random.Range(0, allTiles.Values.Count - 1);

        PathTile randomTile = new List<PathTile>(allTiles.Values)[randomIndex];

        return randomTile;
    }

}
