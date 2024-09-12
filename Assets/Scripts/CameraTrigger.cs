using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform _camPosLeft;
    [SerializeField] private Transform _camPosRight;
    [SerializeField] private Transform _spawnLeft;
    [SerializeField] private Transform _spawnRight;

    [SerializeField] private SpriteRenderer _wallToHide;

    private bool _left = true;
    private bool _once = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null && !_once)
        {
            StopAllCoroutines();
            if(_left)
            {
                Debug.Log("Left");
                collision.transform.position = _spawnRight.position;
                StartCoroutine(CamSlide(.2f, _camPosRight.position));
            }
            else
            {
                Debug.Log("Right");
                collision.transform.position = _spawnLeft.position;
                StartCoroutine(CamSlide(.2f, _camPosLeft.position));
            }
            _once = true;
            _left = !_left;
        }
    }

    private IEnumerator CamSlide(float slideTime, Vector3 newCamPos)
    {
        float timeCount = 0;
        Vector3 camPosition = Camera.main.transform.position;
        _wallToHide.enabled = false;

        while(timeCount < slideTime)
        {
            Camera.main.transform.position = Vector3.Slerp(camPosition, newCamPos, timeCount / slideTime);
            timeCount += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = newCamPos;

        _wallToHide.enabled = true;
        _once = false;
    }

}
