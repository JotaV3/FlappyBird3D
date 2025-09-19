using System;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    private const string PLAYER_PREFS_SENSITIVITY = "Sensitivity";
    private const int DEFAULT_SENSITIVITY = 30;

    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private TextMeshProUGUI sensitivityValueText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;
    [SerializeField] private Button backButton;

    private int sensitivity;

    private void Awake()
    {
        Instance = this;

        // carrega a sensibilidade do save
        sensitivity = PlayerPrefs.GetInt(PLAYER_PREFS_SENSITIVITY, DEFAULT_SENSITIVITY);

        sensitivitySlider.onValueChanged.AddListener((float sensitivity) =>
        {
            // sensitivityValue está como "Whole numbers", ou seja, números inteiros
            this.sensitivity = (int)sensitivity;
            sensitivityValueText.text = this.sensitivity.ToString();
        });

        musicSlider.onValueChanged.AddListener((float volume) =>
        {
            MusicManager.Instance.ChangeVolume(volume);
            musicVolumeText.text = volume.ToString();
        });

        soundEffectsSlider.onValueChanged.AddListener((float volume) =>
        {
            SoundManager.Instance.ChangeVolume(volume);
            soundEffectsVolumeText.text = volume.ToString();
        });

        backButton.onClick.AddListener(() =>
        {
            if(Loader.TryGetScene(Loader.Scene.GameScene))
            {
                MenuManager.Instance.CloseSettingsUI();
            }
            else
            {
                Hide();
            }                
        });
    }

    private void Start()
    {
        Hide();
        UpdateVisual();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_SENSITIVITY, sensitivity);
        PlayerPrefs.Save();
    }

    private void UpdateVisual()
    {
        sensitivityValueText.text = sensitivity.ToString();
        sensitivitySlider.value = sensitivity;
        musicVolumeText.text = MusicManager.Instance.GetVolume().ToString();
        musicSlider.value = MusicManager.Instance.GetVolume();
        soundEffectsVolumeText.text = SoundManager.Instance.GetVolume().ToString();
        soundEffectsSlider.value = SoundManager.Instance.GetVolume();
    }

    public int GetSensitivity()
    {
        return sensitivity;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
