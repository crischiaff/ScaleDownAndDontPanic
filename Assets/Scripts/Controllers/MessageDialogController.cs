using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDialogController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private GameObject container;
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private TextMeshProUGUI effect;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open(string message, string effect)
    {
        this.message.text = message;
        this.effect.text = effect;

        container.SetActive(true);
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        MusicManager.Instance.PlayClick();
        animator.SetBool("Open", false);
        StartCoroutine(CloseRoutine());
    }

    IEnumerator CloseRoutine()
    {
        yield return new WaitForSecondsRealtime(.4f);
        TimeManager.Instance.Resume();
        container.SetActive(false);
    }
}
