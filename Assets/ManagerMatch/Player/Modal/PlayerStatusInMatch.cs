using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusInMatch : MonoBehaviour
{
    private int _Life;
    private int _Energy;
    private List<Card> _cards;
    public PlayerStatusInMatch(int life, int energy, List<Card> cards)
    {
        Life = life;
        Energy = energy;
        this._cards = cards;
    }
    public int Life
    {
        get { return _Life; }
        set { _Life = value; }
    }
    
    public int Energy
    {
        get { return _Energy; }
        set { _Energy = value; }
    }
    public void RemoveCardinDeck(Card card)
    {
        _cards.Remove(card);
    }
    public void AddCardinDeck(Card card)
    {
       _cards.Add(card);
    }
}
