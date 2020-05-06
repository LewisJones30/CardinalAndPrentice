using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] AudioClip[] musicTracks;
    [SerializeField] AudioClip gameOverTrack;
    [SerializeField] float delayBetweenTracks = 2f;

    bool gameOverMusicPlaying = false;

    Health cardinalHealth;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());

        GameObject p1 = GameObject.FindGameObjectWithTag("Player 1");
        cardinalHealth = p1.GetComponent<Health>();
    }

    private void Update()
    {
        if (cardinalHealth.IsDead  && !gameOverMusicPlaying)
        {
            gameOverMusicPlaying = true;
            StopAllCoroutines();
            audioSource.Stop();
            audioSource.clip = gameOverTrack;
            audioSource.Play();
        }
    }

    IEnumerator PlayMusic()
    {
        int index = Random.Range(0, musicTracks.Length);

        while (true)
        {
            audioSource.clip = musicTracks[index];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length + delayBetweenTracks);

            index++;
            if (index >= musicTracks.Length) index = 0;
        }
    }
}
