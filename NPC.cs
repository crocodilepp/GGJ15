using UnityEngine;
using System.Collections;
using GoingUp;


namespace GoingUp
{
public class NPC : Actor {
	
	private bool isFarting_ = false;
	public Gas gasType_ = Gas.TypeA;
	public Gas GasType { get{ return gasType_; } set { gasType_ = value; } }
	public float fartTotalTime = 1.0f;
	private float fartTime = 0.0f;
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
		return isFart_;
	}
	
	

	public bool startFart( float totalTime )
	{
		Debug.Log ("Start Part");
		if (isFarting_) 
		{

		}

	}

	public bool finishFart()
	{
		Debug.Log ("Start Part");
		isFarting = false;
	}



}
}
