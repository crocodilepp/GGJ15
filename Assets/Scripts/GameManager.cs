﻿using UnityEngine;
using UnityEngine.UI;

using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace GoingUp
{
	public class GameManager : MonoBehaviour 
	{
		public int currentFloor;
		public int winFloor = 101;
		public float upTime = 5.0f;
		public float intoTime = 1.0f;
		public float goOutTime = 1.0f;
		public float doorOpeningTime = 2.0f;
		public float doorClosingTime = 4.0f;
		public float atFloorDingTime = 2.5f;
		public Text floorIndexUI;
		public List<NPC> npcList;
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
		public GameObject screenMask;
		public Animator doorRAnimator;
		public Animator doorLAnimator;
		
		void Start () 
		{
			player.onDeath += HandleOnDeath;
			StartCoroutine(OpenDoor());
			RandomPickNpc();
			GameObject.Instantiate(Resources.Load("Prefabs/Fart"));
		}

		void RandomPickNpc()
		{
			npc = npcList[Random.Range(0,(npcList.Count() - 1) )];
			npcAnimator = npc.npcAnimator;
			npcAvatar = npc.npcAvatar;
			npcTempAvatar = npc.npcTempAvatar;
			player.Npc = npc;
		}

		void Update()
		{
			Material mat = screenMask.renderer.material;
			float scale = 2 * ( 1 - player.Hp / player.hpOriginal );
			mat.SetFloat( "Scale" , scale );
		}

		public void HandleOnDeath()
		{
			gameOverText.SetActive(true);
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
			doorRAnimator.SetTrigger ("openDoor");
			doorLAnimator.SetTrigger ("openDoor");
			floorIndexUI.text = "Open";
			audio.PlayOneShot(doorOpenSound);
			yield return new WaitForSeconds(doorOpeningTime);
			AtFloor();
		}
		
		public void AtFloor()
		{
			floorIndexUI.text = currentFloor.ToString();
			if (currentFloor == winFloor)
			{
				gameWinText.SetActive(true);
			}

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
			RandomPickNpc();
			npc.gasType = (Gas) Random.Range(0,3);
			Debug.Log("Create A Npc !!" + npc.gasType );
//			npcTempAvatar.GetComponent<Image>().color = theColor;
//			npcAvatar.GetComponent<Image>().color = theColor;
		}

		IEnumerator GoingInto()
		{
//		audio.PlayOneShot(npc.footstepSound);
			npcAnimator.SetTrigger("InBox");
			yield return new WaitForSeconds(intoTime);
			StartCoroutine(CloseDoor());
			npc.ShowClue();
		}

		public void NpcOutBox()
		{
			StartCoroutine(GoingOutSide());
			npcIsInBox = false;
		}
		
		IEnumerator GoingOutSide()
		{
//		audio.PlayOneShot(npc.footstepSound);
			npcAnimator.SetTrigger("OutBox");
			yield return new WaitForSeconds(goOutTime);
			npc = null;
			AtFloor();
		}

		IEnumerator CloseDoor()
		{
			doorRAnimator.SetTrigger ("closeDoor");
			doorLAnimator.SetTrigger ("closeDoor");
			floorIndexUI.text = "Close";
			audio.PlayOneShot(doorCloseSound);
			yield return new WaitForSeconds(doorClosingTime);
			npc.HideClue();
			UpFloor();
		}
	}
}

