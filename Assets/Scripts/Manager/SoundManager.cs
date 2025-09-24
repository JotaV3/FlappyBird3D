using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private const float DEFAULT_SOUND_EFFECTS_VOLUME = 0.5f;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private AudioSource audioSource;

    private float volume;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, DEFAULT_SOUND_EFFECTS_VOLUME);     
    }

    public void PlayJumpSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.jump, position);
    }

    public void PlaySound(AudioClip[] audioClip, Vector3 position)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void ChangeVolume(float volume)
    {
        // normaliza o volume
        this.volume = volume / 100;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        // retorna o volume normalizado para na escala de 100
        return volume * 100;
    }
}
