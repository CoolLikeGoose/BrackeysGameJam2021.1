using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        player[0].personalId = 0;
        player[1].personalId = 1;
        player[2].personalId = 2;
    }

    private void Start()
    {
        //player[0].personalId = 0;
        //player[1].personalId = 1;
        //player[2].personalId = 2;

        //GameManager.OnIdLoaded?.Invoke();
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player[curPlayer].NextColor();
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
        player[persId].numberInArray = -1;
        StartCoroutine(player[persId].MoveToCur(player[curPlayer].transform.position));

        if (persId < curPlayer)
            curPlayer--;
        player.RemoveAt(persId);

        for (int i = 0; i < player.Count; i++)
        {
            player[i].numberInArray = i;
        }
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
