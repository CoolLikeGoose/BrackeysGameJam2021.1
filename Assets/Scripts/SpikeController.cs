using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneTransition.ReloadScene();
    }
}
