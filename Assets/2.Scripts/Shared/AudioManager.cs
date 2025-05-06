using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private Queue<AudioSource> sfxPool;
    private int sfxPoolSize = 20;

    public AudioMixerGroup sfxGroup;
    public AudioClip bgmClip;
    public AudioSource bgmSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        bgmSource.clip = bgmClip;

        sfxPool = new Queue<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            GameObject obj = new GameObject("sfx" + i);
            obj.transform.parent = transform;
            AudioSource audioSource = obj.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = sfxGroup;
            sfxPool.Enqueue(audioSource);
        }
    }

    void Start()
    {
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource audioSource = sfxPool.Dequeue();

        if (audioSource == null) return;

        audioSource.clip = clip;
        audioSource.Play();
        
        StartCoroutine(EnqueueAfterPlay(audioSource, clip));
    }
    
    IEnumerator EnqueueAfterPlay(AudioSource audioSource, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length + 0.1f);
        sfxPool.Enqueue(audioSource);
    }
}
