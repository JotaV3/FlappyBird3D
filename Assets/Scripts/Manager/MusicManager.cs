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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, DEFAULT_MUSIC_VOLUME);
    }

    private void Start()
    {
        if (Loader.TryGetScene(Loader.Scene.GameScene))
        {
            MenuManager.Instance.OnCloseSettingsUI += MenuManager_OnCloseSettingsUI;
        }
    }

    private void MenuManager_OnCloseSettingsUI(object sender, System.EventArgs e)
    {
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, this.volume);
        PlayerPrefs.Save();
    }

    public void ChangeVolume(float volume)
    {
        // normaliza o volume
        this.volume = volume / 100;

        audioSource.volume = this.volume;       
    }

    public float GetVolume()
    {
        // retorna o volume normalizado na escala de 100
        return volume * 100;
    }
}
