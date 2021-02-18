using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [HideInInspector] public Vector2 destination;

    private float step = 0;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => destination != Vector2.zero);

        float dist = Vector2.Distance(transform.position, destination);
        step = dist / 3;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * 900f * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, destination, step * Time.deltaTime);
    }

    public void Destruct()
    {
        StartCoroutine(StartDestruct());
    }

    private IEnumerator StartDestruct()
    {
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * 0.1f;

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);
    }
}
