using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card/Card")]
public class Card : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] public int ID;
    public Sprite Sprite;
    public string Name;
    public string Description;
    public int Cost;
    public int Attack;
    public int Health;
}
