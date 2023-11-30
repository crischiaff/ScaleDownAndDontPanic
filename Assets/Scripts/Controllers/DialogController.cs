using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        container.SetActive(true);
        MusicManager.Instance.PlayClick();
        if (TimeManager.Instance != null)
        {

            TimeManager.Instance.Pause();
        }
        animator.SetBool("Open", true);
    }

    public virtual void Close()
    {
        animator.SetBool("Open", false);
        StartCoroutine(CloseRoutine());
        MusicManager.Instance.PlayClick();
    }

    IEnumerator CloseRoutine()
    {
        yield return new WaitForSecondsRealtime(.4f);
        if (TimeManager.Instance != null && GameManager.Instance != null)
        {
            if (GameManager.Instance.IsJamStarted())
            {

                TimeManager.Instance.Resume();
            }
        }
        container.SetActive(false);
    }
}
