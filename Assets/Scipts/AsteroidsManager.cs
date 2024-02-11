using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    public enum AsteroidsType
    {
        small,
        large
    }

    public int speed;
    public int health;
    public AsteroidsType asteroidsType;
    
    Transform target;
    Rigidbody2D rb2d;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();

        rb2d = GetComponent<Rigidbody2D>();

        if (asteroidsType == AsteroidsType.large)
            rb2d.AddForce(transform.up * speed);
    }

    void Update()
    {
        // Checking Y position
        if (transform.localPosition.y >= 4.5)
        {
            transform.position = new Vector3(transform.position.x, -2, transform.position.z);
            return;
        }
        else if (transform.localPosition.y <= -5.5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }

        // Checking X position
        if (transform.localPosition.x <= -9)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);
            return;
        }
        else if (transform.localPosition.x >= 9)
        {
            transform.position = new Vector3(-8, transform.position.y, transform.position.z);
        }

        // Moving Small Asteroids Towards Player
        if (asteroidsType == AsteroidsType.small)
            transform.position = Vector2.MoveTowards(transform.position, target.position, 1 * Time.deltaTime);
    }
}
