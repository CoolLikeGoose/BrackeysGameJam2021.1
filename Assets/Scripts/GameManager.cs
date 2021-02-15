using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnMergeComplete;
    public static Action OnChangeColor;
    //public static Action OnIdLoaded;

    private void OnDestroy()
    {
        OnChangeColor = null;
        OnMergeComplete = null;
    }
}
