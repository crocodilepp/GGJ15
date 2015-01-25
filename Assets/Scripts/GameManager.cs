using UnityEngine;
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
//		public float upTime = 5.0f;		
		public float upTimeStep1 = 1.0f;
		public float upTimeStep2 = 3.0f;
		public float upTimeStep3 = 1.0f;
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
		public GameObject screenMask;
		public Animator doorRAnimator;
		public Animator doorLAnimator;

		public GameObject bgFront;
		public GameObject bgLeft;
		public Texture[] bgFrontTextures;
		public Texture[] bgLeftTextures;
		
		public float timeFactor = 0.0f;
		private float curUpTime = 0.0f;
		
		private int   upStep = 0;
		
		void swapBGTexture( Material mat )
		{
			Texture texTop = mat.GetTexture( "TopTex" );
			Texture texDown = mat.GetTexture( "DownTex" );
			mat.SetTexture ("TopTex", texDown );
			mat.SetTexture ("DownTex", texTop );
		}
		
		void setBGTexture( Material mat , Texture texDown , Texture texTop )
		{
			mat.SetTexture ("TopTex", texTop );
			mat.SetTexture ("DownTex", texDown );
		}
		
		void Start () 
		{
			var obj = GameObject.Instantiate(Resources.Load("Prefabs/Npcs")) as GameObject;
			obj.transform.parent = transform;
			foreach(NPC npc in obj.GetComponentsInChildren<NPC>())
			{
				npcList.Add(npc);
			}

			RandomPickNpc();
			player.onDeath += HandleOnDeath;
			StartCoroutine(OpenDoor());
			GameObject.Instantiate(Resources.Load("Prefabs/Fart"));
			setBGTexture( bgFront.renderer.material , bgFrontTextures[0] , bgFrontTextures[1] );
			setBGTexture( bgLeft.renderer.material , bgLeftTextures[0] , bgLeftTextures[1] );
			screenMask = GameObject.Find("ScreenMask");
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
			
			if (upStep > 0 ) 
			{
				curUpTime += Time.deltaTime;
				
				if ( upStep == 1 )
				{
					float f = curUpTime / upTimeStep1;
					timeFactor = f * f;
				}
				else if ( upStep == 2 )
				{
					float loopTime = 6.0f;
					timeFactor = ( loopTime * curUpTime / upTimeStep2 ) % 1.0f;
				}
				else if ( upStep == 3 )
				{
					float f = curUpTime / upTimeStep1;
					timeFactor = (float)System.Math.Sqrt( f );
				}
				
				bgLeft.renderer.material.SetFloat("TimeFactor" , timeFactor);
				bgFront.renderer.material.SetFloat("TimeFactor" , timeFactor);
			}
		}

		public void HandleOnDeath()
		{
			StopAllCoroutines();
			gameOverText.GetComponent<GameOverBoard>().SetLevel(currentFloor);
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
//			floorIndexUI.text = "Up";
			backGroundAnimator.SetTrigger("UpFloor");
			npc.startFart(5.0f);

			
			upStep = 1;
			curUpTime = 0.0f;
			//Debug.LogError("Room up Step =" + upStep );
			yield return new WaitForSeconds(upTimeStep1);
			
			setBGTexture( bgFront.renderer.material , bgFrontTextures[1] , bgFrontTextures[1] );
			setBGTexture( bgLeft.renderer.material , bgLeftTextures[1] , bgLeftTextures[1] );
			
			upStep = 2;
			curUpTime = 0.0f;
			//Debug.LogError("Room up Step =" + upStep );
			yield return new WaitForSeconds(upTimeStep2);
			
			setBGTexture( bgFront.renderer.material , bgFrontTextures[1] , bgFrontTextures[0] );
			setBGTexture( bgLeft.renderer.material , bgLeftTextures[1] , bgLeftTextures[0] );
			upStep = 3;
			curUpTime = 0.0f;
			
			bgLeft.renderer.material.SetFloat("TimeFactor" , 0);
			bgFront.renderer.material.SetFloat("TimeFactor" , 0);
			//Debug.LogError("Room up Step =" + upStep );
			yield return new WaitForSeconds(upTimeStep3);
			
			upStep = 0;
			timeFactor = 0.0f;
			bgLeft.renderer.material.SetFloat("TimeFactor" , timeFactor);
			bgFront.renderer.material.SetFloat("TimeFactor" , timeFactor);
			setBGTexture( bgFront.renderer.material , bgFrontTextures[0] , bgFrontTextures[1] );
			setBGTexture( bgLeft.renderer.material , bgLeftTextures[0] , bgLeftTextures[1] );


			currentFloor += 1;

			audio.Stop();
			audio.loop = false;
			StartCoroutine(PlayDingDong());
		}

		IEnumerator PlayDingDong()
		{
//			floorIndexUI.text = "Ding Dong";
			audio.PlayOneShot(atFloorDingSound);
			yield return new WaitForSeconds(atFloorDingTime);
			StartCoroutine(OpenDoor());
		}

		IEnumerator OpenDoor()
		{
			doorRAnimator.SetTrigger ("openDoor");
			doorLAnimator.SetTrigger ("openDoor");
//			floorIndexUI.text = "Open";
			audio.PlayOneShot(doorOpenSound);
			yield return new WaitForSeconds(doorOpeningTime);
			AtFloor();
		}
		
		public void AtFloor()
		{
			floorIndexUI.text = (currentFloor + 1).ToString();
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
//			floorIndexUI.text = "Close";
			audio.PlayOneShot(doorCloseSound);
			yield return new WaitForSeconds(doorClosingTime);
			npc.HideClue();
			UpFloor();
		}
	}
}

