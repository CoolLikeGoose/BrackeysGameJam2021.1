using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator anim;
    private static SceneTransition instance;

    private static string curTransScene;
    //private static int curTransIndex;

    public static void SwitchToScene(string name)
    {
        curTransScene = name;
        instance.anim.SetTrigger("transStart");
    }

    public static int GetCurSceneId()
    {
        string[] sceneName = SceneManager.GetActiveScene().name.Split('_');

        return int.Parse(sceneName[1]);
    }

    //public static void SwitchToSceneById(int ind)
    //{
    //    curTransScene = null;
    //    curTransIndex = ind;
    //    instance.anim.SetTrigger("transStart");
    //}

    public static void ReloadScene()
    {
        SwitchToScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();

        anim.SetTrigger("transEnd");
    }

    public void OnAnimationEnd()
    {
        SceneManager.LoadScene(curTransScene);
        //if (curTransScene == null)
        //{
        //    SceneManager.LoadScene(curTransScene);
        //}
        //else
        //{
        //    SceneManager.LoadScene(curTransIndex);
        //}
    }
}
