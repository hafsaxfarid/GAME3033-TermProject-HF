using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackID
{
    MainMenu,
    Castle,
    Forest,
    None
}

public class AudioManager : MonoBehaviour
{
    [Header("Track ID List")]
    public TrackID tracks;


    [Header("List of Music Audio Clips")]
    // Add in same order as TrackID - list of all audio that will be used
    [SerializeField]
    AudioClip[] audioClips;

    [SerializeField]
    AudioSource audioSource1;

    [SerializeField]
    AudioSource audioSource2;

    // singleton variable for Audio Manager
    public static AudioManager amInstance;

    private void Awake()
    {
        if (amInstance == null)
        {
            amInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayAudio(TrackID.MainMenu);
    }

    public void PlayAudio(TrackID trackID)
    {
        audioSource1.Stop();
        audioSource2.Stop();

        audioSource2.clip = audioSource1.clip;

        audioSource1.clip = audioClips[(int)trackID];
        audioSource1.Play();
        tracks = TrackID.MainMenu;
    }
}
