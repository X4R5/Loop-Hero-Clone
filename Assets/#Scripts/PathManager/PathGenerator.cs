using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] GameObject _pathTilePrefab, _outsideTilePrefab, _startHousePrefab;
    GameObject _startHouse;
    [SerializeField] Vector2 _pathStartPoint, _outsideStartPoint;
    Vector2 _lastDirection, _lastPosition;
    PathTile _lastPlacedTile;

    public int _maxXLenght = 14, _maxYLenght = 7;
    int _currentXLenght = 1, _currentXPoint = 0, _currentYPoint = 0;
    [SerializeField] Sprite _verticalPathSprite, _horizontalPathSprite, _leftUpPathSprite, _leftDownPathSprite, _rightUpPathSprite, _rightDownPathSprite;

    bool _isPathCreated = false;

    public void GeneratePath()
    {
        ClearPath();

        var startTile = Instantiate(_pathTilePrefab, _pathStartPoint, Quaternion.identity);
        PathManager.instance.AddPathTile(_pathStartPoint, startTile.GetComponent<PathTile>());
        PathManager.instance.SetStartTile(startTile.GetComponent<PathTile>());
        SetStartHouse();
        startTile.transform.parent = this.transform;
        _lastPosition = _pathStartPoint;
        _lastDirection = Vector2.right;
        _lastPlacedTile = startTile.GetComponent<PathTile>();
        while (!_isPathCreated)
        {
            PlaceTile();
        }

        PaintPath();
        //SetTileNexts();
        GenerateOutsideTiles();
    }

    private void SetStartHouse()
    {
        var startHouse = Instantiate(_startHousePrefab, _pathStartPoint, Quaternion.identity);
        startHouse.transform.parent = this.transform;
        _startHouse = startHouse;
    }

    void RemoveStartHouse()
    {
        Destroy(_startHouse);
        _startHouse = null;
    }

    private void GenerateOutsideTiles()
    {
        Vector2 lastPos = _outsideStartPoint;
        Vector2 fp = _outsideStartPoint;
        for (int i = 0; i < 143; i++)
        {
            Vector2 nextPos = lastPos;
            if(i == 0)
            {
                var startOutsideTile = Instantiate(_outsideTilePrefab, lastPos, Quaternion.identity);
                startOutsideTile.transform.parent = this.transform;
                PathManager.instance.AddOutsideTile(lastPos, startOutsideTile.GetComponent<OutsideTile>());
                continue;
            }

            if (lastPos.x == 3f)
            {
                nextPos = fp + Vector2.down;
                fp = nextPos;
            }
            else
            {
                nextPos = lastPos + Vector2.right;
            }

            if (PathManager.instance.GetPathTiles().ContainsKey(nextPos))
            {
                lastPos = nextPos;
                continue;
            }

            var mewOutsideTile = Instantiate(_outsideTilePrefab, nextPos, Quaternion.identity);
            mewOutsideTile.transform.parent = this.transform;
            PathManager.instance.AddOutsideTile(nextPos, mewOutsideTile.GetComponent<OutsideTile>());
            lastPos = nextPos;
        }
    }

    private void ClearPath()
    {
        if(PathManager.instance.GetPathTiles().Count != 0) PathManager.instance.ClearPath();
        RemoveStartHouse();
        _isPathCreated = false;
        _currentXLenght = 1;
        _currentXPoint = 0;
        _currentYPoint = 0;
    }

    private void PlaceTile()
    {
        var pathTiles = PathManager.instance.GetPathTiles();
        Vector2 nextDirection = SelectNextDirection(_lastDirection);
        Vector2 nextPosition = _lastPosition + nextDirection;

        if (nextDirection == Vector2.zero || pathTiles.ContainsKey(nextPosition))
        {
            _lastPlacedTile.SetNextTile(PathManager.instance.GetStartTile());
            _isPathCreated = true;
            return;
        }

        _lastPosition = nextPosition;
        _lastDirection = nextDirection;

        var newPathTile = Instantiate(_pathTilePrefab, _lastPosition, Quaternion.identity);
        PathManager.instance.AddPathTile(_lastPosition, newPathTile.GetComponent<PathTile>());
        newPathTile.transform.parent = this.transform;
        _lastPlacedTile.SetNextTile(newPathTile.GetComponent<PathTile>());
        _lastPlacedTile = newPathTile.GetComponent<PathTile>();
    }

    private Vector2 SelectNextDirection(Vector2 lastDirection)
    {
        var _pathTiles = PathManager.instance.GetPathTiles();
        List<Vector2> possibleDirections = new List<Vector2>();

        possibleDirections.Add(Vector2.up);
        possibleDirections.Add(Vector2.down);
        possibleDirections.Add(Vector2.left);
        possibleDirections.Add(Vector2.right);

        if (lastDirection == Vector2.up || _currentYPoint <= -_maxYLenght / 2 || _currentXLenght < 3 || (_currentXLenght < _maxXLenght && _currentYPoint == 0) || _currentXPoint == 0 || _currentXPoint == 1 || _pathTiles.ContainsKey(new Vector2(_lastPosition.x - 1, _lastPosition.y -1)) || _pathTiles.ContainsKey(new Vector2(_lastPosition.x + 1, _lastPosition.y - 1)) )
        {
            possibleDirections.Remove(Vector2.down);
        }
        if (lastDirection == Vector2.down || _currentYPoint >= _maxYLenght / 2 || _currentXPoint == _maxXLenght - 1 || _currentYPoint == -1 || (_currentYPoint == 0 && _currentXLenght == _maxXLenght) || (_currentYPoint == -2 && _currentXPoint != 0) || _pathTiles.ContainsKey(new Vector2(_lastPosition.x + 1, _lastPosition.y + 1)) || _pathTiles.ContainsKey(new Vector2(_lastPosition.x - 1, _lastPosition.y + 1)) || _currentXPoint == _maxXLenght - 2 )
        {
            possibleDirections.Remove(Vector2.up);
        }
        if (lastDirection == Vector2.right || _currentXLenght < _maxXLenght || _currentYPoint > 0 || _currentXPoint == 0 || (_currentYPoint == 0 && _currentXLenght == _maxXLenght) || _pathTiles.ContainsKey(new Vector2(_lastPosition.x - 1, _lastPosition.y + 1)))
        {
            possibleDirections.Remove(Vector2.left);
        }
        if (lastDirection == Vector2.left || _currentXLenght >= _maxXLenght)
        {
            possibleDirections.Remove(Vector2.right);
        }

        if (possibleDirections.Count == 0) return Vector2.zero;

        var randomDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];

        if (randomDirection == Vector2.up)
        {
            if (_currentYPoint >= 0 && _currentYPoint < _maxYLenght / 2)
            {
                _currentYPoint++;
            }
            else if (_currentYPoint <= 0 && _currentYPoint >= -_maxYLenght / 2)
            {
                _currentYPoint++;
            }
        }
        else if (randomDirection == Vector2.down)
        {
            if (_currentYPoint <= 0 && _currentYPoint > -_maxYLenght / 2)
            {
                _currentYPoint--;
            }
            else if (_currentYPoint >= 0 && _currentYPoint <= _maxYLenght / 2)
            {
                _currentYPoint--;
            }
        }
        else if (randomDirection == Vector2.right)
        {
            _currentXPoint++;
            _currentXLenght++;
        }
        else if (randomDirection == Vector2.left)
        {
            _currentXPoint--;
        }

        return randomDirection;
    }

    void PaintPath()
    {
        var _pathTiles = PathManager.instance.GetPathTiles();
        foreach (var pathTile in _pathTiles)
        {
            var pathTilePosition = pathTile.Key;
            var pathTileGameObject = pathTile.Value;

            if(_pathTiles.ContainsKey(pathTilePosition + Vector2.up) && _pathTiles.ContainsKey(pathTilePosition + Vector2.down))
            {
                pathTileGameObject.SetSprite(_verticalPathSprite);
            }else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.left) && _pathTiles.ContainsKey(pathTilePosition + Vector2.right))
            {
                pathTileGameObject.SetSprite(_horizontalPathSprite);
            }else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.down) && _pathTiles.ContainsKey(pathTilePosition + Vector2.left))
            {
                pathTileGameObject.SetSprite(_rightUpPathSprite);
            }else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.down) && _pathTiles.ContainsKey(pathTilePosition + Vector2.right))
            {
                pathTileGameObject.SetSprite(_leftUpPathSprite);
            }else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.up) && _pathTiles.ContainsKey(pathTilePosition + Vector2.left))
            {
                pathTileGameObject.SetSprite(_rightDownPathSprite);
            }else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.up) && _pathTiles.ContainsKey(pathTilePosition + Vector2.right))
            {
                pathTileGameObject.SetSprite(_leftDownPathSprite);
            }
        }
    }

    //void SetTileNexts()
    //{
    //    var _pathTiles = PathManager.instance.GetPathTiles();
    //    foreach (var pathTile in _pathTiles)
    //    {
    //        var pathTilePosition = pathTile.Key;
    //        var pathTileGameObject = pathTile.Value;

    //        if (_pathTiles.ContainsKey(pathTilePosition + Vector2.up))
    //        {
    //            pathTileGameObject.SetNextTile(_pathTiles[pathTilePosition + Vector2.up]);
    //        }
    //        else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.right))
    //        {
    //            pathTileGameObject.SetNextTile(_pathTiles[pathTilePosition + Vector2.right]);
    //        }
    //        else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.down))
    //        {
    //            pathTileGameObject.SetNextTile(_pathTiles[pathTilePosition + Vector2.down]);
    //        }
    //        else if (_pathTiles.ContainsKey(pathTilePosition + Vector2.left))
    //        {
    //            pathTileGameObject.SetNextTile(_pathTiles[pathTilePosition + Vector2.left]);
    //        }
            
    //    }
    //}
}