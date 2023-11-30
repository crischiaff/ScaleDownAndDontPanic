using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TutorialScreen", menuName = "ScriptableObjects/TutorialScreen", order = 1)]

public class TutorialScreen : ScriptableObject
{
    [TextArea]
    public string description;
    public Sprite image;
}
