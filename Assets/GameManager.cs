﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GoingUp
{
	public class GameManager : MonoBehaviour 
	{
		
		public int currentFloor;
		public float upTime = 5.0f;
		public float intoTime = 1.0f;
		public float goOutTime = 1.0f;
		public float doorOpeningTime = 2.0f;
		public float doorClosingTime = 4.0f;
		public float atFloorDingTime = 2.5f;
		public Text floorIndexUI;
		public NPC npc;
		public Player player;
		public Animator backGroundAnimator;
		public Animator npcAnimator;
		public GameObject npcAvatar;
		public GameObject npcTempAvatar;
		public GameObject gameOverText;
		public GameObject gameWinText;
		public bool npcIsInBox = false;
		public AudioClip doorOpenSound;
		public AudioClip doorCloseSound;
		public AudioClip elevatorMovingSound;
		public AudioClip atFloorDingSound;
		public AudioClip footstepSound;
		
		void Start () 
		{
			StartCoroutine(OpenDoor());

		}
		
		void Update () 
		{
			
		}



		public void UpFloor()
		{
			StartCoroutine(Uping());
			StopCoroutine(GoingInto());
			StopCoroutine(CloseDoor());
		}
		
		IEnumerator Uping()
		{
			audio.clip = elevatorMovingSound;
			audio.loop = true;
			audio.Play();
			floorIndexUI.text = "Up";
			backGroundAnimator.SetTrigger("UpFloor");
			npc.startFart(5.0f);
			yield return new WaitForSeconds(upTime);
			currentFloor += 1;

			audio.Stop();
			audio.loop = false;
			StartCoroutine(PlayDingDong());
		}

		IEnumerator PlayDingDong()
		{
			floorIndexUI.text = "Ding Dong";
			audio.PlayOneShot(atFloorDingSound);
			yield return new WaitForSeconds(atFloorDingTime);
			StartCoroutine(OpenDoor());
		}

		IEnumerator OpenDoor()
		{
			floorIndexUI.text = "Open";
			audio.PlayOneShot(doorOpenSound);
			yield return new WaitForSeconds(doorOpeningTime);
			AtFloor();
		}
		
		public void AtFloor()
		{
			floorIndexUI.text = currentFloor.ToString();
			StopCoroutine(Uping());
			StopCoroutine(OpenDoor());
			if(npcIsInBox)
			{
				NpcOutBox();
			}
			else
			{
				NpcInBox();
			}
		}

		public void NpcInBox()
		{
			MakeNpc();
			StartCoroutine(GoingInto());
			npcIsInBox = true;
		}

		public void MakeNpc()
		{
			npc.gasType = (Gas) Random.Range(0,3);
			Debug.LogWarning("Create A Npc !!" + npc.gasType );
			
//			npcTempAvatar.GetComponent<Image>().color = theColor;
//			npcAvatar.GetComponent<Image>().color = theColor;
		}

		IEnumerator GoingInto()
		{
//			audio.PlayOneShot(npc.footstepSound);
			npcAnimator.SetTrigger("InBox");
			yield return new WaitForSeconds(intoTime);
			StartCoroutine(CloseDoor());
		}

		public void NpcOutBox()
		{
			StartCoroutine(GoingOutSide());
			npcIsInBox = false;
		}
		
		IEnumerator GoingOutSide()
		{
//			audio.PlayOneShot(npc.footstepSound);
			npcAnimator.SetTrigger("OutBox");
			yield return new WaitForSeconds(goOutTime);
			AtFloor();
		}

		IEnumerator CloseDoor()
		{
			floorIndexUI.text = "Close";
			audio.PlayOneShot(doorCloseSound);
			yield return new WaitForSeconds(doorClosingTime);
			UpFloor();
		}
	}
}
