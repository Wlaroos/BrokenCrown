using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform _camPos01;
    [SerializeField] private Transform _camPos02;
    [SerializeField] private Transform _spawnPos01;
	[SerializeField] private Transform _spawnPos02;
	[SerializeField] private Transform _openSign;

	[SerializeField] private SpriteRenderer _wallToHide;
    
	[SerializeField] private float _camSlideTime = 0.2f;
	
	private SpriteRenderer _signSR;
	private Light2D _signLight;
	private BoxCollider2D _bc;
	
    private bool _isFirstPosition = true;
    private bool _doOnce = false;
	private bool _isOpen = false;

	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		_signLight = _openSign.GetComponent<Light2D>();
		_signSR = _openSign.GetComponent<SpriteRenderer>();
		_bc = GetComponent<BoxCollider2D>();
		
		_signLight.enabled = false;
		_openSign.localPosition = Vector2.zero;
		_bc.enabled = false;
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z)) 
		{
			OpenStore();
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
	    if(collision.GetComponent<PlayerMovement>() != null && !_doOnce && _isOpen)
        {
            // Just in case, stops all camera movement in the coroutine
            StopAllCoroutines();

            // If player is on the left, move them and the camera to the right
            if(_isFirstPosition)
            {
                collision.transform.position = _spawnPos02.position;
                StartCoroutine(CamSlide(_camSlideTime, _camPos02.position));
            }
            // Vice Versa
            else
            {
                collision.transform.position = _spawnPos01.position;
	            StartCoroutine(CamSlide(_camSlideTime, _camPos01.position));
	            CloseStore();
            }

            // Changes bools so that it swaps between the two positions and the coroutine can't be activated a second time when it's already active 
            _doOnce = true;
            _isFirstPosition = !_isFirstPosition;
            
            PlayerStats.Instance.ScreenChange(!_isFirstPosition);
        }
    }

    // Coroutine to move the camera from position 01 to 02
    private IEnumerator CamSlide(float slideTime, Vector3 newCamPos)
    {
        // Setup (_wallToHide is to make the transition look smoother)
        float timeCount = 0;
        Vector3 camPosition = Camera.main.transform.position;
        _wallToHide.enabled = false;

        // Lerping camera position
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
    
	private IEnumerator SignSlide(float slideTime, Vector3 newSignPos, bool isOpen)
	{
		// Delay
		yield return new WaitForSeconds(_camSlideTime);
		
		// Setup
		float timeCount = 0;
		Vector3 signPosition = _openSign.localPosition;
		
		// Turns off light early when closing shop
		if(!isOpen)
		{
			_signLight.enabled = isOpen;
		}

		// Lerping sign position
		while(timeCount < slideTime)
		{
			_openSign.localPosition = Vector3.Slerp(signPosition, newSignPos, timeCount / slideTime);
			timeCount += Time.deltaTime;
			yield return null;
		}
		_openSign.localPosition = newSignPos;

		// Coroutine is over, allow it to be activated again (doOnce)
		_bc.enabled = isOpen;
		_signLight.enabled = isOpen;
		_isOpen = isOpen;
	}
    
	public void OpenStore()
	{
		StartCoroutine(SignSlide(0.5f, new Vector3(-1, 0, 0), true));
	}
	
	private void CloseStore()
	{
		StartCoroutine(SignSlide(0.5f, Vector3.zero, false));
	}

}
