using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    Dictionary<Vector2, PathTile> _pathTiles = new Dictionary<Vector2, PathTile>();
    Dictionary<Vector2, OutsideTile> _outsideTiles = new Dictionary<Vector2, OutsideTile>();
    PathTile _startTile;

    PathGenerator _pathGenerator;

    private void Awake()
    {
        instance = this;
        _pathGenerator = GetComponent<PathGenerator>();
    }

    private void Start()
    {
        _pathGenerator.GeneratePath();
        CharacterController.instance.StartMove(_startTile);
    }

    public void SetStartTile(PathTile tile)
    {
        _startTile = tile;
    }

    public PathTile GetStartTile()
    {
        return _startTile;
    }

    public void AddPathTile(Vector2 position, PathTile tile)
    {
        _pathTiles.Add(position, tile);
    }

    public void RemovePathTile(Vector2 position)
    {
        _pathTiles.Remove(position);
    }

    public void ClearPath()
    {

        ClearOutsideTiles();

        foreach (var pathTile in _pathTiles)
        {
            Destroy(pathTile.Value.gameObject);
        }
        _pathTiles.Clear();
    }

    public Dictionary<Vector2, PathTile> GetPathTiles()
    {
        return _pathTiles;
    }

    public void AddOutsideTile(Vector2 position, OutsideTile tile)
    {
        _outsideTiles.Add(position, tile);
    }

    public void RemoveOutsideTile(Vector2 position)
    {
        _outsideTiles.Remove(position);
    }

    public Dictionary<Vector2, OutsideTile> GetOutsideTiles()
    {
        return _outsideTiles;
    }

    public void ClearOutsideTiles()
    {
        foreach (var outsideTile in _outsideTiles)
        {
            Destroy(outsideTile.Value.gameObject);
        }
        _outsideTiles.Clear();
    }
}
