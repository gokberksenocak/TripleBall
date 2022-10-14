using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource audioSource;
    
    public void BonusVoice()
    {
        audioSource.PlayOneShot(sounds[0]);
    }
    public void FailVoice()
    {
        audioSource.PlayOneShot(sounds[1]);
    }
    public void GoalVoice()
    {
        audioSource.PlayOneShot(sounds[2]);
    }
    public void PassVoice()
    {
        audioSource.PlayOneShot(sounds[3]);
    }
}