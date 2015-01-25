﻿using UnityEngine;
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

	public Gas   gasType = Gas.TypeA;
	public float fartTotalTime = 1.0f;
	private float fartTime = 0.0f;


	private bool isFarting_ = false;
	// Use this for initialization
	void Start () 
	{
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

		if (gasType == Gas.TypeA)
			GameObject.FindGameObjectWithTag("Fart").SendMessage("RandomPlay");
	}

	public void finishFart()
	{
		Debug.Log ("Finsh Fart");
		isFarting_ = false;
		if ( onFinishFart != null )
			onFinishFart (this);
	}



}
}
