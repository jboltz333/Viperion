using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    private GameObject confirmationPanel;

    private void Start()
    {
        confirmationPanel = GameObject.Find("Panel_PlayGame_ExitConfirmation");
        confirmationPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            confirmationPanel.SetActive(true);
            var yesButtonClicked = GameObject.Find("Button_PlayGame_ExitYes").GetComponent<Button>();
            var noButtonClicked = GameObject.Find("Button_PlayGame_ExitNo").GetComponent<Button>();

            // Wait to see what button the user selects and load that buttons scene or exit if exit is pressed
            yesButtonClicked.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
            noButtonClicked.onClick.AddListener(delegate { confirmationPanel.SetActive(false); Time.timeScale = 1; });
        }
    }
}
