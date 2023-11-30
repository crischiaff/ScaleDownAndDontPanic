using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultDialogController : DialogController
{
    [SerializeField]
    private Image jamImageUi;

    [SerializeField]
    private TextMeshProUGUI resultScoreText;
    [SerializeField]
    private TextMeshProUGUI rankingText;
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private GameObject features;

    [SerializeField]
    private GameObject featurePrefab;

    public void Open(Jam currentJam, int Score, string gameTitle, string ranking, Dictionary<string, int> featureScores)
    {
        jamImageUi.sprite = currentJam.image;
        jamImageUi.gameObject.SetActive(true);
        resultScoreText.text = "Score: " + Score.ToString();
        titleText.text = gameTitle;
        rankingText.text = ranking;

        foreach (KeyValuePair<string, int> featureScore in featureScores)
        {
            var newSelectedFeature = Instantiate(featurePrefab, Vector3.zero, Quaternion.identity, features.transform);
            AddedFeatureController controller = newSelectedFeature.GetComponent<AddedFeatureController>();
            controller.content = featureScore.Key;
            if (featureScore.Value > 0 )
            {

                controller.score = "<b>" + featureScore.Value.ToString() + "</b> pts";
            } else
            {
                controller.score = "<b>" + featureScore.Value.ToString() + "</b> pt";
            }
        }

        this.Open();
    }
}
