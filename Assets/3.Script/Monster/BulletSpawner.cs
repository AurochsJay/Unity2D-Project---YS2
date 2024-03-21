using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //Bullet ÇÁ¸®Æé
    [SerializeField] private GameObject Bullet_Large_Prefabs;
    [SerializeField] private GameObject Bullet_Small_Prefabs;
    [SerializeField] private GameObject Boss;

    private GameObject[] largebullets;
    private GameObject[] smallbullets;

    private Vector3[] largebulletspos = new Vector3[2];
    private Vector3[] smallbulletspos = new Vector3[4];

    public Coroutine bullet_co;

    void Start()
    {
        largebullets = new GameObject[2];
        smallbullets = new GameObject[4];
    }

    private void Update()
    {
        largebulletspos[0] = new Vector3(Boss.transform.position.x - 0.5f, Boss.transform.position.y - 1f, 0);
        largebulletspos[1] = new Vector3(Boss.transform.position.x + 0.5f, Boss.transform.position.y - 1f, 0);
        smallbulletspos[0] = new Vector3(Boss.transform.position.x + 1f, Boss.transform.position.y - 1f, 0);
        smallbulletspos[1] = new Vector3(Boss.transform.position.x + 1.5f, Boss.transform.position.y - 1f, 0);
        smallbulletspos[2] = new Vector3(Boss.transform.position.x - 1f, Boss.transform.position.y - 1f, 0);
        smallbulletspos[3] = new Vector3(Boss.transform.position.x - 1.5f, Boss.transform.position.y - 1f, 0);
    }

    public IEnumerator SpawnBullet_co()
    {
        yield return null;

        for (int i = 0; i < largebullets.Length; i++)
        {
            largebullets[i] = Instantiate(Bullet_Large_Prefabs, largebulletspos[i], Quaternion.identity);
        }

        for (int i = 0; i < smallbullets.Length; i++)
        {
            smallbullets[i] = Instantiate(Bullet_Small_Prefabs, smallbulletspos[i], Quaternion.identity);
        }

    }
}
