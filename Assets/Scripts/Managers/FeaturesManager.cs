using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesManager : MonoBehaviour
{
    private static FeaturesManager instance = null;

    [SerializeField]
    private List<Feature> Features;

    [SerializeField]
    private GameObject featurePrefab;

    [SerializeField]
    private GameObject selectedFeaturePrefab;

    [SerializeField]
    private GameObject featureListContainer;

    [SerializeField]
    private GameObject selectedFeaturesContainer;
    [SerializeField]
    private GameObject limitReachedObj;

    private Dictionary<string, Feature> featuresDictionary = new Dictionary<string, Feature>();

    private Dictionary<string, GameObject> postItDictionary = new Dictionary<string, GameObject>();

    public SelectedFeatures selectedFeatures;

    const int MaxFeaturesNumber = 100;

    [SerializeField]
    private ScrollRect selectedFeaturesScrollRect;

    [SerializeField]
    private FeaturesController featuresListController;

    public static FeaturesManager Instance
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

    void Start()
    {
        selectedFeatures = new SelectedFeatures();
        foreach (var feature in Features)
        {
            var instantiatedFeature = Instantiate(featurePrefab, Vector3.zero, Quaternion.identity, featureListContainer.transform);

            postItDictionary[feature.identifier] = instantiatedFeature;

            PostItController controller = instantiatedFeature.GetComponent<PostItController>();
            controller.header = feature.category;
            controller.content = feature.description;
            controller.myFeature = feature;

            feature.SetPostItController(controller);

            featuresDictionary.Add(feature.identifier, feature);
        }

        featuresListController.totalPages = Mathf.CeilToInt(Features.Count / 5f);
    }


    public void SelectFeature(string identifier)
    {
        var featureToAdd = featuresDictionary[identifier];
        if (TeamManager.Instance.CanAddFeature(featureToAdd))
        {
            MusicManager.Instance.PlayAddFeature();


            var newSelectedFeature = Instantiate(selectedFeaturePrefab, Vector3.zero, Quaternion.identity, selectedFeaturesContainer.transform);
            SelectedFeatureController controller = newSelectedFeature.GetComponent<SelectedFeatureController>();
            controller.content = featureToAdd.description;
            controller.myFeature = featureToAdd;

            featuresDictionary[identifier].SetSelectedFeatureGameObject(newSelectedFeature);

            selectedFeatures.AddFeature(featureToAdd);

            TeamManager.Instance.AddFeatureToTimes(featureToAdd);
            featureToAdd.Select();

            StartCoroutine(ScrollDownFeatures());
        }
        if (selectedFeatures.features.Count >= MaxFeaturesNumber) {
            limitReachedObj.SetActive(true);
        }
    }

    public IEnumerator ScrollDownFeatures()
    {
        yield return new WaitForEndOfFrame();
        selectedFeaturesScrollRect.verticalNormalizedPosition = 0;
    }

        public void UnselectFeature(string identifier)
    {
        if (featuresDictionary[identifier] != null) {
            MusicManager.Instance.PlayRemoveFeature();

            limitReachedObj.SetActive(false);

            if (selectedFeatures.Contains(featuresDictionary[identifier]))
            {

                TeamManager.Instance.RemoveFeatureFromTimes(featuresDictionary[identifier]);
            }

            selectedFeatures.RemoveFeature(featuresDictionary[identifier]);
            featuresDictionary[identifier].Unselect();
        }
    }

    public bool IsFeatureSelected(Feature feature)
    {
        foreach (var selectedFeature in selectedFeatures.features) {
            if (selectedFeature.identifier == feature.identifier)
            {
                return true;
            }
        }
        return false;
    }

    public void DeleteFeaturePostIt(string identifier)
    {
        postItDictionary[identifier].GetComponent<PostItController>().Disable();
    }
}
