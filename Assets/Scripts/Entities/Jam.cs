using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jam", menuName = "ScriptableObjects/Jam", order = 1)]

public class Jam : ScriptableObject
{
    public string indentifier;

    public string title;

    public string payoff;

    public int maxScore;

    public Sprite image;

    public List<Rule> rules;

    public List<Accident> accidents;
}
