using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu, settings, characterSelection, credits;
    public GameObject gameplayPanel, audioPanel, videoPanel;
    public Slider volumeSlider;
    public Toggle muteToggle;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private float defaultVolume = 1.0f;

    void Start()
    {
        ShowMainMenu();
        InitializeAudioSettings();
        InitializeVideoSettings();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        characterSelection.SetActive(false);
        credits.SetActive(false);
    }
    public void ShowCharacterSelection()
    {
        mainMenu.SetActive(false);
        characterSelection.SetActive(true);
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void ExitToMainFromSettings()
    {
        settings.SetActive(false);
        ShowMainMenu();
    }

    public void ExitToMainFromCharacterSelection()
    {
        characterSelection.SetActive(false);
        ShowMainMenu();
    }

    public void ExitToMainFromCredits()
    {
        credits.SetActive(false);
        ShowMainMenu();
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
        // Populate the dropdown with resolution options
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
