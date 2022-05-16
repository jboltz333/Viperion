using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
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
            //PlayerSelectionScene();
        }
        else if (scene.name == "MainMenuScene")
        {
            //MainMenuScene();
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
        var mainMenuExitButton = GameObject.Find("Button_MainMenu_Exit").GetComponent<Button>();
        mainMenuExitButton.onClick.AddListener(delegate { OnExitGame(); });
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
