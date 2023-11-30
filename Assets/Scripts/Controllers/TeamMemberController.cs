using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamMemberController : MonoBehaviour
{
    [SerializeField]
    private Image progressImage;

    [SerializeField]
    private GameObject alertIcon;

    [SerializeField]
    private TextMeshProUGUI progressText;

    [SerializeField]
    private float initialTotalTime = 100;

    private float totalTime;

    private float allocatedTime = 0;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = initialTotalTime;
        progressImage.transform.localScale = new Vector3(0, 1, 1);
        alertIcon.SetActive(false);
        progressText.text = allocatedTime.ToString() + " / " + totalTime.ToString() + "h";
        animator = GetComponent<Animator>();
    }

    public void AllocateTime(int time)
    {
        allocatedTime += time;
        RefreshUI();
    }

    public void DeallocateTime(int time)
    {
        allocatedTime -= time;
        RefreshUI();
    }

    public void RemoveTime(int time)
    {
        totalTime -= time;
        RefreshUI();
    }

    public bool HasRemainingTime(int time)
    {

        bool hasRemainingTime =  (totalTime - allocatedTime) >= time;
        if (!hasRemainingTime)
        {
            animator.SetTrigger("Cannot");
        }

        return hasRemainingTime;
    }

    private void RefreshUI()
    {
        progressImage.transform.localScale = new Vector3(Mathf.Min(1, allocatedTime / totalTime), 1, 1);
        if (allocatedTime > totalTime - 10)
        {
            alertIcon.SetActive(true);
        }
        else
        {
            alertIcon.SetActive(false);
        }
        progressText.text = allocatedTime.ToString() + " / " + totalTime.ToString() + "h";
    }
}