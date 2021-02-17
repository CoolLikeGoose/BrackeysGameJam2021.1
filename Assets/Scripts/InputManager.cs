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
    public static int particleColors;
    public ParticleSystem particles;

    public GameObject PausePopup;

    [Header("Materials")]
    public List<Material> materials;

    [Header("Skin Main")]
    public List<Sprite> mainSprite;

    [Header("Skin Core")]
    public List<Sprite> coreSprite;

    [Header("Skin CoreHalf")]
    public List<Sprite> halfSprite;

    private void Awake()
    {
        Instance = this;

        player[0].personalId = 0;
        player[1].personalId = 1;
        player[2].personalId = 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player[curPlayer].sr.material = materials[3];

            curPlayer++;
            if (curPlayer == player.Count)
                curPlayer = 0;

            player[curPlayer].sr.material = materials[player[curPlayer].personalId];
            player[curPlayer].rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player[curPlayer].Jump(jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneTransition.ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player[curPlayer].NextColor();
            GameManager.OnChangeColor?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player[curPlayer].StartIdolActivity();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PausePopup.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    public PlayerController GetPlayerById(int id)
    {
        return player[id];
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
