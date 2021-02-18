using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnMergeComplete;
    public static Action OnChangeColor;
    public static Action OnAllColorsCollected;
    public static Action OnLevelComplete;
    public static Action OnIdLoaded;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerPrefs.SetInt("Level", SceneTransition.GetCurSceneId());
    }

    private void OnDestroy()
    {
        OnChangeColor = null;
        OnMergeComplete = null;
        OnAllColorsCollected = null;
        OnLevelComplete = null;
    }
}
