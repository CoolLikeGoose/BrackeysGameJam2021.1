using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;

    private bool isOpen;
    private Coroutine waitCor;
    private Rigidbody2D _activeRb;
    private OrbController curOrb;
    private PlayerController _curPlayer;

    private void Start()
    {
        GameManager.OnChangeColor += () =>
        {
            CancelAll();
        };
        GameManager.OnMergeComplete += () =>
        {
            CancelAll();
        };
    }

    private void CancelAll()
    {
        if (isOpen)
            CloseAll();
        if (waitCor != null)
            StopCoroutine(waitCor);
    }

    public void GiveOrbDest(OrbController orb)
    {
        Vector2 dest = transform.position;
        dest.y += .165f;
        orb.destination = dest;

        curOrb = orb;
    }

    public void Activate(PlayerController curPlayer)
    {
        _curPlayer = curPlayer;
        _activeRb = _curPlayer.rb;

        OpenAll();
        waitCor = StartCoroutine(WaitUntilDeactivate());
    }

    private IEnumerator WaitUntilDeactivate()
    {
        yield return new WaitUntil(() => _activeRb.velocity.x != Vector2.zero.x);

        CloseAll();
    }

    private void OpenAll()
    {
        isOpen = true;
        foreach (DoorController door in doors)
        {
            door.Open();
        }
    }

    private void CloseAll()
    {
        curOrb.Destruct();
        _curPlayer.isIdolActivated = false;

        isOpen = false;
        foreach (DoorController door in doors)
        {
            door.Close();
        }
    }
}
