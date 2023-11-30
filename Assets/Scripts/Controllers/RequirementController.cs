using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequirementController : MonoBehaviour
{
    public string description;

    [SerializeField]
    private TextMeshProUGUI text;
    void Start()
    {
        text.text = description;
    }
}
