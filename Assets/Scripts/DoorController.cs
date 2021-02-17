using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //public float delay = 0.01f;
    //public float speed = 0.01f;

    //private BoxCollider2D mainCollider;
    //private SpriteRenderer sr;
    private Animator anim;

    //private bool isOpenning;
    //private bool isClosing;

    //private Coroutine curCor;
    //private Coroutine buffCor;

    //private float finalScale = 0.8f;
    //private float startScale = 0.01f;

    private void Start()
    {
        //mainCollider = GetComponent<BoxCollider2D>();
        //sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        //if (isOpenning)
          //return;

        //mainCollider.enabled = false;
        //sr.enabled = false;
        anim.SetTrigger("Close");
        //isOpenning = true;

        //buffCor = StartCoroutine(OnOpen());
    }

    public void Close()
    {
        //if (isClosing)
            //return;

        ////mainCollider.enabled = true;
        ////sr.enabled = true;
        anim.SetTrigger("Open");
        //isClosing = true;

        //buffCor = StartCoroutine(OnClose());
    }

    //private IEnumerator OnOpen()
    //{
    //    isOpenning = true;

    //    GetReadyToCor(finalScale);

    //    while (transform.localScale.x > startScale)
    //    {
    //        transform.Rotate(Vector3.forward * speed * 100);

    //        transform.localScale -= Vector3.one * speed;

    //        yield return new WaitForSeconds(delay);
    //    }

    //    transform.localScale = Vector3.one * startScale;

    //    isOpenning = false;
    //}

    //private IEnumerator OnClose()
    //{
    //    isClosing = true;

    //    GetReadyToCor(startScale);

    //    while (transform.localScale.x < finalScale)
    //    {
    //        transform.Rotate(Vector3.forward * -speed * 100);

    //        transform.localScale += Vector3.one * speed;

    //        yield return new WaitForSeconds(delay);
    //    }

    //    transform.localScale = Vector3.one * finalScale;

    //    isClosing = false;
    //}

    //private void GetReadyToCor(float setScale)
    //{
    //    if (curCor != null)
    //        StopCoroutine(curCor);
    //    curCor = buffCor;

    //    transform.localRotation = Quaternion.Euler(Vector3.zero);
    //    transform.localScale = Vector3.one * setScale;

    //    isOpenning = false;
    //    isClosing = false;
    //}
}
