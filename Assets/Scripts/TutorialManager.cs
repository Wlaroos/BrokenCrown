using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
	public static TutorialManager Instance;
	
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
	private bool _eBool = false;
	
	public UnityEvent CombatTutorialCompleteEvent;
	public UnityEvent ShopTutorialCompleteEvent;
	public UnityEvent TutorialCompleteEvent;

	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		// Singleton pattern
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
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
			
			StartCoroutine(StaticCoroutines.Fade(1f, sr: _tutorialWASD));
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_w));
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_a));
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_s));
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_d));
			
			Test();
		}
		
		if (Input.GetKeyDown(KeyCode.E) && _e.enabled == false && PlayerStats.Instance.IsShopping) 
		{
			_eBool = true;
			
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_tutorialE));
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_e));
			
			Test();
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && !_mouseBool) 
		{
			_mouseBool = true;
			
			StartCoroutine(StaticCoroutines.Fade(1f, sr:_tutorialLMB));
			
			Test();
		}
		
	}
	
	private void Test()
	{
		if(_eBool)
		{
			ShopTutorialCompleteEvent.Invoke();
		}
		else if(_wasdBool && _mouseBool)
		{
			CombatTutorialCompleteEvent.Invoke();
		}
		else if(_wasdBool && _eBool && _mouseBool)
		{
			TutorialCompleteEvent.Invoke();
			Destroy(gameObject, 1f);
		}
	}

}
