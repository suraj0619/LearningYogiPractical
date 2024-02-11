using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;

    void Start()
    {
        //GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * bulletSpeed);
        Destroy(this.gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);

        if(collision.transform.CompareTag("Enemy"))
        {
            AsteroidsManager inst = collision.transform.GetComponent<AsteroidsManager>();
            inst.health--;

            if(inst.health <= 0)
            {
                GameController.inst.DestroyEnemies(inst, collision);
            }            
        }
    }
}
