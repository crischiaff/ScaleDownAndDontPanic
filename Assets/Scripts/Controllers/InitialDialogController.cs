using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialDialogController : DialogController
{
    [SerializeField]
    private Image jamImageUi;
    [SerializeField]
    private TextMeshProUGUI jamPayoff;
    [SerializeField]
    private InputField titleInput;
    public void Open(Jam currentJam)
    {
        jamImageUi.sprite = currentJam.image;
        jamImageUi.gameObject.SetActive(true);
        jamPayoff.text = currentJam.payoff;

        this.Open();
    }

    public override void Close()
    {
        GameManager.Instance.OnGameStarted(titleInput.text);
        base.Close();
    }
}
