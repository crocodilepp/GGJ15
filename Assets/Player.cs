using UnityEngine;
using System.Collections;
using GoingUp;

namespace GoingUp
{
public class Player : Actor {
	KeyCode keyType = KeyCode.Space;
	private	static float currentlyHp = 0;
	private static float originalHp = 100;

		private float hp_;
		private float o2value_;
		public  float o2LostSpeed;

		public float HP { get { return hp_; } set { hp_ = value; } }
		public float O2Value { get { return o2value_; } set { o2value_ = value; } }

	void Update () {

			if (isBreathing ()) 
			{
						
			}


	
	}


	public bool isBreathing()
	{
		bool onBreating = false;
		//get input
		if (Input.touchCount > 0 || Input.GetKey (keyType)) {
			onBreating = true;
			Debug.Log ("Info Breathing(" + onBreating+")");
		}
		return onBreating;
	}


	public void effect( NPC npc )
	{


	}


}

}
