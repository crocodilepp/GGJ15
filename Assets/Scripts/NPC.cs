using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoingUp;


namespace GoingUp
{
public class NPC : Actor {

	public delegate void FnOnStartFart( NPC npc );
	public delegate void FnOnFinishFart( NPC npc );
	public FnOnStartFart onStartFart;
	public FnOnFinishFart onFinishFart;

	public Animator npcAnimator;
	public GameObject npcAvatar;
	public GameObject npcTempAvatar;

	public Gas   gasType = Gas.Yam;
	public float fartTotalTime = 1.0f;
	private float fartTime = 0.0f;
	public Clue clue;

	private bool isFarting_ = false;
	// Use this for initialization
	void Start () 
	{
		clue = transform.FindChild("Clue").GetComponent<Clue>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isFarting ()) 
		{
			fartTime += Time.deltaTime;
			if ( fartTime > fartTotalTime )
			{

				finishFart();
			}
		}
	}

	public bool isFarting()
	{
		return isFarting_;
	}
	
	public void startFart( float totalTime )
	{
		if (isFarting_) 
		{
			Debug.Log("Fart Have Started" );
			return;
		}
		Debug.Log ("Start Fart");
		fartTime = 0 ;
		fartTotalTime  = totalTime ;
		isFarting_ = true;

		if ( onStartFart != null )
			onStartFart (this);

		if (gasType == Gas.Yam)
		{
			GameObject.FindGameObjectWithTag("Fart").SendMessage("RandomPlay");
			clue.ShowFart();
		}
	}

	public void finishFart()
	{
		Debug.Log ("Finsh Fart");
		isFarting_ = false;
		if ( onFinishFart != null )
			onFinishFart (this);
		clue.Reset();
	}

	public void ShowClue()
	{
			switch(gasType)
			{
			case Gas.DirtyBody:
				clue.ShowDirtyBody();
				break;
			case Gas.Perfume:
				clue.ShowPerfume();
				break;
			case Gas.Smoke:
				clue.ShowSmoke();
				break;
			case Gas.Yam:
				clue.ShowYam();
				break;
			case Gas.StinkingFeet:
				clue.ShowStinkingFeet();
				break;
			}
	}

	public void HideClue()
	{
		clue.Reset();
	}
}
}
