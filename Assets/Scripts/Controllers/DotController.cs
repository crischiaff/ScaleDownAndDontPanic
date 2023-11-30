using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotController : MonoBehaviour
{
    [SerializeField]
    private Image dotImage;
    
    public void AssignColor ( Color color)
    {
        if (dotImage != null)
        {
            dotImage.color = color;
        }
    }
}
