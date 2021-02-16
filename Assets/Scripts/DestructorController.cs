using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorController : MonoBehaviour
{
    private int canPass = 0;

    private void Start()
    {
        GameManager.OnMergeComplete += () =>
        {
            canPass += 4;
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canPass != 0)
        {
            canPass--;
        }
        else
        {
            SceneTransition.ReloadScene();
        }
    }
}
