using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AccidentType
{
    ADD_RULE,
    REMOVE_TIME,
    REMOVE_FEATURE
}

public enum TeamRole
{
    DEVELOPER,
    DESIGNER,
    COMPOSER,
    WRITER
}

[CreateAssetMenu(fileName = "Accident", menuName = "ScriptableObjects/Accident", order = 1)]
public class Accident : ScriptableObject
{
    public string identifier;

    public string message;

    public string effect;

    public AccidentType type;

    [Header("ADD_RULE")]
    public Rule ruleToAdd;

    [Header("REMOVE_TIME")]
    public TeamRole role;
    public int hours;

    [Header("REMOVE_FEATURE")]
    public Feature feature;

    
}
