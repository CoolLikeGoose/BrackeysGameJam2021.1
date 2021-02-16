using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenuController : MonoBehaviour
{
    public GameObject pausePopup;

    public void OnContinueBtn()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnSettingsBtn()
    {

    }

    public void OnMenuBtn()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneTransition.SwitchToScene("MenuV2");
    }
}
