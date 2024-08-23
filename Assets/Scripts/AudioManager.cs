using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Clips de audio para música y efectos de sonido (asignarlos en el Inspector)
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;
    public AudioClip gachaPullSound;
    // ... otros clips de audio

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Reproduce música de fondo
    public void PlayMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // Detiene la música de fondo
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Reproduce un efecto de sonido
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Otros métodos para controlar el volumen, pausar/reanudar la música, etc.
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}