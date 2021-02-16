using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator anim;
    private static SceneTransition instance;

    private static string curTransScene;

    public static void SwitchToScene(string name)
    {
        curTransScene = name;
        instance.anim.SetTrigger("transStart");
    }

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
    }
}
