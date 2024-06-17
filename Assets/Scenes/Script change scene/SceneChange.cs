using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   
    public string loadingSceneName = "LoadingScene";

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger area. Loading the next scene.");
            SceneManager.LoadScene(loadingSceneName);
        }
    }
}
