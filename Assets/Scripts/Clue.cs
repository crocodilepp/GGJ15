using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clue : MonoBehaviour {

	private GameObject mSmoke;
	private GameObject mYam;
	private GameObject mPerfume;
	private GameObject mFart;
	private GameObject mStinkingFeet;
	private GameObject mDirtyBody;
	private List<GameObject> mClues = new List<GameObject>();

	// Use this for initialization
	void Start () {
		mSmoke = transform.FindChild("Smoke").gameObject;
		mYam = transform.FindChild("Yam").gameObject;
		mPerfume = transform.FindChild("Perfume").gameObject;
		mFart = transform.FindChild("Fart").gameObject;
		mStinkingFeet = transform.FindChild("StinkingFeet").gameObject;
		mDirtyBody = transform.FindChild("DirtyBody").gameObject;

		mClues.Add(mSmoke);
		mClues.Add(mYam);
		mClues.Add(mPerfume);
		mClues.Add(mFart);
		mClues.Add(mStinkingFeet);
		mClues.Add(mDirtyBody);
	}

	public void Reset()
	{
		foreach(GameObject obj in mClues)
		{
			obj.SetActive(false);
		}
	}

	public void ShowSmoke()
	{
		Reset();
		mSmoke.SetActive(true);
	}

	public void ShowYam()
	{
		Reset();
		mYam.SetActive(true);
	}

	public void ShowPerfume()
	{
		Reset();
		mPerfume.SetActive(true);
	}

	public void ShowFart()
	{
		Reset();
		mFart.SetActive(true);
		StartCoroutine(HideFart());
	}

	private IEnumerator HideFart()
	{
		yield return new WaitForSeconds(0.5f);
		Reset();
	}

	public void ShowStinkingFeet()
	{
		Reset();
		mStinkingFeet.SetActive(true);
	}

	public void ShowDirtyBody()
	{
		Reset();
		mDirtyBody.SetActive(true);
	}
}
