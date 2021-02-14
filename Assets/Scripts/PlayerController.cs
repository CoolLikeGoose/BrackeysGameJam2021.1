using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public int personalId;

    private bool canMove = true;
    private float castRadius = 0.01f;
    private Rigidbody2D rb;

    private Transform groundChecker;
    //private Transform playerChecker;

    //private bool mergeComplete = false;
    private List<int> avaibleColors;
    private bool isNowMerging = false;

    [SerializeField] private LayerMask anotherPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundChecker = transform.GetChild(0);
        //playerChecker = transform.GetChild(1);

        GameManager.OnMergeComplete += () =>
        {
            if (personalId == InputManager.Instance.curPlayer)
                avaibleColors.AddRange(InputManager.avaibleColors);
        };

        GameManager.OnIdLoaded += () =>
        {
            avaibleColors = new List<int>() { personalId };
        };
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InputManager.Instance.curPlayer != personalId && !isNowMerging)
            {
                //InputManager.Instance.GetDestination(this);
                isNowMerging = true;

                MergeIntoAnother();
            }
            //else
            //{
            //    //StartCoroutine(ProvideMerge());
            //}
        }   
    }

    public void MergeIntoAnother()
    {
        InputManager.avaibleColors = avaibleColors;
        gameObject.SetActive(false);

        InputManager.Instance.DeleteCube(personalId);
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

    //public IEnumerator MoveToCur(Vector2 target)
    //{
    //    rb.isKinematic = true;
    //    rb.velocity = Vector2.zero;
    //    canMove = false;

    //    while (Vector2.Distance(target, transform.position) > 0.05f)
    //    {
    //        transform.position = Vector2.Lerp(transform.position, target, 0.1f);
    //        yield return null;
    //    }

    //    GameManager.OnMergeComplete?.Invoke();

    //    Destroy(gameObject);
    //}
}
