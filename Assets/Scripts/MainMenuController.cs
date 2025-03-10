using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject playButton, optionsButton, creditsButton, gameplayButton, characterOne, characterTwo, characterThree, characterFour, playGameButton, exitButton;

    private Dictionary<string, GameObject> firstSelectedMap;

    private float defaultVolume = 1.0f;
    private string selectedCharacter = ""; // Tracks the selected character

    private List<GameObject> characterButtons; // List of all character buttons
    private int playerCount = 1; // Start with player 1
    public GameObject[] playerOverlays; // Array for player overlays
    private List<string> joinedDevices = new List<string>(); // Track unique controllers
    private Dictionary<int, string> playerControllerAssignments = new Dictionary<int, string>(); // Track which controller is assigned to which player

    void Start()
    {
        InitializeFirstSelectedMap();
        ShowMainMenu();
        InitializeAudioSettings();
        InitializeVideoSettings();

        // Disable the PlayGame button initially
        playGameButton.GetComponent<Button>().interactable = false;

        // Initialize the list of character buttons
        characterButtons = new List<GameObject> { characterOne, characterTwo, characterThree, characterFour };
    }

    void Update()
    {
        // Check if "A" button (Xbox controller button) is pressed and there is room for another player
        if (IsXboxControllerButtonPressed())
        {
            OnPlayerJoin();
        }
    }

    private void InitializeFirstSelectedMap()
    {
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
        SetFirstSelected("ExitToMainFromSettings");
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
        gameplayPanel.SetActive(true); // Default to showing the gameplay panel
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

    public void PlayGameFromCharacterSelection()
    {
        if (selectedCharacter != "")
        {
            // Assuming all players selected characters
            Debug.Log("Starting game with characters: " + selectedCharacter);
            SceneManager.LoadScene("Level1Test");
        }
    }

    public void ExitToMainFromCredits()
    {
        credits.SetActive(false);
        ShowMainMenu();
        SetFirstSelected("ExitToMainFromCredits");
    }

    public void OnPlayerJoin()
    {
        // Ensure that only Player 1 can join before character selection
        if (!characterSelection.activeSelf && playerCount >= 1)
        {
            Debug.Log("Only one player can join before character selection.");
            return;
        }

        string[] joystickNames = Input.GetJoystickNames();
        string currentDevice = "";

        // Only register Xbox controllers
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            string joystick = Input.GetJoystickNames()[i];

            if (IsXboxController(joystick) && !playerControllerAssignments.ContainsValue(joystick))
            {
                currentDevice = joystick; // Set the current device that is trying to join
                break;
            }
        }

        if (!string.IsNullOrEmpty(currentDevice))
        {
            // Limit to 4 players
            if (playerCount < 4)
            {
                // Ensure Player 1 is the only one joining before character selection
                if (playerCount == 1 || (playerCount > 1 && characterSelection.activeSelf))
                {
                    playerCount++;
                    playerControllerAssignments[playerCount] = currentDevice; // Assign controller to the player

                    // Enable the corresponding player overlay
                    playerOverlays[playerCount - 1].SetActive(true);
                    Debug.Log($"Player {playerCount} joined using {currentDevice}");

                    // Update Character Selection Screen
                    SetFirstSelected("ShowCharacterSelection");
                }
            }
            else
            {
                Debug.Log("Maximum number of players reached.");
            }
        }
    }


    private bool IsXboxControllerButtonPressed()
    {
        // Check Xbox controller button (usually "A" button for joining)
        return Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    private bool IsXboxController(string joystickName)
    {
        // Check if the joystick is an Xbox controller
        return joystickName.Contains("Xbox");
    }

    public void SelectCharacter(string characterName)
    {
        // Update the selected character
        selectedCharacter = characterName;
        playGameButton.GetComponent<Button>().interactable = true;
        Debug.Log($"{GetPlayerLabel(playerCount)} selected: " + selectedCharacter);

        // Move the corresponding player's icon based on the character selection
        MovePlayerIcon(characterName, playerCount);
        foreach (GameObject characterButton in characterButtons)
        {
            Button button = characterButton.GetComponent<Button>();
            ColorBlock colors = button.colors;

            if (characterButton.name == characterName)
            {
                colors.normalColor = colors.selectedColor;
            }
            else
            {
                colors.normalColor = colors.disabledColor;
            }

            button.colors = colors;
        }
    }    

    private void MovePlayerIcon(string characterName, int playerNum)
    {
        Vector2 newScreenPos = Vector2.zero;

        // Set position based on player number and character selected
        switch (playerNum)
        {
            case 1: //move P1 to one of the 4 characters to select them for the level
                switch (characterName)
                {
                    case "CharacterOne":
                        newScreenPos = new Vector2(-480, 0);
                        break;
                    case "CharacterTwo":
                        newScreenPos = new Vector2(0, 0);
                        break;
                    case "CharacterThree":
                        newScreenPos = new Vector2(480, 0);
                        break;
                    case "CharacterFour":
                        newScreenPos = new Vector2(960, 0);
                        break;
                    default:
                        Debug.LogWarning("Character name not recognized.");
                        return;
                }
                break;

            case 2: //move P2 to one of the 4 characters to select them for the level
                switch (characterName)
                {
                    case "CharacterOne":
                        newScreenPos = new Vector2(-480, -330);
                        break;
                    case "CharacterTwo":
                        newScreenPos = new Vector2(0, -330);
                        break;
                    case "CharacterThree":
                        newScreenPos = new Vector2(480, -330);
                        break;
                    case "CharacterFour":
                        newScreenPos = new Vector2(960, -330);
                        break;
                    default:
                        Debug.LogWarning("Character name not recognized.");
                        return;
                }
                break;

            case 3: //move P3 to one of the 4 characters to select them for the level
                switch (characterName)
                {
                    case "CharacterOne":
                        newScreenPos = new Vector2(-830, -330);
                        break;
                    case "CharacterTwo":
                        newScreenPos = new Vector2(-350, -330);
                        break;
                    case "CharacterThree":
                        newScreenPos = new Vector2(130, -330);
                        break;
                    case "CharacterFour":
                        newScreenPos = new Vector2(610, -330);
                        break;
                    default:
                        Debug.LogWarning("Character name not recognized.");
                        return;
                }
                break;

            case 4: //move P4 to one of the 4 characters to select them for the level
                switch (characterName)
                {
                    case "CharacterOne":
                        newScreenPos = new Vector2(-830, 0);
                        break;
                    case "CharacterTwo":
                        newScreenPos = new Vector2(-350, 0);
                        break;
                    case "CharacterThree":
                        newScreenPos = new Vector2(130, 0);
                        break;
                    case "CharacterFour":
                        newScreenPos = new Vector2(610, 0);
                        break;
                    default:
                        Debug.LogWarning("Character name not recognized.");
                        return;
                }
                break;

            default:
                Debug.LogWarning("Player number not valid.");
                return;
        }

        // Access the player's overlay and move the icon
        GameObject overlay = playerOverlays[playerNum - 1]; // Get the overlay for the correct player
        RectTransform rt = overlay.GetComponent<RectTransform>();
        rt.anchoredPosition = newScreenPos;
    }

    private string GetPlayerLabel(int playerNum)
    {
        return playerNum switch
        {
            1 => "P1",
            2 => "P2",
            3 => "P3",
            4 => "P4",
            _ => "Unknown Player"
        };
    }

    private void HideAllPanels()
    {
        gameplayPanel.SetActive(false);
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
    }

    public void ShowGameplaySettings()
    {
        HideAllPanels();
        gameplayPanel.SetActive(true); // Display the gameplay panel
        Debug.Log("Gameplay settings shown");
    }

    public void ShowAudioSettings()
    {
        HideAllPanels();
        audioPanel.SetActive(true); // Display the audio panel
        Debug.Log("Audio settings shown");
    }

    public void ShowVideoSettings()
    {
        HideAllPanels();
        videoPanel.SetActive(true); // Display the video panel
        Debug.Log("Video settings shown");
    }

    private void InitializeAudioSettings()
    {
        volumeSlider.value = defaultVolume;
        muteToggle.isOn = false;
    }

    private void InitializeVideoSettings()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new List<string> { "1920x1080", "1280x720", "1024x768" });
        fullscreenToggle.isOn = true;
    }
}
