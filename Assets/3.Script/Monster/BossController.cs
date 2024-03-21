using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BulletSpawner bulletspawner;

    public int HP;
    public int maxHP;

    private float firerange;
    private bool canfire = true;

    private void Start()
    {
        maxHP = 130;
        HP = maxHP;
    }

    void Update()
    {
        if(CalculateDistnace() <= 15f)
        {
            Move();
        }

        firerange = ProjectManager.instance.gamemanager.player.transform.position.x - this.gameObject.transform.position.x;

        if (firerange < 0.2f && canfire)
        {
            StartCoroutine(FireBulletsWithDelay());
        }
    }

    //보스 이동
    private void Move()
    {
        Vector3 playerposition = new Vector3(ProjectManager.instance.gamemanager.player.transform.position.x, this.transform.position.y, 0);
        Vector3 monsterposition = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        this.transform.position = Vector3.Lerp(monsterposition, playerposition, 0.5f * Time.deltaTime);
    }

    //플레이어와 거리 계산
    private float CalculateDistnace()
    {
        Vector2 playerposition = new Vector2(ProjectManager.instance.gamemanager.player.transform.position.x, ProjectManager.instance.gamemanager.player.transform.position.y);
        Vector2 bossposition = new Vector2(this.transform.position.x, this.transform.position.y);
        float distnace = Mathf.Sqrt(Mathf.Pow(playerposition.x - bossposition.x, 2) + Mathf.Pow(playerposition.y - bossposition.y, 2));

        return distnace;
    }

    private IEnumerator FireBulletsWithDelay()
    {
        canfire = false;

        yield return new WaitForSeconds(3f);
        
        yield return bulletspawner.bullet_co = StartCoroutine(bulletspawner.SpawnBullet_co());
        
        canfire = true;
    }

}
