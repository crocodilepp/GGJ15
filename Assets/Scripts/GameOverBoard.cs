using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverBoard : MonoBehaviour 
{
//	public float upSpeed = 10.0f;
	public Image panel;
	public Text scoreText;
	public Text bestText;
//
//	public void Start()
//	{
////		SlideUp();
//	}
//
//	public void Update()
//	{
//		if (panel.rectTransform.position.y > 0)
//		{
//			panel.rectTransform.Translate(Vector3.up * Time.deltaTime * upSpeed);
//		}
//	}
//
//	public void SlideUp()
//	{
//		StartCoroutine(PanelMoveUp());
//	}
//
//	IEnumerator PanelMoveUp()
//	{
//		while (panel.transform.position.y > 0)
//		{
//			yield return null;
//			panel.transform.Translate(Vector3.up * Time.deltaTime * upSpeed);
//		}
//	}
}
