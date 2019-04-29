using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 控制声音
 * */
 [RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay; // 防止多次播放
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }
    
    /**
     * 播放声音
     * */
    public void Play()
    {
        if (!canPlay)
        {
            return;
        }

        GameManager.Instance.Timer.Add(
            () => { canPlay = true; }, delayBetweenClips);
        canPlay = false;
        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }
}
