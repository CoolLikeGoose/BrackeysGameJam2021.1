using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;

    [SerializeField] private ColorToMask colorReact;

    private bool isOpen = false;

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

        CloseAll();
    }

    private void OpenAll()
    {
        foreach (DoorController door in doors)
        {
            door.Open();
        }
    }

    private void CloseAll()
    {
        foreach (DoorController door in doors)
        {
            door.Close();
        }
    }
}

enum ColorToMask
{
    Red = 6,
    Green = 7,
    Blue = 8
}
