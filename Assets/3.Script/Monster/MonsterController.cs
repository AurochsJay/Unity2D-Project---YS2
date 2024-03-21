using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private int level;
    public int maxHP;
    public int HP;
    public int ATK;
    public int DEF;
    public int EXP;
    public int Gold;

    //플레이어와의 거리를 알기 위해 플레이어 정보를 가져와야한다.
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gamemanager;

    private void Start()
    {
        //gamemanager = GetComponent<GameManager>();

        if(this.name == "Crase")
        {
            level = 2;
            maxHP = 20;
            HP = 20;
            ATK = 20;
            DEF = 6;
            EXP = 5;
            Gold = 8;
            Debug.Log("Crase");
        }
        else if(this.name == "Curksharn")
        {
            level = 4;
            maxHP = 30;
            HP = 30;
            ATK = 32;
            DEF = 10;
            EXP = 5;
            Gold = 10;
            Debug.Log("Curksharn");
        }
        else if(this.name == "Godargo")
        {
            level = 8;
            maxHP = 50;
            HP = 50;
            ATK = 56;
            DEF = 28;
            EXP = 6;
            Gold = 12;
            Debug.Log("Godargo");
        }

        //몬스터 리스폰시 변수 초기화
        player = GameObject.FindGameObjectWithTag("Player");
        gamemanager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
        if (gamemanager.ishitmonster)
        {

        }
        else
        {
            Move();
            //Hit();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        gamemanager = GameObject.FindObjectOfType<GameManager>();
        //player = player.GetComponent<GameObject>();

    }

    //플레이어가 시야에 들어오면 그쪽 방향으로 간다.
    private void Move()
    {
        Vector3 playerposition = new Vector3(player.transform.position.x, player.transform.position.y,0);
        Vector3 monsterposition = new Vector3(this.transform.position.x, this.transform.position.y,0);
        float distance = CalculateDistance(playerposition, monsterposition);

        if(distance < 6)
        {
            this.transform.position = Vector3.Lerp(monsterposition, playerposition, 0.5f * Time.deltaTime);
        }
        else
        {
            //int randx = Random.Range(-1, 1);
            //int randy = Random.Range(-1, 1);
            //Vector3 moveposition = new Vector3(randx, randy, 0);
            //this.transform.position += moveposition;
        }
    }

    //맞았을때 데미지, 넉백(위치조정)
    private void Hit()
    {

    }

    //플레이어와 몬스터의 거리계산
    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(point1.x - point2.x, 2) + Mathf.Pow(point1.y - point2.y, 2));
        return distance;
    }

}
