using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] musics;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if (musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
