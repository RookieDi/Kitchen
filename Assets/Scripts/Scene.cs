using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static  class Scene 
{
    public enum Scenee
    {
        StartScene,
        LoadingScene,
        GameScene
    }
    
    private static Scenee targetScene;
    
    public static void Load(Scenee scene)
    {
        targetScene = scene;
        SceneManager.LoadScene(Scenee.LoadingScene.ToString());
    }
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
