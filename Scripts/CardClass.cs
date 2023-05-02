using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardScriptableObject", order = 1)]
public class CardClass : ScriptableObject
{
    [SerializeField] private CardType _type;
    [SerializeField] private CardColor _color;
    [SerializeField] private int _value;
    [SerializeField] private int _drawOrDiscard;

    [SerializeField] public Material SkinClassic;
    [SerializeField] public Material SkinFood;
    [SerializeField] public Material SkinShape;

    public int GetDrawNumber
    {
        get
        {
            return _drawOrDiscard;
        }
    }
    public bool Compare(CardClass compareCard)
    {
        if ((this._color == compareCard._color) || (this._value == compareCard._value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public CardType GetCardType
    {
        get { return _type; }
    }
    public CardColor GetCardColor
    {
        get { return _color; }
    }
    public int GetValue
    {
        get { return _value; }
    }
}
public enum CardColor
{
    Blue,
    Green,
    Red,
    Yellow,
    None
}
public enum CardType
{
    Common,
    Wild,
    Draw,
    Discard,
    GameOver
}
