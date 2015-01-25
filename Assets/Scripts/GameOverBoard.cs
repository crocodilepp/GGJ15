using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoingUp;

public class GameOverBoard : MonoBehaviour 
{
	public Animator animator;
	public Image panel;
	public Text levelText;
	public Text bestText;

	int bestLevel = 0;

//	GameManager gm;
//
//	public void Awake()
//	{
//		Debug.Log("GameOverBoard awake");
//		gm = GameObject.FindObjectOfType<GameManager>();
//		gm.player.onDeath += HanledPlayerOnDeath;
//	}
//
//	void HanledPlayerOnDeath()
//	{
//		Debug.Log("HanledPlayerOnDeath");
//		SetLevel(gm.currentFloor);
//		gameObject.SetActive(true);
//	}

	public void SetLevel(int level)
	{
		if (level > bestLevel)
		{
			bestLevel = level;
			bestText.text = level.ToString();
		}

		levelText.text = level.ToString();
	}
}
