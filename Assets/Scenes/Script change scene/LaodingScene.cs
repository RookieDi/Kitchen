using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaodingScene : MonoBehaviour
{
    public string nextSceneName = "GameScene"; 
    public float waitTime = 20f; // Timpul de așteptare înainte de a încărca scena finală

    private void Start()
    {
        Debug.Log("Loading scene started. Waiting to load the next scene.");
        StartCoroutine(LoadSceneAfterWait());
    }

    private IEnumerator LoadSceneAfterWait()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Wait time completed. Loading the next scene.");
        SceneManager.LoadScene(nextSceneName);
    }
}
