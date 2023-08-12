using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject _enemyPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        DayCycle.onDayChanged += SpawnEnemy;
    }

    private void OnDisable()
    {
        DayCycle.onDayChanged -= SpawnEnemy;
    }

    void SpawnEnemy()
    {
        PathTile randomTile = GetRandomTile();
        EnemyScriptableObject randomEnemy = GetRandomEnemy();
        randomTile.SetEnemy(randomEnemy);
        GameObject enemy = Instantiate(_enemyPrefab, randomTile.transform.position, Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().sprite = randomEnemy._sprite;
        randomTile.SetEnemyGameObject(enemy);
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

    EnemyScriptableObject GetRandomEnemy()
    {
        var allEnemies = Resources.LoadAll<EnemyScriptableObject>("Enemies");

        if (allEnemies.Length == 0)
        {
            throw new System.Exception("Kaynaklar boş!");
        }

        int randomIndex = UnityEngine.Random.Range(0, allEnemies.Length - 1);

        EnemyScriptableObject randomEnemy = allEnemies[randomIndex];

        return randomEnemy;
    }

}
