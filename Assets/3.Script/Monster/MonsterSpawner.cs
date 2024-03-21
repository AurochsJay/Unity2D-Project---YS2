using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Monster_Prefabs;
    [SerializeField] private GameObject player;

    //배열을 이용해서 몬스터 3,3,2마리 생성
    private GameObject[] crase;
    private GameObject[] curksharn;
    private GameObject[] godargo;
    private int crase_count = 3;
    private int curksharn_count = 3;
    private int godargo_count = 2;
    private Vector3[] monsterspawnposition = new Vector3[8];
    //private Vector3 monsterspawnposition1 = new Vector3(-52, 72, 0);
    //private Vector3 monsterspawnposition2 = new Vector3(-59, 80, 0);
    //private Vector3 monsterspawnposition3 = new Vector3(-51, 88, 0);
    //private Vector3 monsterspawnposition4 = new Vector3(-34, 88, 0);
    //private Vector3 monsterspawnposition5 = new Vector3(-52, 81, 0);
    //private Vector3 monsterspawnposition6 = new Vector3(-43, 83, 0);
    //private Vector3 monsterspawnposition7 = new Vector3(-14, 75, 0);
    //private Vector3 monsterspawnposition8 = new Vector3(-13, 75, 0);

    private void Awake()
    {
        crase = new GameObject[crase_count];
        curksharn = new GameObject[curksharn_count];
        godargo = new GameObject[godargo_count];
        monsterspawnposition[0] = new Vector3(-52, 72, 0);
        monsterspawnposition[1] = new Vector3(-59, 80, 0);
        monsterspawnposition[2] = new Vector3(-51, 88, 0);
        monsterspawnposition[3] = new Vector3(-34, 88, 0);
        monsterspawnposition[4] = new Vector3(-52, 81, 0);
        monsterspawnposition[5] = new Vector3(-43, 83, 0);
        monsterspawnposition[6] = new Vector3(-14, 75, 0);
        monsterspawnposition[7] = new Vector3(-13, 75, 0);


    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
    }

    //플레이어와 스포너 위치가 13이상이면 리스폰된다.
    private void CheckPlayerDistance()
    {
        Vector3 playerposition = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        float[] distance = new float[8];
        for (int i = 0; i < 8; i++)
        {
            distance[i] = CalculateDistance(playerposition, monsterspawnposition[i]);
        }

        for(int i = 0; i < 8; i++)
        {
            if(distance[i] >= 13)
            {
                StartCoroutine(SpawnMonster_co(i));
            }
        }
    }

    //플레이어와 스포너 거리계산
    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(point1.x - point2.x, 2) + Mathf.Pow(point1.y - point2.y, 2));
        return distance;
    }

    //스폰 코루틴
    private IEnumerator SpawnMonster_co(int i)
    {
        //WaitForSeconds wfs = new WaitForSeconds(0.5f);

        if(i>=0 && i<=2)
        {
            if(crase[i] == null)
            {
                crase[i] = Instantiate(Monster_Prefabs[0], monsterspawnposition[i], Quaternion.identity);
            }
        }
        else if(i >=3 && i<=5)
        {
            if(curksharn[i-3] == null)
            {
                curksharn[i-3] = Instantiate(Monster_Prefabs[1], monsterspawnposition[i], Quaternion.identity);
            }
        }
        else if(i >= 6 && i <=7)
        {
            if(godargo[i-6] == null)
            {
                godargo[i - 6] = Instantiate(Monster_Prefabs[2], monsterspawnposition[i], Quaternion.identity);
            }
        }

        //yield return wfs;
        yield break;
    }
}
