using UnityEngine;
using System.Collections;
using GoingUp;

namespace GoingUp
{
public class Player : Actor {
	public delegate void FnOnDeath( Player player);
	public delegate void FnOnDamage( Player player , float value );
	FnOnDeath onDeath;
	FnOnDamage onDamage;

	KeyCode keyType = KeyCode.Space;
	
	public NPC npc;
	public float hp = 0;
	public float hpOriginal = 100;
	public  float hpLostSpeed = 10;
	public float o2value = 0;
	public  float o2LostSpeed = 10;
	public float o2OriginalValue = 100;
	public float o2RecoverySpeed = 10;

	void Start()
	{
		initPlayerValue ();
	}

	public void initPlayerValue()
	{
		hp = hpOriginal;
		o2value = o2OriginalValue;
	}
	
	public float takeDamage(float damage)
	{		
		hp -= damage;
		if ( onDamage != null )
		{
			onDamage (this, damage);
		}
		
		if (hp <= 0) 
		{
			if(onDeath != null)
			{
				onDeath(this);
				hp = 0;
			}
			Debug.Log ("player is death by hp");
		}
		Debug.Log ("Take HpDamage! (damage:"+damage+") (player hp:"+hp+")");
		return hp;
	}
	public float LowerO2( float value )
	{
		o2value -= value;
		if (o2value <= 0) 
		{
			takeDamage(hpLostSpeed * Time.deltaTime);
			o2value = 0;
		}
		Debug.Log ("Take LowerO2! (value:"+value+") (player O2:"+o2value+")");
		return hp;
	}

	public void RecoveryO2(float value)
	{
		bool playerDeath = hp <= 0;

		if (!playerDeath) 
		{
			bool maxO2 = o2value <= o2OriginalValue;
			if (maxO2) 
			{	
				o2value += value;
			} 
			else 
			{
				o2value =o2OriginalValue;
			}
		} 
		Debug.Log ("Recovery O2! (value:"+value+") (player O2:"+o2value+")");
	}

	bool haveGas()
	{
		return false;
		//return npc.isFarting;
	}
	void Update () 
	{
		if (!isBreathing ()) 
		{
			LowerO2 (o2LostSpeed * Time.deltaTime);
		} 
		else 
		{
			if(!haveGas())
			{
				RecoveryO2(o2RecoverySpeed * Time.deltaTime);
			}
			else
			{
				takeDamage(hpLostSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			initPlayerValue();
			Debug.Log("ResetStartGame");
		}
	}


	public bool isBreathing()
	{
		bool onBreating = true;
		//get input
		if (Input.touchCount > 0 || Input.GetKey (keyType)) {
			onBreating = false;
			Debug.Log ("Info Breathing(" + onBreating+")");
		}
		return onBreating;
	}

	

	public void effect( NPC npc )
	{
		switch (npc.GasType) 
		{
			case Gas.TypeA:
				break;
			case Gas.TypeB:
				break;
			case Gas.TypeC:
				break;
		}
	}


}

}
