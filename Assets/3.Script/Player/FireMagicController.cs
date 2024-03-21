using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagicController : MonoBehaviour
{
    private float firemagic_speed = 10f;
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.gameObject.transform.position += new Vector3(0, firemagic_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Boss"))
        {
            Debug.Log("��ų �ݶ��̴� ������?");
            ProjectManager.instance.gamemanager.HitBoss(col);
            ProjectManager.instance.gamemanager.targetmonster = false;
            ProjectManager.instance.gamemanager.targetboss = true;
            Destroy(this.gameObject);
        }
        
        if(col.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
