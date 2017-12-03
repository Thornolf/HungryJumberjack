using System;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public int Health;
    public int Damage;
    public Boolean IsAlive;
    public double Speed;
    protected Sprite sprite;
    
    public Transform sightStart;
    public Transform sightEnd;
    public Boolean Spotted;
    public double nextAttackAllowed = 2f;
    public double cooldown = 2f;
    public GameObject loot;

    public Animal()
    {
        IsAlive = true;
        Health = 30;
        Damage = 15;
        Speed = 1f;
    }
    public void Attack(Player player)
    {
        player.takeDamage(Damage);
    }

    public void takeDamage(int damage)
    {
        Health -= damage;
    }
    public void Move()
    {
    
    }

    public void Eat(Consumable consomable)
    {
        Destroy(consomable); //TODO vérifié que ça fonctionne avec le Destroy (Possibilité de delay
    }

    protected void FindFood()
    {
        //TODO algo pour truver la bouffe
    }

    public void Dying()
    {
        Instantiate(loot, this.gameObject.transform.position, Quaternion.Euler(0, 180, 0));
        
        Destroy(this.gameObject);
    }

    void Start()
    {
    }

    void Update()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.cyan);
        if (Health <= 0)
            Dying();

        if (Time.time > nextAttackAllowed)
        {
            Spotted = Physics2D.Linecast(sightEnd.position, sightStart.position, 1 << LayerMask.NameToLayer("Player"));

            if (Spotted == true)
            {
                var colliderGameObject = Physics2D
                    .Linecast(sightEnd.position, sightStart.position, 1 << LayerMask.NameToLayer("Player")).collider
                    .gameObject;
                if (colliderGameObject != null)
                {
                    //GameObject gb = colliderGameObject;
                    Player player = colliderGameObject.GetComponent<Player>();
                    Attack(player);
                    nextAttackAllowed = Time.time + cooldown;
                }
            }
        }
    }
}
