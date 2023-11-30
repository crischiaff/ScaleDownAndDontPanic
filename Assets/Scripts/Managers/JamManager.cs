using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class JamManager : MonoBehaviour
{
    [SerializeField]
    private Image jamImageUi;

    [SerializeField]
    private List<Jam> availableJams;
    [SerializeField]
    private GameObject rulesList;
    [SerializeField]
    private GameObject requirementPrefab;
    [SerializeField]
    private GameObject requirementDoublePrefab;

    private List<Rule> currentRules;


    private static JamManager instance = null;


    public Jam currentJam;

    public List<Accident> accidents;

    public static JamManager Instance
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

    public List<Rule> GetCurrentRules()
    {
        return this.currentRules;
    }

    private void RandomizeAccidents()
    {
        accidents = new List<Accident>(currentJam.accidents);

        // Use the Fisher-Yates shuffle algorithm to randomize the order
        System.Random rand = new System.Random();
        int n = accidents.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            Accident value = accidents[k];
            accidents[k] = accidents[n];
            accidents[n] = value;
        }

    }

    public void PickAJam()
    {
        currentJam = availableJams[UnityEngine.Random.Range(0, availableJams.Count)];
        jamImageUi.sprite = currentJam.image;
        jamImageUi.gameObject.SetActive(true);
        currentRules = new List<Rule>();
        foreach (Rule rule in currentJam.rules)
        {
            currentRules.Add(rule);
        }
        RandomizeAccidents();
        RefreshUI();
    }

    public Accident PickAccident()
    {
        if (accidents.Count > 0)
        {
            Accident accident = accidents[accidents.Count - 1];
            accidents.RemoveAt(accidents.Count - 1);
            return accident;
        }
        else
        {
            throw new Exception("No more accidents");
        }
    }

    private void RefreshUI()
    {
        foreach (Transform child in rulesList.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Rule rule in currentRules)
        {

            var instantiatedRule = Instantiate(rule.isDouble ? requirementDoublePrefab : requirementPrefab, Vector3.zero, Quaternion.identity, rulesList.transform);
            instantiatedRule.GetComponent<RequirementController>().description = rule.description;
        }
    }

    public void AddRule(Rule rule)
    {
        currentRules.Add(rule);
        RefreshUI();
    }

    public Jam GetCurrentJam ()
    {
        return this.currentJam;
    }
}
