using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feature", menuName = "ScriptableObjects/FeatureObject", order = 1)]

public class Feature: ScriptableObject
{
    public string identifier;

    public string category;

    public string description;


    public int developerEffortPercent;
    public int designerEffortPercent;
    public int composerEffortPercent;
    public int writerEffortPercent;

    private PostItController postItController;
    private GameObject selectedFeatureGameObject;


    public void SetPostItController (PostItController postItController)
    {
        this.postItController = postItController;
    }

    public void SetSelectedFeatureGameObject(GameObject selectedFeatureGameObject)
    {
        this.selectedFeatureGameObject = selectedFeatureGameObject;
    }

    public void Select()
    {
        postItController.OnSelect();
    }

    public void Unselect()
    {
        postItController.OnUnselect();

        Destroy(this.selectedFeatureGameObject);
    }
}
