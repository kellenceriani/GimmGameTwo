using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu, settings, characterSelection, credits;
    public GameObject gameplayPanel, audioPanel, videoPanel;
    public Slider volumeSlider;
    public Toggle muteToggle;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    public EventSystem eventSystem;
    public GameObject playButton, optionsButton, creditsButton, gameplayButton, characterOne, exitButton;

    private Dictionary<string, GameObject> firstSelectedMap;

    private float defaultVolume = 1.0f;

    void Start()
    {
        InitializeFirstSelectedMap();
        ShowMainMenu();
        InitializeAudioSettings();
        InitializeVideoSettings();
    }

    private void InitializeFirstSelectedMap()
    {
        // Map methods to their respective first selected GameObject
        firstSelectedMap = new Dictionary<string, GameObject>
        {
            { "ShowCharacterSelection", characterOne },
            { "ShowSettings", gameplayButton },
            { "ShowCredits", exitButton },
            { "ExitToMainFromSettings", playButton },
            { "ExitToMainFromCharacterSelection", playButton },
            { "ExitToMainFromCredits", playButton }
        };
    }

    private void SetFirstSelected(string methodName)
    {
        if (firstSelectedMap.ContainsKey(methodName))
        {
            eventSystem.SetSelectedGameObject(firstSelectedMap[methodName]);
        }
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        characterSelection.SetActive(false);
        credits.SetActive(false);
        SetFirstSelected("ExitToMainFromSettings"); // Default to PlayButton
    }

    public void ShowCharacterSelection()
    {
        mainMenu.SetActive(false);
        characterSelection.SetActive(true);
        SetFirstSelected("ShowCharacterSelection");
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
        HideAllPanels();
        gameplayPanel.SetActive(true);
        SetFirstSelected("ShowSettings");
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        SetFirstSelected("ShowCredits");
    }

    public void ExitToMainFromSettings()
    {
        settings.SetActive(false);
        ShowMainMenu();
        SetFirstSelected("ExitToMainFromSettings");
    }

    public void ExitToMainFromCharacterSelection()
    {
        characterSelection.SetActive(false);
        ShowMainMenu();
        SetFirstSelected("ExitToMainFromCharacterSelection");
    }

    public void ExitToMainFromCredits()
    {
        credits.SetActive(false);
        ShowMainMenu();
        SetFirstSelected("ExitToMainFromCredits");
    }

    public void ShowGameplaySettings()
    {
        HideAllPanels();
        gameplayPanel.SetActive(true);
    }

    public void ShowAudioSettings()
    {
        HideAllPanels();
        audioPanel.SetActive(true);
    }

    public void ShowVideoSettings()
    {
        HideAllPanels();
        videoPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        gameplayPanel.SetActive(false);
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
    }

    private void InitializeAudioSettings()
    {
        volumeSlider.value = defaultVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(MuteAudio);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        muteToggle.isOn = volume == 0;
    }

    public void MuteAudio(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : volumeSlider.value;
    }

    private void InitializeVideoSettings()
    {
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1920x1080"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1280x720"));
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetResolution(int index)
    {
        if (index == 0) Screen.SetResolution(1920, 1080, fullscreenToggle.isOn);
        else if (index == 1) Screen.SetResolution(1280, 720, fullscreenToggle.isOn);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
