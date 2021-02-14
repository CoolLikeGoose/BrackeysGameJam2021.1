using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private float playerSpeed = 300f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Players")]
    [Tooltip("RGB")]
    [SerializeField] private List<PlayerController> player;

    private float moveX = 0;
    [HideInInspector] public int curPlayer = 0;
    [HideInInspector] public int lastMerge = -1;

    public static List<int> avaibleColors;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player[0].personalId = 0;
        player[1].personalId = 1;
        player[2].personalId = 2;

        GameManager.OnIdLoaded?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            curPlayer++;
            if (curPlayer == player.Count)
                curPlayer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player[curPlayer].Jump(jumpForce);
        }

        moveX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        //rb[curPlayer].velocity = new Vector2(moveX * Time.fixedDeltaTime * playerSpeed, rb[curPlayer].velocity.y);
        player[curPlayer].Move(moveX * Time.fixedDeltaTime * playerSpeed);

        //if (player[curPlayer].CheckAnotherPlayer())
        //{
        //    print("yep");
        //}
    }

    public void DeleteCube(int persId)
    {
        if (persId < curPlayer)
            curPlayer--;
        player.RemoveAt(persId);

        GameManager.OnMergeComplete?.Invoke();
    }

    //public static void GetColorsList(List<int> _avaibleColors)
    //{
    //    avaibleColors = _avaibleColors;
    //}

    //public static List<int> ReturnColorList()
    //{
    //    return avaibleColors;
    //}

    //public void GetDestination(PlayerController pc)
    //{
    //    lastMerge = pc.personalId;
    //    StartCoroutine(pc.MoveToCur(player[curPlayer].transform.position));

    //    player.RemoveAt(lastMerge);

    //    curPlayer--;
    //}
}
