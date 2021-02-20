using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private bool activated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        activated = true;

        collision.GetComponent<PlayerController>().ImitDeath();
        SceneTransition.ReloadScene();
    }
}
