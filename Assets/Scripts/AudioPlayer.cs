//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;

public class AudioPlayer: MonoBehaviour
{
    AudioSource m_MyAudioSource;

    //Play the music
    //private bool m_Play = false;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    public bool m_ToggleChange = false;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Check to see if you just set the toggle to positive
        if (m_ToggleChange == true)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Play();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }

    }

}