using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private CardClass[] _specialCardPool;
    [SerializeField] private int _maxCoins;
    [SerializeField] private int _maxCards;
    [SerializeField] private float _trackWidth;
    [SerializeField] private AnimationCurve _specialCardPoolBasedOnLevel;
    [SerializeField] private float _coinToCardRatio;
    void Start()
    {
        if (Random.value < _coinToCardRatio || PlayerPrefs.GetInt("level",1)<4)
        {
            PlaceCoins(Random.Range(0, _maxCoins + 1));
        }
        else
        {
            PlaceSpecialCards(Random.Range(0, _maxCards + 1));
        }
        
    }
    private void PlaceCoins(int num)
    {
        for (int i = 1; i <= num; i++)
        {
            GameObject coinGO = Instantiate(_coinPrefab, transform);
            coinGO.transform.localPosition = new Vector3(-_trackWidth + i * 2 * _trackWidth / (num + 1), 0, 0);
        }
    }
    private void PlaceSpecialCards(int num)
    {
        for (int i = 1; i <= num; i++)
        {
            GameObject cardGO = Instantiate(_cardPrefab, transform);
            cardGO.transform.localPosition = new Vector3(-_trackWidth + i * 2 * _trackWidth / (num + 1), 0, 0);
        }
    }
}
