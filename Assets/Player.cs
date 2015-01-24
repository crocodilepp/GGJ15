using UnityEngine;
using System.Collections;
using GoingUp;

namespace GoingUp
{
public class Player : Actor {
	KeyCode keyType = KeyCode.Space;

	public float hp_;
	public float hpOriginal = 100;
	public  float hpLsotSpeed;
	public float o2value_;
	public  float o2LostSpeed;
	public float o2OriginalValue = 100;


	public void initPlayerValue()
	{
		hp_ = hpOriginal;
		o2value_ = o2OriginalValue;
	}
	
	public float takeHpDamage(float damage)
	{
		hp_ -= damage;
		if (hp_ >= 0) {
				initPlayerValue();
			}
			return hp_;
	}

	


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
