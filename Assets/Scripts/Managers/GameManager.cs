using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField]
    private TutorialController tutorial;
    [SerializeField]
    private InitialDialogController initialDialog;
    [SerializeField]
    private TextMeshProUGUI gameTitle;
    [SerializeField]
    private GameObject startJamPanel;

    public string title;

    [SerializeField]
    private GameParams gameParams;
    [SerializeField]
    private Animator buttonAnimator;


    private bool jamStarted = false;

    public int totalDays
    {
        get { return gameParams.totalDays; }
    }

    public int secondsForDay
    {
        get { return gameParams.secondsForDay; }
    }

    public float featureCostMultiplier
    {
        get { return gameParams.featureCostMultiplier; }
    }

    public int featureBaseScore
    {
        get { return gameParams.featureBaseScore; }
    }

    public static GameManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.Pause();
        tutorial.StartTutorial(true);
    }

    public void OnTutorialCompleted()
    {
        
    }

    public void OnGameStarted(string title)
    {
        gameTitle.text = title;
        this.title = title;
        TimeManager.Instance.Resume();
    }

    public void BackToTitle()
    {
        MusicManager.Instance.PlayClick();
        SceneManager.LoadScene("TitleScene");

    }
    public void RestartGame()
    {
        MusicManager.Instance.PlayClick();
        SceneManager.LoadScene("GameScene");

    }

    public bool IsJamStarted()
    {
        if (!jamStarted)
        {
            buttonAnimator.SetTrigger("Crazy");
        }
        return jamStarted;
    }

    public void StartJam()
    {
        jamStarted = true;
        JamManager.Instance.PickAJam();
        initialDialog.Open(JamManager.Instance.currentJam);
        startJamPanel.SetActive(false);
    }
}
