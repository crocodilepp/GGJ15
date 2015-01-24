using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour 
{
	public float delay;

	public void DelayLoad(string name)
	{
		StartCoroutine(Load(name));
	}

	IEnumerator Load(string name)
	{
		yield return new WaitForSeconds(delay);
		Application.LoadLevel(name);
	}
}
