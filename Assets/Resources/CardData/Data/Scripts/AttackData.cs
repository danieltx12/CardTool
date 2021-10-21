using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Attack Data", menuName = "CardData/Attack")]
public class AttackData : CardData
{
    public float power;
    public int maxHealth;
    public DamageType damageType;
}
