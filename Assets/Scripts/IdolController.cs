    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolController : MonoBehaviour
{
    [SerializeField] private List<DoorController> doors;
    [SerializeField] private IdolController linkedIdol;

    [HideInInspector] public bool isOpen;
    private Coroutine waitCor;
    private Rigidbody2D _activeRb;
    private OrbController curOrb;
    private PlayerController _curPlayer;

    private Transform target;

    private void Start()
    {
        target = transform.GetChild(0);

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
        orb.destination = target.position;

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
        yield return new WaitUntil(() => _activeRb.velocity.x != Vector2.zero.x); //&& Input.GetAxis("Horizontal") != 0);

        CloseAll();
    }

    private void OpenAll()
    {
        isOpen = true;

        if (linkedIdol != null && linkedIdol.isOpen)
            return;

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

        if (linkedIdol != null && linkedIdol.isOpen)
            return;

        foreach (DoorController door in doors)
        {
            door.Close();
        }
    }
}
