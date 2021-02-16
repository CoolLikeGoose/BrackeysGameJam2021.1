using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExitController : MonoBehaviour
{
    [Header("Floating")]
    [SerializeField] float amplitude = 0.25f;
    [SerializeField] float speed = 1.2f;
    [SerializeField] float rotspeed = 0.01f;

    [Header("Flickering")]
    [SerializeField] float delay = 0.01f;
    [SerializeField] float flickSpeed = 0.1f;

    //[SerializeField] float offset = .4f;
    //[SerializeField] float smoothFactor = .4f;
    //[SerializeField] float updateDelay = .01f;

    //[Header("Flickering")]
    //[SerializeField] float flickSpeed = 0.1f;

    private float startY;
    private bool canWin;

    private void Start()
    {
        //StartCoroutine(StartFloat());
        startY = transform.position.y;

        GameManager.OnAllColorsCollected += () =>
        {
            StartCoroutine(StartFlickering());
            canWin = true;
        };
    }

    private void Update()
    {
        Vector2 newCoord = transform.position;

        newCoord.y = startY + amplitude * Mathf.Sin(speed * Time.time);

        transform.position = newCoord;

        transform.Rotate(new Vector3(0, 0, rotspeed));
    }

    //private IEnumerator StartFloat()
    //{
    //    float startY = transform.position.y;

    //    while (true)
    //    {

    //    }

    //    //Vector2 startPos = transform.position;
    //    //Vector2 curPos = startPos;

    //    //while (true)
    //    //{
    //    //    if (Mathf.Abs(curPos.y - startPos.y) >= distToFloat)
    //    //        speed *= -1;

    //    //    curPos.y += speed;
    //    //    transform.position = curPos;

    //    //    yield return new WaitForSeconds(delay);
    //    //}

    //    //Vector2 firstPoint = transform.position;
    //    //Vector2 secondPoint = firstPoint;
    //    //secondPoint.y += offset;

    //    //while (true)
    //    //{
    //    //    if (Mathf.Approximately(transform.position.y, secondPoint.y))
    //    //    {
    //    //        Vector2 buffer = firstPoint;
    //    //        firstPoint = secondPoint;
    //    //        secondPoint = buffer;
    //    //    }

    //    //    transform.position = Vector2.Lerp(transform.position, secondPoint, smoothFactor);

    //    //    yield return new WaitForSeconds(updateDelay);
    //    //}
    //}

    private IEnumerator StartFlickering()
    {
        Light2D light = transform.GetChild(0).GetComponent<Light2D>();

        while (true)
        {
            float curRadius = light.pointLightInnerRadius;

            if (curRadius <= 0 || curRadius >= 1.14f)
                flickSpeed *= -1;

            curRadius += flickSpeed;
            light.pointLightInnerRadius = curRadius;

            yield return new WaitForSeconds(delay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canWin && collision.CompareTag("Player"))
        {
            //GameManager.OnLevelComplete?.Invoke();
            SceneTransition.ReloadScene();
        }
    }
}
