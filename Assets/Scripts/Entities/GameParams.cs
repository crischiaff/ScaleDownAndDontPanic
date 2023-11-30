using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParams", menuName = "ScriptableObjects/GameParams", order = 2)]
public class GameParams : ScriptableObject
{
    public int secondsForDay;
    public int totalDays;
    public float featureCostMultiplier;
    public int featureBaseScore;
}
