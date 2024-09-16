using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	[SerializeField] SpriteRenderer _tutorialWASD;
	[SerializeField] SpriteRenderer _tutorialLMB;
	[SerializeField] SpriteRenderer _tutorialE;
	[SerializeField] SpriteRenderer _w;
	[SerializeField] SpriteRenderer _a;
	[SerializeField] SpriteRenderer _s;
	[SerializeField] SpriteRenderer _d;
	[SerializeField] SpriteRenderer _e;
	
	private bool _wasdBool = false;
	private bool _mouseBool = false;
	private int _num = 0;

	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	private void Update()
	{
		if(_w.enabled == false || _a.enabled == false || _s.enabled == false || _d.enabled == false)
		{
			if (Input.GetKeyDown(KeyCode.W) && _w.enabled == false) 
			{
				_w.enabled = true;
			}
		
			if (Input.GetKeyDown(KeyCode.A) && _a.enabled == false) 
			{
				_a.enabled = true;
			}
		
			if (Input.GetKeyDown(KeyCode.S) && _s.enabled == false) 
			{
				_s.enabled = true;
			}
		
			if (Input.GetKeyDown(KeyCode.D) && _d.enabled == false) 
			{
				_d.enabled = true;
			}
		}
		else if(!_wasdBool)
		{
			_wasdBool = true;
			_num++;
			
			StartCoroutine(StaticCoroutines.Fade(1f, _tutorialWASD));
			StartCoroutine(StaticCoroutines.Fade(1f, _w));
			StartCoroutine(StaticCoroutines.Fade(1f, _a));
			StartCoroutine(StaticCoroutines.Fade(1f, _s));
			StartCoroutine(StaticCoroutines.Fade(1f, _d));
			
			Test();
		}
		
		if (Input.GetKeyDown(KeyCode.E) && _e.enabled == false) 
		{
			_e.enabled = true;
			_num++;
			
			StartCoroutine(StaticCoroutines.Fade(1f, _tutorialE));
			StartCoroutine(StaticCoroutines.Fade(1f, _e));
			
			Test();
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && !_mouseBool) 
		{
			_mouseBool = true;
			_num++;
			
			StartCoroutine(StaticCoroutines.Fade(1f, _tutorialLMB));
			
			Test();
		}
		
	}
	
	private void Test()
	{
		if(_num >= 3)
		{
			Destroy(gameObject, 1f);
		}
	}

}
