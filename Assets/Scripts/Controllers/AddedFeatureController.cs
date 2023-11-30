using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddedFeatureController : MonoBehaviour
{
    public string content;
    public string score;

    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
        contentText.text = content;
        scoreText.text = score;
    }
}
