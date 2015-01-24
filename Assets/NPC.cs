using UnityEngine;
using System.Collections;
using GoingUp;


namespace GoingUp
{
public class NPC : Actor {

	// Use this for initialization
	void Start () 
	{


	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public Gas gasType_;
	public Gas GasType { get{ return gasType_; } set { gasType_ = value; } }  
}
}
