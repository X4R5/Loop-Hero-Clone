using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCardsManager : MonoBehaviour
{
    public static EnvironmentCardsManager Instance;

    List<GameObject> _cards = new List<GameObject>();
    [SerializeField] Transform _minPos, _maxPos;
    [SerializeField] GameObject _cardPrefab;

    private void Awake()
    {
        Instance = this;
    }


    public void AddCard(EnvironmentCardScriptableObject card)
    {
        var newCard = Instantiate(_cardPrefab, transform);
        _cards.Add(newCard);
        SetCardPositionsInHand();
    }
    public void SetCardPositionsInHand()
    {
        var distanceBetweenPoints = _maxPos.position - _minPos.position;
        if (_cards.Count > 1)
        {
            distanceBetweenPoints = (_maxPos.position - _minPos.position) / (_cards.Count - 1);
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.position = _minPos.position + (distanceBetweenPoints * i);
        }
    }
}
