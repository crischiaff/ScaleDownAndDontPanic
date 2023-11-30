using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private ResultDialogController dialog;

    private static ResultManager instance;



    public static ResultManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void ShowResults()
    {
        int score = 0;

        var rules = JamManager.Instance.GetCurrentRules();

       
        Dictionary<string, int> featureScores = new Dictionary<string, int>();

        foreach (var rule in rules)
        {
            List<Feature> consideredFeaturesInRule = new List<Feature>();
            foreach ( var ruleScore in rule.rules.scores)
            {
                consideredFeaturesInRule.Add(ruleScore.feature);
                if (FeaturesManager.Instance.selectedFeatures.features.Contains(ruleScore.feature))
                {
                    score += ruleScore.score;
                    if (featureScores.ContainsKey(ruleScore.feature.description))
                    {
                        featureScores[ruleScore.feature.description] += ruleScore.score;
                    }
                    else
                    {
                        featureScores[ruleScore.feature.description] = ruleScore.score;
                    }
                }

            }

            foreach (var selectedFeature in FeaturesManager.Instance.selectedFeatures.features)
            {
                if (!consideredFeaturesInRule.Contains(selectedFeature))
                {
                    score += GameManager.Instance.featureBaseScore;
                    if (featureScores.ContainsKey(selectedFeature.description))
                    {
                        featureScores[selectedFeature.description] += GameManager.Instance.featureBaseScore;
                    }
                    else
                    {
                        featureScores[selectedFeature.description] = GameManager.Instance.featureBaseScore;
                    }
                }
            }
        }

       

        Jam currentJam = JamManager.Instance.currentJam;

        string ranking = ScoreToRanking(score, currentJam);

        MusicManager.Instance.PlayResult();
        dialog.Open(currentJam, score, GameManager.Instance.title, ranking, featureScores);
    }

    private string ScoreToRanking(int score, Jam currentJam)
    {
        float scorePercent = (float)score / currentJam.maxScore;
        if (scorePercent > 0.8)
        {
            return "1st";
        }
        if (scorePercent > 0.6)
        {
            return "2nd";
        }
        if (scorePercent > 0.4)
        {
            return "3rd";
        }
        if (scorePercent > 0.2)
        {
            return "4th";
        }

        return "5th";
    }
}
