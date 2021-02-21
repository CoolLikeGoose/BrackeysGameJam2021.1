using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject playBtn;

    [Header("BackGroundGameplay")]
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float Ypos;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playBtn);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Btns methods
    public void OnPlayBtn()
    {
        SceneTransition.SwitchToScene("level_1");
    }

    public void OnMenuBtn()
    {
        SceneTransition.SwitchToScene("MenuV2");
    }

    //Continue
    public void OnSettingsBtn()
    {
        SceneTransition.SwitchToScene($"level_{PlayerPrefs.GetInt("Level", 1)}");
    }

    public void OnExitBtn()
    {
        Application.Quit();
    }

    private void RespawnObj(GameObject go)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        go.transform.position = new Vector2(Random.Range(leftX, rightX), Ypos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(leftX, Ypos), new Vector2(rightX, Ypos));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RespawnObj(collision.gameObject);
    }
}
