using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private BoxCollider2D mainCollider;
    private SpriteRenderer sr;
    //private Animator anim;

    //private bool isOpenning;
    //private bool isClosing;

    private Coroutine curCor;

    private void Start()
    {
        mainCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
    }

    public void Open()
    {
        //if (isOpenning)
        //return;

        //mainCollider.enabled = false;
        //sr.enabled = false;
        //anim.SetTrigger("Close");
        //isOpenning = true;
        StartCoroutine(OnOpen());
    }

    public void Close()
    {
        //if (isClosing)
        //    return;

        ////mainCollider.enabled = true;
        ////sr.enabled = true;
        //anim.SetTrigger("Open");
        //isClosing = true;
        StartCoroutine(OnClose());
    }

    private IEnumerator OnOpen()
    {
        yield return null;
    }

    private IEnumerator OnClose()
    {
        yield return null;
    }

    private void GetReadyToCor()
    {

    }
}
