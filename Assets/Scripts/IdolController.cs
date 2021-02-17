using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;

    private bool isOpen;

    private void Start()
    {
        GameManager.OnChangeColor += () =>
        {
            if (isOpen)
                CloseAll();
        };
    }

    public void Activate()
    {
        OpenAll();
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
