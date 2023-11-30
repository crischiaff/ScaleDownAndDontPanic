using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedFeatureController : MonoBehaviour
{
    public string content;
    public Feature myFeature;

    [SerializeField]
    private TextMeshProUGUI contentText;

    [SerializeField]
    private GameObject roleList;
    [SerializeField]
    private GameObject roleDotPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Color developerColor;
        ColorUtility.TryParseHtmlString("#CF6ED1", out developerColor);

        Color designerColor;
        ColorUtility.TryParseHtmlString("#6E7AD1", out designerColor);

        Color composerColor;
        ColorUtility.TryParseHtmlString("#78D16E", out composerColor);

        Color writerColor;
        ColorUtility.TryParseHtmlString("#C6923F", out writerColor);


        if (myFeature.developerEffortPercent > 0)
        {
            var devDot = Instantiate(roleDotPrefab, Vector3.zero, Quaternion.identity, roleList.transform);
            devDot.GetComponent<DotController>().AssignColor(developerColor);
        }
        if (myFeature.designerEffortPercent > 0)
        {
            var designerDot = Instantiate(roleDotPrefab, Vector3.zero, Quaternion.identity, roleList.transform);
            designerDot.GetComponent<DotController>().AssignColor(designerColor);
        }
        if (myFeature.composerEffortPercent > 0)
        {
            var composerDot = Instantiate(roleDotPrefab, Vector3.zero, Quaternion.identity, roleList.transform);
            composerDot.GetComponent<DotController>().AssignColor(composerColor);
        }
        if (myFeature.writerEffortPercent > 0)
        {
            var devDot = Instantiate(roleDotPrefab, Vector3.zero, Quaternion.identity, roleList.transform);
            devDot.GetComponent<DotController>().AssignColor(writerColor);
        }
        contentText.text = content;
    }

    public void OnUnselect()
    {
        FeaturesManager.Instance.UnselectFeature(myFeature.identifier);
    }
}
