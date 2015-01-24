using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkText : MonoBehaviour 
{
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
			yield return new WaitForSeconds(0.5f);
			_text.text = "";

			yield return new WaitForSeconds(0.5f);
			_text.text = tempText;
		}
	}
}
