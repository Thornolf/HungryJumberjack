using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using Source;
using UnityEngine.UI;
using UnityEngineInternal;

public class Player : MonoBehaviour
{

	public int Health;
	public int Damage;
	public int Fatness;
	public double Speed;
	public Consumable LiftingItem;
	public String Weapon;
	public Boolean IsAlive;
	public Sprite sprite;
	public Transform sightStart;
	public Transform sightEnd;
	public Boolean Spotted;
	public double nextAttackAllowed = 0.2f;
	public double cooldown= 0.2f;
	public Slider sliderFatness;
	public Slider sliderHealth;
	public Text healthText;
	public Text fatnessText;
	private float nextActionTime = 0.0f;
	public float period = 10f;
	public Text timerDisplay;
	public AnimationClip anim;
	
	Player()
	{
		Health = 200;
		Damage = 10;
		Speed = 5f;
		Fatness = 125;
		Weapon = "Axe";
		IsAlive = true;
	}
	
	/*
	 * Fatness :
	 * 0 - 50 (Very Skinny)
	 * 50 - 100 (Skinny)
	 * 100 - 150 (Normal)
	 * 150 - 200 (Fat)
	 * 200 - 250 (Veryfat)
	 *
	 * 0 ou 250 = mort
	 */

	void Start()
	{
		Health = 200;
		Damage = 10;
		Speed = 5f;
		Fatness = 125;
		Weapon = "Axe";
		IsAlive = true;
		
		sliderFatness.value = Fatness;
		sliderHealth.value = Health;
		healthText.text = Health.ToString();
		fatnessText.text = Fatness.ToString();
	}

	public void takeDamage(int damage)
	{
		Health -= damage;
	}

	public void Attack(Animal animal)
	{
		//TODO géré les dégats selon la fatness
		animal.takeDamage(Damage);
	}

	public void Healing(int heal)
	{
		if ((Health + heal) <= 200)
		{
			Health += heal;
		}
	}

	public void Fatupdate(int fat)
	{
		if ((Fatness + fat) <= 250)
		{
			Fatness += fat;
		}
		else
		{
			Dying();
		}
	}
	public void Eat(Consumable consomable)
	{
		Healing(consomable.HealthRestoring);
		Fatupdate(consomable.Fat);
	}

	public void Dying()
	{
		//TODO Affiché le splash screen
		Debug.Log("Player is dead");
		Application.LoadLevel("Menu");
	}

	public void updateStatus()
	{
		if (Fatness >= 0 && Fatness <= 50)
		{
			Healing(-10);
			Speed = 7f;
		}
		if (Fatness > 50 && Fatness <= 200)
		{
			Healing(-1);
			Speed = 5f;
		}
		if (Fatness > 200)
		{
			Healing(-20);
			Speed = 3f;
		}
		Fatupdate(-10);
	}
	
	void Update ()
	{
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.cyan);
		if (Health <= 0)
			Dying();
		if (Input.GetKeyDown("e"))
		{
			//gameObject.GetComponent<Animation>().Play("Attack");
			
			Spotted = Physics2D.Linecast(sightEnd.position, sightStart.position, 1 << LayerMask.NameToLayer("Enemy"));
		
			if (Spotted == true)
			{
				var colliderGameObject = Physics2D.Linecast(sightEnd.position, sightStart.position, 1 << LayerMask.NameToLayer("Enemy")).collider.gameObject;
				if (colliderGameObject != null)
				{
					//GameObject gb = colliderGameObject;
					Animal animal = colliderGameObject.GetComponent<Animal>();
					if (Time.time > nextAttackAllowed)
					{
						Attack(animal);
						nextAttackAllowed = Time.time + cooldown;
					}
					Debug.Log(animal.Health);
				}
			}
		}
		if (Time.time > nextActionTime ) {
			nextActionTime += period;
			updateStatus();
		}
		sliderFatness.value = Fatness;
		sliderHealth.value = Health;
		healthText.text = Health.ToString();
		fatnessText.text = Fatness.ToString();
		timerDisplay.text = ((int) Time.timeSinceLevelLoad).ToString() + " seconds"; // TODO Fix timer don't start at the beginning of the scene
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}