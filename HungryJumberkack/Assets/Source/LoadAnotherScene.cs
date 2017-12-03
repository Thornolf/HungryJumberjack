using UnityEngine;
using System.Collections;

public class LoadAnotherScene : MonoBehaviour
{

	private double time = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		if (time > 2f)
		{
			if (Input.anyKey)
				Application.LoadLevel("Level1");
		}
	}
}
