using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI daysText;

    private static TimeManager instance = null;

    private float elapsedTimeFromLastDay = 0;
    private int remainingDays;
    private int elapsedDays = 0;
    private int secondsForDay;
    //private float accidentOccurRate = 0.2f;

    private bool timePause = false;


    [SerializeField]
    private MessageDialogController messageDialogController;
    [SerializeField]
    private Animator flyAnimator;

    private float speed = 1;


    private static System.Random random = new System.Random();


    public static TimeManager Instance
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
        remainingDays = GameManager.Instance.totalDays;
        secondsForDay = GameManager.Instance.secondsForDay;
        instance = this;
    }

    private void Start()
    {
        refreshUI();
    }

    public void Pause()
    {
        timePause = true;
        flyAnimator.speed = 0;
    }

    public void Resume()
    {
        timePause = false;
        flyAnimator.speed = speed * 1f/secondsForDay;
    }

    private void Update()
    {
        if (timePause)
        {
            return;
        }

        elapsedTimeFromLastDay += Time.deltaTime * speed;
        if (elapsedTimeFromLastDay > secondsForDay)
        {
            elapsedTimeFromLastDay = 0f;
            remainingDays--;
            elapsedDays++;
            refreshUI();

            MaybeAccidentOccur();
        }

        if (remainingDays == 0)
        {
            this.Pause();
            ResultManager.Instance.ShowResults();
        }
    }

    private void refreshUI()
    {
        daysText.text = remainingDays.ToString();
    }

    private void MaybeAccidentOccur()
    {


        if (elapsedDays == 7)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 10)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 13)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 17)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 20)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 23)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
        if (elapsedDays == 25)
        {
            AccidentOccur(JamManager.Instance.PickAccident());
        }
    }

    private void AccidentOccur(Accident accident)
    {
        Pause();
        MusicManager.Instance.PLayCasualty();

        messageDialogController.Open(accident.message, accident.effect);

        if (accident.type == AccidentType.ADD_RULE)
        {
            ExecuteRuleAccident(accident);
        }
        if (accident.type == AccidentType.REMOVE_TIME)
        {
            ExecuteRemoveTimeAccident(accident);
        }
        if (accident.type == AccidentType.REMOVE_FEATURE)
        {
            ExecuteRemoveFeatureAccident(accident);
        }
    }

    private void ExecuteRuleAccident(Accident accident)
    {
        JamManager.Instance.AddRule(accident.ruleToAdd);
    }

    private void ExecuteRemoveTimeAccident(Accident accident)
    {
        TeamManager.Instance.RemoveTime(accident.role, accident.hours);
    }

    private void ExecuteRemoveFeatureAccident(Accident accident)
    {
        FeaturesManager.Instance.UnselectFeature(accident.feature.identifier);
        FeaturesManager.Instance.DeleteFeaturePostIt(accident.feature.identifier);
    }

    public void FasterSpeed()
    {
        speed = 2;
        if (!timePause)
        {
            flyAnimator.speed = speed * 1f / secondsForDay;
        }
    }

    public void NormalSpeed()
    {
        speed = 1;
        if (!timePause)
        {
            flyAnimator.speed = speed * 1f / secondsForDay;
        }
    }
}
