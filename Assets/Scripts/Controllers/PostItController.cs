using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostItController : MonoBehaviour
{
    public string header;
    public string content;
    public Feature myFeature;

    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private GameObject checkmark;

    [SerializeField]
    private GameObject roleDotPrefab;

    [SerializeField]
    private GameObject roleList;

    [SerializeField]
    private Image pivotImage;
    [SerializeField]
    private GameObject deactivated;

    private Animator animator;

    private bool enabled = true;

    public void Disable ()
    {
        Color whiteLowAlpha;
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteLowAlpha);
        whiteLowAlpha.a = 0.5f;
        pivotImage.color = whiteLowAlpha;
        enabled = false;
        deactivated.SetActive(true);
    }


    void Start()
    {
        this.animator = GetComponent<Animator>();

        this.transform.rotation = Quaternion.identity;
        this.transform.Rotate(new Vector3(0, 0, 3) * Random.Range(-2f, 2f));

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

        headerText.text = header;
        contentText.text = content;
    }

    public void PointerEnter()
    {
        if (!enabled)
        {
            return;
        }
        animator.SetBool("Hover", true);
    }

    public void PointerExit()
    {
        if (!enabled)
        {
            return;
        }
        animator.SetBool("Hover", false);
    }

    public void PointerClick()
    {
        if (!enabled)
        {
            return;
        }

        if (!GameManager.Instance.IsJamStarted() )
        {
            return;
        }
        if (FeaturesManager.Instance.IsFeatureSelected(myFeature))
        {
            // Then remove
            FeaturesManager.Instance.UnselectFeature(myFeature.identifier);
        }
        else
        {
            // Then add
            FeaturesManager.Instance.SelectFeature(myFeature.identifier);
        }
    }

    public void OnSelect()
    {
        checkmark.SetActive(true);
    }

    public void OnUnselect()
    {
        checkmark.SetActive(false);
    }
}
