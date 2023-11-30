using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip musicClip;
    [SerializeField]
    private AudioClip addFeatureClip;
    [SerializeField]
    private AudioClip removeFeatureClip;
    [SerializeField]
    private AudioClip clickClip;
    [SerializeField]
    private AudioClip errorClip;
    [SerializeField]
    private AudioClip casualtyClip;
    [SerializeField]
    private AudioClip resultClip;

    [SerializeField]
    private GameObject audioOffOverlay;

    private AudioSource source;

    private static MusicManager instance = null;

    private bool audioOn;

    public static MusicManager Instance
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

    public void ToggleAudio()
    {
        if (audioOn)
        {
            source.Stop();
            audioOffOverlay.SetActive(true);
            audioOn = false;
        }
        else
        {
            source.Play();
            audioOffOverlay.SetActive(false);
            audioOn = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        audioOn = true;
        source.Play();
    }

    public void PlayClick()
    {
        source.PlayOneShot(clickClip, 1);
    }

    public void PlayAddFeature()
    {
        source.PlayOneShot(addFeatureClip, 1);
    }

    public void PlayRemoveFeature()
    {
        source.PlayOneShot(removeFeatureClip, 1);
    }

    public void PlayError()
    {
        source.PlayOneShot(errorClip, 1);
    }

    public void PLayCasualty()
    {
        source.PlayOneShot(casualtyClip, 1);
    }

    public void PlayResult()
    {
        source.PlayOneShot(resultClip, 1);
    }
}
