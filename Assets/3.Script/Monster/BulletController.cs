using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bullet_speed = 3f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.gameObject.transform.position += new Vector3(0, -bullet_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
