using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class CardData : ScriptableObject
{
    public GameObject prefab;
    public Texture2D backImage;
    public Texture2D frontImage;
    public int manaCost;
    public string description;
    public string name;
}
