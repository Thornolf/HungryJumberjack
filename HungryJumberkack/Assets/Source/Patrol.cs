using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
    public int speed = 3;
    
    void Start()
    {
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), GameObject.FindGameObjectWithTag("Player").transform.position, speed * Time.deltaTime);
    }
}