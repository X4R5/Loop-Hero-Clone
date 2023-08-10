using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject _highLightGameObject;

    public bool IsPathTile()
    {
        return this.GetComponent<PathTile>() != null;
    }

    public bool IsOutsideTile()
    {
        return this.GetComponent<OutsideTile>() != null;
    }

    private void OnMouseOver()
    {
        _highLightGameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highLightGameObject.SetActive(false);
    }
}
