using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorController : MonoBehaviour
{
    private int canPass = 3;

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
            print("A");
            SceneTransition.ReloadScene();
            canPass = 3;
        }
    }
}
