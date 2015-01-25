using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkText : MonoBehaviour 
{
	public float blinkSpeed = 0.5f;
	Text _text;
	string tempText;

	void Awake()
	{
		_text = GetComponent<Text>();
		tempText = _text.text;
	}

	void Start()
	{
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		while (true)
		{
			yield return new WaitForSeconds(blinkSpeed);
			_text.text = "";

			yield return new WaitForSeconds(blinkSpeed);
			_text.text = tempText;
		}
	}

	public void ChangeSpeed(float speed)
	{
		StopCoroutine(Blink());
		blinkSpeed = speed;
		StartCoroutine(Blink());
	}
}
