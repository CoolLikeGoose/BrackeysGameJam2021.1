using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("BackGroundGameplay")]
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float Ypos;
    
    //Btns methods
    public void OnPlayBtn()
    {
        SceneTransition.SwitchToScene("SceneToShow");
    }

    public void OnSettingsBtn()
    {

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
