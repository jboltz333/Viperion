using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
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
        else if (scene.name == "MainMenuScene")
        {
            MainMenuScene();
        }
        else if (scene.name == "InstructionScene")
        {
            InstructionScene();
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
        var mainMenuInstructionsButton = GameObject.Find("Button_MainMenu_Instructions").GetComponent<Button>();
        var mainMenuExitButton = GameObject.Find("Button_MainMenu_Exit").GetComponent<Button>();

        // Wait to see what button the user selects and load that buttons scene or exit if exit is pressed
        mainMenuPlayGameButton.onClick.AddListener(delegate { LoadSceneByNum(2); });
        mainMenuInstructionsButton.onClick.AddListener(delegate { LoadSceneByNum(1); });
        mainMenuExitButton.onClick.AddListener(delegate { OnExitGame(); });
    }

    private void InstructionScene()
    {
        var instructionBackButton = GameObject.Find("Button_InstructionScene_Back").GetComponent<Button>();
        instructionBackButton.onClick.AddListener(delegate { LoadSceneByNum(0); });
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
