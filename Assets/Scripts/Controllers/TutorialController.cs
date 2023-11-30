using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private int currentTutorialPage;
    private Animator animator;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private List<TutorialScreen> tutorials;

    [SerializeField]
    private Image currentTutorialImage;
    [SerializeField]
    private TextMeshProUGUI currentTutorialDescription;

    [SerializeField]
    private TextMeshProUGUI currentTutorialOnlyDescription;

    [SerializeField]
    private GameObject tutorialWithImage;
    [SerializeField]
    private GameObject tutorialWithText;

    [SerializeField]
    private TextMeshProUGUI closeTutorialText;

    [SerializeField]
    private Button rightButton;
    [SerializeField]
    private Button leftButton;

    private bool isOnInit; // If is on init then time is managed externally
    

    public void StartTutorial(bool onInit)
    {
        this.isOnInit = onInit;
        if (!this.isOnInit)
        {
            closeTutorialText.text = "close tutorial";
            TimeManager.Instance.Pause();
        }
        else
        {
            closeTutorialText.text = "skip tutorial";
        }
        currentTutorialPage = 0;
        container.SetActive(true);
        GetComponent<Animator>().SetBool("Open", true);
        UpdateUI();
    }

    public void Next()
    {

        MusicManager.Instance.PlayClick();
        if (currentTutorialPage >= (tutorials.Count - 1))
        {
            DoSkip();
        } else
        {
            currentTutorialPage++;
            UpdateUI();
        }
    }

    public void Previuos()
    {
        if (currentTutorialPage <= 0)
        {
            return;
        }
        currentTutorialPage--;
        MusicManager.Instance.PlayClick();
        UpdateUI();
    }

    public void Skip()
    {
        MusicManager.Instance.PlayClick();
        DoSkip();
    }

    private void DoSkip()
    {
        Close();

        if (this.isOnInit)
        {
            GameManager.Instance.OnTutorialCompleted();
        }
        else
        {
            if (GameManager.Instance.IsJamStarted())
            {

                TimeManager.Instance.Resume();
            }
        }
    }

    private void UpdateUI()
    {
        if (tutorials[currentTutorialPage].image == null)
        {
            tutorialWithImage.SetActive(false);
            tutorialWithText.SetActive(true);
            currentTutorialOnlyDescription.text = tutorials[currentTutorialPage].description;
        } else
        {
            tutorialWithImage.SetActive(true);
            tutorialWithText.SetActive(false);
            currentTutorialImage.sprite = tutorials[currentTutorialPage].image;
            currentTutorialDescription.text = tutorials[currentTutorialPage].description;
        }
        
        
        if (currentTutorialPage <= 0)
        {
            leftButton.interactable = false;
        } else
        {
            leftButton.interactable = true;
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetBool("Open", false);
        StartCoroutine(CloseRoutine());
    }

    IEnumerator CloseRoutine()
    {
        yield return new WaitForSecondsRealtime(.4f);
        container.SetActive(false);
    }
}
