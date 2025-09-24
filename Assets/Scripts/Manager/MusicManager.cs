using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const float DEFAULT_MUSIC_VOLUME = 0.3f;

    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, DEFAULT_MUSIC_VOLUME);
    }

    public void ChangeVolume(float volume)
    {
        // normaliza o volume
        this.volume = volume / 100;

        audioSource.volume = this.volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, this.volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        // retorna o volume normalizado na escala de 100
        return volume * 100;
    }
}
