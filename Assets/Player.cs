using UnityEngine;
using System.Collections;
using GoingUp;

namespace GoingUp
{
public class Player : Actor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			if (isBreathing ()) 
			{

						
			}


	
	}


		public bool isBreathing()
		{
			return true;
		}

	public void effect( NPC npc )
	{

	}

	private float hp_;
	private float o2value_;
	public  float o2LostSpeed;
	



	public float HP { get { return hp_; } set { hp_ = value; } }
	public float O2Value { get { return o2value_; } set { o2value_ = value; } }
}

}
