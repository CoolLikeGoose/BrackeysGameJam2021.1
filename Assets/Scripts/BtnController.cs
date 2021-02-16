using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;

    [SerializeField] private ColorToMask colorReact;

    private bool isOpen = false;
    private bool stillInCollider = false;

    private void Start()
    {
        GameManager.OnChangeColor += () =>
        {
            if (isOpen)
                CloseAll();
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != (int)colorReact)
            return;

        isOpen = true;

        OpenAll();   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != (int)colorReact)
            return;

        isOpen = false;
        stillInCollider = false;

        StartCoroutine(CloseAll());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (stillInCollider || collision.gameObject.layer != (int)colorReact)
            return;

        stillInCollider = true;
    }

    private void OpenAll()
    {
        foreach (DoorController door in doors)
        {
            door.Open();
        }
    }

    private IEnumerator CloseAll()
    {
        yield return null;

        if (stillInCollider)
            yield break;

        foreach (DoorController door in doors)
        {
            door.Close();
        }
    }
}

enum ColorToMask
{
    Default = 0,
    Red = 6,
    Green = 7,
    Blue = 8
}
