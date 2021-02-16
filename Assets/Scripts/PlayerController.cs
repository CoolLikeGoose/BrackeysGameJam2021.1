using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public int personalId;
    [HideInInspector] public int numberInArray;

    private bool canMove = true;
    private float castRadius = 0.01f;

    [HideInInspector] public Rigidbody2D rb;
    private Light2D light2d;
    [SerializeField] private GameObject childWithLight;
    [HideInInspector] public SpriteRenderer sr;

    private Transform groundChecker;
    //private Transform playerChecker;

    //private bool mergeComplete = false;
    private List<int> avaibleColors;
    private bool isNowMerging = false;
    private int curColorIndex = 0;

    //[SerializeField] private LayerMask anotherPlayer;

    //Skin
    [SerializeField] private SpriteRenderer coreRend;
    [SerializeField] private SpriteRenderer halfRend;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        light2d = childWithLight.GetComponent<Light2D>();

        groundChecker = transform.GetChild(0);
        //playerChecker = transform.GetChild(1);

        GameManager.OnMergeComplete += () =>
        {
            EndMergeAsMain();
        };

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
        LayerMask maskForthiObj = 1 << gameObject.layer | 1 << gameObject.layer + 4 | 1 << gameObject.layer + 5 | 1;
        if (Physics2D.BoxCast(groundChecker.position, new Vector2(castRadius * 35, castRadius), 0, Vector3.forward, 50f, maskForthiObj))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * factor, ForceMode2D.Impulse);
        }
    }

    private int ArrayClamp(int number)
    {
        if (avaibleColors.Count > number)
        {
            return avaibleColors[number];
        }
        else
        {
            return avaibleColors[number - avaibleColors.Count];
        }
    }

    public void NextColor()
    {
        curColorIndex++;
        if (curColorIndex == avaibleColors.Count)
            curColorIndex = 0;

        int nowColor = avaibleColors[curColorIndex];
        gameObject.layer = nowColor + 6;
        personalId = nowColor;

        sr.material = InputManager.Instance.materials[nowColor];

        sr.sprite = InputManager.Instance.mainSprite[ArrayClamp(curColorIndex)];

        if (avaibleColors.Count >= 2)
        {
            coreRend.sprite = InputManager.Instance.coreSprite[ArrayClamp(curColorIndex + 1)];
        }

        if (avaibleColors.Count == 3)
        {
            halfRend.sprite = InputManager.Instance.halfSprite[ArrayClamp(curColorIndex + 2)];
        }

        //lights
        switch (nowColor)
        {
            case 0:
                light2d.color = new Color(1f, 0.1254902f, 0.1726206f);
                break;
            case 1:
                light2d.color = new Color(0.1273585f, 1f, 0.5259069f);
                break;
            case 2:
                light2d.color = new Color(0.1254902f, 0.6270434f, 1f);
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
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        InputManager.avaibleColors = avaibleColors;
        InputManager.particleColors = avaibleColors[curColorIndex];
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
        if (numberInArray == InputManager.Instance.curPlayer)
        {
            avaibleColors.AddRange(InputManager.avaibleColors);

            ParticleSystem.MainModule ps = Instantiate(InputManager.Instance.particles, transform.position, Quaternion.identity).main;
            Color newColor = Color.black;
            switch (InputManager.particleColors)
            {
                case 0:
                    newColor = new Color(1f, 0.1254902f, 0.1726206f);
                    break;
                case 1:
                    newColor = new Color(0.1273585f, 1f, 0.5259069f);
                    break;
                case 2:
                    newColor = new Color(0.1254902f, 0.6270434f, 1f);
                    break;
            }
            ps.startColor = newColor;
        }

        rb.isKinematic = false;
        canMove = true;

        //Change skin
        if (avaibleColors.Count >= 2)
        {
            coreRend.sprite = InputManager.Instance.coreSprite[avaibleColors[1]];
        }

        if (avaibleColors.Count == 3)
        {
            halfRend.sprite = InputManager.Instance.halfSprite[avaibleColors[2]];
            GameManager.OnAllColorsCollected?.Invoke();
        }
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
