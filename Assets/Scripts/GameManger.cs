using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManger : MonoBehaviour
{
    // Makes Play Game button on the main menu scene interactable/non-interactable based on if user made character selections
    private bool isInteractable = false;

    // Player selection variables
    private InputField inputCharName;
    private Image bowSprite;

    // You can choose between these colors for your character
    private Color[] colors = { Color.cyan, Color.blue, Color.green, Color.red, Color.yellow, Color.white, Color.magenta };

    // Used to keep track of which color option we are on when the user clicks the right/left button in player selection
    private int colorCount = 0;

    void Start()
    {
        MainMenuScene();
    }

    public static GameManger Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // OnEnable, OnDisable, and OnLevelLoaded work to tell us when a new scene has been loaded
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene is fully loaded, call our scene's function to load components for that scene
        if (scene.name == "PlayGameScene")
        {
            //PlayGameScene();
        }
        else if (scene.name == "PlayerSelectionScene")
        {
            PlayerSelectionScene();
        }
        else if (scene.name == "MainMenuScene")
        {
            MainMenuScene();
        }
        else if (scene.name == "InstructionScene")
        {
            //InstructionScene();
        }
    }

    // This function loads new scenes when called
    private void LoadSceneByNum(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    private void MainMenuScene()
    {
        var mainMenuPlayGameButton = GameObject.Find("Button_MainMenu_PlayGame").GetComponent<Button>();
        var mainMenuPlayerSelectionButton = GameObject.Find("Button_MainMenu_PlayerSelection").GetComponent<Button>();
        var mainMenuInstructionsButton = GameObject.Find("Button_MainMenu_Instructions").GetComponent<Button>();
        var mainMenuExitButton = GameObject.Find("Button_MainMenu_Exit").GetComponent<Button>();

        // Wait to see what button the user selects and load that buttons scene or exit if exit is pressed
        mainMenuPlayGameButton.onClick.AddListener(delegate { LoadSceneByNum(3); });
        mainMenuPlayerSelectionButton.onClick.AddListener(delegate { LoadSceneByNum(2); });
        mainMenuInstructionsButton.onClick.AddListener(delegate { LoadSceneByNum(1); });
        mainMenuExitButton.onClick.AddListener(delegate { OnExitGame(); });

        // If the user has saved his choices in the Player Selection scene, they can use the Play Game button
        mainMenuPlayGameButton.interactable = isInteractable;
    }

    private void PlayerSelectionScene()
    {
        // Wait for the user to press the back button and load the main menu scene when they do
        var selectionBackButton = GameObject.Find("Button_PlayerSelection_Back").GetComponent<Button>();
        selectionBackButton.onClick.AddListener(delegate { LoadSceneByNum(0); });

        // Wait for the user to press the play button after he selects the player
        var playGame = GameObject.Find("Button_PlayerSelection_PlayGame").GetComponent<Button>();
        playGame.onClick.AddListener(delegate { LoadSceneByNum(3); });
        playGame.interactable = isInteractable;

        // Wait for the user to press the Save Selections button when they are done selecting their choices
        var saveGame = GameObject.Find("Button_PlayerSelection_SaveSelection").GetComponent<Button>();
        saveGame.onClick.AddListener(delegate { OnSaveGame(); playGame.interactable = true; });

        // Update our player name selection when the user types in a name and remember it between scenes
        inputCharName = GameObject.Find("InputField_PlayerSelection_PlayerName").GetComponent<InputField>();
        //inputCharName.onEndEdit.AddListener(delegate { OnEndEditName(); });

        // Update our players bow color selection when the user clicks the left/right button and remember it
        bowSprite = GameObject.Find("Image_PlayerSelection_PlayerColor").GetComponent<Image>();
        bowSprite.color = colors[colorCount];
        var rightColor = GameObject.Find("Button_PlayerSelection_PlayerColorLeft").GetComponent<Button>();
        var leftColor = GameObject.Find("Button_PlayerSelection_PlayerColorRight").GetComponent<Button>();
        rightColor.onClick.AddListener(delegate { SwitchColor(++colorCount); });
        leftColor.onClick.AddListener(delegate { SwitchColor(--colorCount); });
    }

    private void OnSaveGame()
    {
        // When the player's selections are saved, set this value to true so the user can press the play game button on main menu
        isInteractable = true;
    }

    private void OnEndEditName()
    {
        // Remember our players chosen character name between scenes
        //playerData.characterName = inputCharName.text.ToString();
    }

    // Whenever the user switches the bow color with the left/right buttons, we grab the corresponding index in the colors array
    private void SwitchColor(int colorInt)
    {
        // Make sure we don't go under index 0 or over index 6 since we only have 7 different colors in our colors array
        if (colorInt < 0)
        {
            colorCount = colorInt = 6;
        }
        else if (colorInt > 6)
        {
            colorCount = colorInt = 0;
        }

        // Set our player sprite color and remember it between scenes
        bowSprite.color = colors[colorInt];
    }

    // If the user clicks the Exit button, exit the game, whether the user is using the unity editor or hes playing the .exe version
    private void OnExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
