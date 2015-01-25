using UnityEngine;
using System.Collections;

public class BGMPlayer : MonoBehaviour 
{
	public void Start()
	{
		Object.DontDestroyOnLoad(gameObject);
	}
}
