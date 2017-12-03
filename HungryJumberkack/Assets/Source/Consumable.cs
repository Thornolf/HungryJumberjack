using System;
using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{

	public int Fat;
	public int HealthRestoring;
	public int Weight;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "Player")
		{
			GameObject obj = other.gameObject;
			Player player = obj.GetComponent<Player>();
			player.Eat(this);
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
/*
		Health += consomable.HealthRestoring;
		Fatness += consomable.Fat;
		*/