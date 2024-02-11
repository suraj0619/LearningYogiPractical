using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("*** Control Inputs ***")]
    public float movementSpeed;
    public float rotationSpeed;
    public int health;

    [Header("*** Bullet Prefab ***")]
    public GameObject BulletPrefab;

    void Update()
    {
        // Ship Movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector2.up);
        }

        // Ship Rotation - Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        // Ship Rotation - Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        // Bullet Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(InstantiateBullet());
        }
    }

    #region Instantiating Bullet on Space btn click
    IEnumerator InstantiateBullet()
    {
        for (int i = 0; i < 3; i++)
        {
            //GameObject gb = Instantiate(BulletPrefab, new Vector3(pos.x, pos.y, pos.x), Quaternion.identity);
            GameObject gb = Instantiate(BulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
            gb.transform.rotation = this.transform.rotation;
            gb.transform.localScale = Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            health--;
            GameController.inst.HealthTxt.text = "Health\n" + health;
            collision.transform.localPosition = new Vector3(-10, -10, 0);

            if (health <= 0)
            {
                StopAllCoroutines();
                GameController.inst.ShowResult("lost", GameController.inst.ScoreTxt.text);
            }
        }
    }
}
