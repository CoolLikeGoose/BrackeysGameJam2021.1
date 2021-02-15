using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;

    [SerializeField] private ColorToMask colorReact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print((int)colorReact);
        if (collision.gameObject.layer != (int)colorReact)
            return;

        foreach (DoorController door in doors)
        {
            door.Open();
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != (int)colorReact)
            return;

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
