using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public static void PlayAudio(AudioClip audioClip)
    {
        var sfxPlayer = FindObjectOfType<SfxPlayer>();
        sfxPlayer.PlayClip(audioClip);
    }
}
