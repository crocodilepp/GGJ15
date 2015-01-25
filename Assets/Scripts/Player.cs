using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GoingUp;

namespace GoingUp
{
public class Player : Actor {
	public delegate void FnOnDeath();
	public delegate void FnOnDamage( float value );
	public FnOnDeath onDeath;
	public FnOnDamage onDamage;

	public List<GameObject> faceList;

	public AudioClip Suffocate;
	public AudioClip Relief;
	public AudioClip Fall;
	private bool mIsBreathing = true;
	public bool IsBreathing
	{
		set
		{
			if(mIsBreathing != value)
			{
				if(mIsBreathing)
				{
					audio.PlayOneShot(Suffocate);
            	}
				else
				{
					audio.PlayOneShot(Relief);
				}
			}
			
			mIsBreathing = value;
		}
	}

	KeyCode keyType = KeyCode.Space;

	public Slider hpSlider;

	public void ShowHpSlider()
	{
		hpSlider.maxValue = hpOriginal;
		hpSlider.value = Hp;
	}

	public Slider o2Slider;

	public void ShowO2Slider()
	{
		o2Slider.maxValue = o2OriginalValue;
		o2Slider.value = o2value;
		float o2perCent = (o2value / o2OriginalValue) ;

//		if(isBreathing())
//		{
//			faceList.ForEach(x => x.SetActive(false));
//			faceList[1].SetActive(true);
//		}
//		else
		{
			
				foreach(GameObject face in faceList)
				{
					if(!face.name.Equals("HotGirl"))
					{
						face.SetActive(false);
					}
				}
				if(o2perCent > 0.8f) 
				{
					faceList[1].SetActive(true);
				}
				else if(o2perCent > 0.6f) 
				{
					faceList[2].SetActive(true);
				}
				else if(o2perCent > 0.4f) 
				{
					faceList[3].SetActive(true);
				}
				else if(o2perCent > 0.2f) 
				{
					faceList[4].SetActive(true);
				}
				else if(o2perCent > -1.0f)
				{
					faceList[5].SetActive(true);
				}
		}
	}

	
	private NPC npc;
		public NPC Npc
		{
			get
			{
				return npc;
			}
			set
			{
				npc = value;
				npc.onStartFart = delegate {
					if(npc.gasType == Gas.Yam && mIsBreathing)
					{
						takeDamage(30f);
					}
				};
				if(npc.name.Equals("Npc2") || npc.name.Equals("Npc4"))
				{
					faceList[0].SetActive(true);
					StartCoroutine(HideHotGirlFace());
				}
			}
		}
	private float hp = 0;
	public float Hp
	{
		get
		{
//			Debug.Log("Hp = " + hp);
			return hp;
		}
		set
		{
			hp = value;
			if(hp > hpOriginal)
			{
				hp = hpOriginal;
			}
		}
	}
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

		private IEnumerator HideHotGirlFace()
		{
			yield return new WaitForSeconds(1f);
			faceList[0].SetActive(false);
		}

	public void initPlayerValue()
	{
		hp = hpOriginal;
		o2value = o2OriginalValue;
	}
	
	public float takeDamage(float damage)
	{		
		if(hp <= 0) return 0;

		Hp -= damage;
		if ( onDamage != null )
		{
			onDamage (damage);
		}
		
		if (hp <= 0) 
		{
			audio.PlayOneShot(Fall);
			if(onDeath != null)
			{
				onDeath();
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
//		Debug.Log ("Recovery O2! (value:"+value+") (player O2:"+o2value+")");
	}

	bool haveGas()
	{
//		return false;
		return npc.isFarting();
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			initPlayerValue();
			Debug.Log("ResetStartGame");
	    }

		if(hp <= 0) return;

		ShowHpSlider();
		ShowO2Slider();
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
				effect(npc);
//				takeDamage(hpLostSpeed * Time.deltaTime);
			}
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
		IsBreathing = onBreating;
		return onBreating;
	}

	public void effect( NPC npc )
	{
		switch (npc.gasType) 
		{
			case Gas.Perfume:
				takeDamage(hpLostSpeed * Time.deltaTime * -1.0f);
				break;
			case Gas.Smoke:
				takeDamage(hpLostSpeed * 0.5f * Time.deltaTime * 2);
				break;
			case Gas.DirtyBody:
				takeDamage(hpLostSpeed * 0.5f * Time.deltaTime * 1.5f);
				break;
			case Gas.StinkingFeet:
				takeDamage(hpLostSpeed * 0.5f * Time.deltaTime);
				break;
		}
	}


}

}
