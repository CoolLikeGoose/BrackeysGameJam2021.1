using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public int personalId;
    [HideInInspector] public int numberInArray;

    private bool canMove = true;
    private float castRadius = 0.01f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Transform groundChecker;
    //private Transform playerChecker;

    //private bool mergeComplete = false;
    private List<int> avaibleColors;
    private bool isNowMerging = false;
    private int curColorIndex = 0;

    [SerializeField] private LayerMask anotherPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        groundChecker = transform.GetChild(0);
        //playerChecker = transform.GetChild(1);

        GameManager.OnMergeComplete += () =>
        {
            if (numberInArray == InputManager.Instance.curPlayer)
                avaibleColors.AddRange(InputManager.avaibleColors);

            EndMergeAsMain();
        };

        //GameManager.OnIdLoaded += () =>
        //{
        //    avaibleColors = new List<int>() { personalId };
        //    numberInArray = personalId;
        //};

        avaibleColors = new List<int>() { personalId };
        numberInArray = personalId;
    }

    public void Move(float factor)
    {
        if (canMove)
            rb.velocity = new Vector2(factor, rb.velocity.y);
    }

    public void Jump(float factor)
    {
        if (Physics2D.BoxCast(groundChecker.position, new Vector2(castRadius * 35, castRadius), 0, Vector3.forward))
            rb.AddForce(Vector2.up * factor, ForceMode2D.Impulse);
    }

    public void NextColor()
    {
        curColorIndex++;
        if (curColorIndex == avaibleColors.Count)
            curColorIndex = 0;

        switch (avaibleColors[curColorIndex])
        {
            // TODO: Replace color to sprite
            case 0:
                //E74963
                sr.color = new Color(0.9058824f, 0.2862746f, 0.387396f);
                gameObject.layer = 6;
                break;
            case 1:
                //4AE749
                sr.color = new Color(0.2884732f, 0.9056604f, 0.2862228f);
                gameObject.layer = 7;
                break;
            case 2:
                //49C6E7
                sr.color = new Color(0.2862746f, 0.7764998f, 0.9058824f);
                gameObject.layer = 8;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InputManager.Instance.curPlayer != numberInArray && !isNowMerging)
            {
                //InputManager.Instance.GetDestination(this);
                isNowMerging = true;

                MergeIntoAnother();
            }
            else
            {
                StartMergeAsMain();
            }
        }
    }

    public void MergeIntoAnother()
    {
        InputManager.avaibleColors = avaibleColors;
        //gameObject.SetActive(false);

        InputManager.Instance.DeleteCube(numberInArray);
    }

    public void StartMergeAsMain()
    {
        rb.isKinematic = true;
        Vector2 lastVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        canMove = false;
    }

    public void EndMergeAsMain()
    {
        rb.isKinematic = false;
        canMove = true;
    }

    //public IEnumerator ProvideMerge()
    //{
    //    rb.isKinematic = true;
    //    Vector2 lastVelocity = rb.velocity;
    //    rb.velocity = Vector2.zero;
    //    canMove = false;

    //    yield return new WaitUntil(() => mergeComplete);

    //    mergeComplete = false;

    //    rb.isKinematic = false;
    //    rb.velocity = lastVelocity;
    //    canMove = true;

    //    avaibleColors.Add(InputManager.Instance.lastMerge);
    //}

    public IEnumerator MoveToCur(Vector2 target)
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        canMove = false;

        while (Vector2.Distance(target, transform.position) > 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, target, 0.1f);
            yield return null;
        }

        GameManager.OnMergeComplete?.Invoke();

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
