using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesController : MonoBehaviour
{
    private int currentPage = 0;
    private bool moving = false;

    [SerializeField]
    private RectTransform featuresRect; // 9.55f

    [SerializeField]
    private TMP_Text pagesText;
    [SerializeField]
    private TMP_Text totalPagesText;

    public int totalPages; // Set from other place

    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;

    
    private Vector2 destinationPos;
    private Coroutine currentMoveCoroutine;

    private void Start()
    {
        leftButton.interactable = false;

        UpdatePagination();
        destinationPos = new Vector2(featuresRect.anchoredPosition.x, featuresRect.anchoredPosition.y);
    }

    private void UpdateDisabledButtons()
    {
        if (currentPage == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }

        if (currentPage == totalPages - 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
    }

    public void NextPage()
    {
        if (currentPage >= (totalPages - 1) || moving)
        {
            return;
        }
        MusicManager.Instance.PlayClick();
        MoveLeft(featuresRect);
        currentPage++;
        UpdatePagination();
        UpdateDisabledButtons();
    }

    public void PrevPage()
    {
        if (currentPage <= 0 || moving)
        {
            return;
        }
        MusicManager.Instance.PlayClick();
        MoveRight(featuresRect);
        currentPage--;
        UpdatePagination();
        UpdateDisabledButtons();
    }

    private void UpdatePagination()
    {
        totalPagesText.text = totalPages.ToString();
        pagesText.text = (currentPage + 1).ToString();

    }

    public void MoveLeft(RectTransform panel)
    {
        destinationPos = new Vector2(destinationPos.x - 9.55f, destinationPos.y);
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        currentMoveCoroutine = StartCoroutine(Move(panel, destinationPos));
    }

    public void MoveRight(RectTransform panel)
    {
        destinationPos = new Vector2(destinationPos.x + 9.55f, destinationPos.y);
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(Move(panel, destinationPos));
    }

    IEnumerator Move(RectTransform rt, Vector2 targetPos)
    {
        //moving = true;
        float step = 0;
        while (step < 1)
        {
            rt.offsetMin = Vector2.Lerp(rt.offsetMin, targetPos, step += Time.deltaTime);
            rt.offsetMax = Vector2.Lerp(rt.offsetMax, targetPos, step += Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //moving = false;
    }
}
