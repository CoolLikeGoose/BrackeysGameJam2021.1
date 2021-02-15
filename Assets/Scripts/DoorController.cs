using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private BoxCollider2D mainCollider;
    private SpriteRenderer sr;

    private void Start()
    {
        mainCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Open()
    {
        mainCollider.enabled = false;
        sr.enabled = false;
    }

    public void Close()
    {
        mainCollider.enabled = true;
        sr.enabled = true;
    }
}
