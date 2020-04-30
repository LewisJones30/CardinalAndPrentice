using System;
using UnityEngine;

    public class RandomAudioPlayer : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] float pitchVariation = 0.1f;
        [SerializeField] bool onAwake = false;
        [SerializeField] AudioClip[] audioClips;

        float pitchOrigin;

        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (onAwake) PlayRandomAudio();

            pitchOrigin = audioSource.pitch;
        }

        public void PlayRandomAudio()
        {
            audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            audioSource.pitch = UnityEngine.Random.Range(pitchOrigin - pitchVariation, pitchOrigin + pitchVariation);
            audioSource.Play();
        }
    }


