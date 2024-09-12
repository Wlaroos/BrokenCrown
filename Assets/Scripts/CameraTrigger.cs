using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform _camPos01;
    [SerializeField] private Transform _camPos02;
    [SerializeField] private Transform _spawnPos01;
    [SerializeField] private Transform _spawnPos02;

    [SerializeField] private SpriteRenderer _wallToHide;

    private bool _isFirstPosition = true;
    private bool _doOnce = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null && !_doOnce)
        {
            // Just in case, stops all camera movement in the coroutine
            StopAllCoroutines();

            // If player is on the left, move them and the camera to the right
            if(_isFirstPosition)
            {
                collision.transform.position = _spawnPos02.position;
                StartCoroutine(CamSlide(.2f, _camPos02.position));
            }
            // Vice Versa
            else
            {
                collision.transform.position = _spawnPos01.position;
                StartCoroutine(CamSlide(.2f, _camPos01.position));
            }

            // Changes bools so that it swaps between the two positions and the coroutine can't be activated a second time when it's already active 
            _doOnce = true;
            _isFirstPosition = !_isFirstPosition;
        }
    }

    // Coroutine to move the camera from position 01 to 02
    private IEnumerator CamSlide(float slideTime, Vector3 newCamPos)
    {
        // Setup (_wallToHide is to make the transition look smoother)
        float timeCount = 0;
        Vector3 camPosition = Camera.main.transform.position;
        _wallToHide.enabled = false;

        // Lerping camera
        while(timeCount < slideTime)
        {
            Camera.main.transform.position = Vector3.Slerp(camPosition, newCamPos, timeCount / slideTime);
            timeCount += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = newCamPos;

        // Coroutine is over, allow it to be activated again (doOnce)
        _wallToHide.enabled = true;
        _doOnce = false;
    }

}
