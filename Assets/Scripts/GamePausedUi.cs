using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePausedUi : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button MainMenuButton;
   // public string mainMenuSceneName = "Assets/Scenes/StartScene"; 
    public string gameSceneName = "GameScene";
#if UNITY_EDITOR
    [SerializeField] private SceneAsset mainMenuSceneAsset;
#endif
    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);

        
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

}
    private void OnResumeButtonClicked()
    {
        GameManager.Instance.PauseGameStart();
    }

    private void OnMainMenuButtonClicked()
    {
#if UNITY_EDITOR
        if (mainMenuSceneAsset != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(mainMenuSceneAsset);
            SceneManager.LoadScene(scenePath);
        }
#else
        Debug.Log("Main Menu button clicked. Loading Main Menu scene.");
        SceneManager.LoadScene("StartScene"); 
#endif
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        GameManager.Instance.OnGameUNPaused += KitchenGameManage_OnGameUNPaused;
        Hide();
    }

    private void KitchenGameManage_OnGameUNPaused(object sender, EventArgs e)
    {
       Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
