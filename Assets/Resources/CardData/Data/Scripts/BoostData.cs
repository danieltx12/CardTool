using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Boost Data", menuName = "CardData/Boost")]
public class BoostData : CardData
{
    public StatIncrease statIncrease;
    public int boostPercent;
}
