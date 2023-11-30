using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule", menuName = "ScriptableObjects/Rule", order = 1)]
public class Rule : ScriptableObject
{
    public string identifier;

    public string description;

    public bool isDouble = false;

    public Score rules;
}
