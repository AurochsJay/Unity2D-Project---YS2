using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject entrance;
    [SerializeField] public BossController boss;
    //private PlayerController playerController;
    private MonsterController monster;

    //파이어매직
    [SerializeField] private FireMagicSpawner firemagicspawner;

    //플레이어 정보
    public int level;
    public int ATK;
    public int DEF;
    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;
    public int currentEXP;
    public int maxEXP;
    public int Gold;

    //플레이어 장비 정보
    public Sword sword;
    public Shield shield;
    public Armor armor;
    public Magic magic;
    public Accessory accessory;
    public Item item;

    //플레이어가 충돌한 녀석의 몬스터 정보를 갖고와야한다. UI에 표시해주기 위해, save,load 하기위해
    public int monstermaxHP;
    public int monsterHP;
    public bool targetmonster = false; // 몬스터가 지정되지 않았다면 UI에 띄우면 안됨.
    public bool targetboss = false;

    //이전 위치
    public Vector3 previousposition;

    //넉백변수
    public float knockbackduration = 1f; //넉백시간
    public float knockbacktimer; // 넉백시간 계산
    public float knockbackforce = 100f;

    //플레이어가 맞았는지
    public bool ishitplayer = false;

    //몬스터가 맞았는지
    public bool ishitmonster = false;

    //코루틴 변수
    public Coroutine combattomonster_co;
    public Coroutine combattoplayer_co;

    private void Awake()
    {
        //player = GetComponent<GameObject>();
        //playerController = new PlayerController();
        //playerController = GetComponent<PlayerController>();
        monster = GetComponent<MonsterController>();

        // playerController가 null인지 확인
        if (playerController == null)
        {
            Debug.LogError("PlayerController가 찾을 수 없습니다. player 게임 오브젝트에 PlayerController 컴포넌트가 연결되어 있는지 확인하세요.");
        }

        previousposition = player.transform.position;

        //플레이어 스텟 초기화
        PlayerStatInitialize();

        
    }

    private void Update()
    {
        if (ProjectManager.instance.istitle == true)
        {
            //ProjectManager.instance.FindGameObject();

            ProjectManager.instance.istitle = false;
            ProjectManager.instance.isvillage = true;
            ProjectManager.instance.isruin = false;
            ProjectManager.instance.isbossroom = false;
            ProjectManager.instance.cameracontroller.audiosource.Stop();
            Gold += 300;
            //ProjectManager.instance.cameracontroller.audiosource.Play();
            //ProjectManager.instance.datamanager.LoadGameDataFromJson();
            //ProjectManager.instance.datamanager.DataFileToGameManager();
        }

        if (ProjectManager.instance.istitleload == true)
        {
            //ProjectManager.instance.FindGameObject();
            ProjectManager.instance.istitleload = false;
            //ProjectManager.instance.cameracontroller.audiosource.Stop();
            ProjectManager.instance.datamanager.LoadGameDataFromJson();
            ProjectManager.instance.datamanager.DataFileToGameManager();
            // ProjectManager.instance.cameracontroller.audiosource.Play();
        }

        if (ProjectManager.instance.isgameoverload == true)
        {
            //ProjectManager.instance.FindGameObject();
            ProjectManager.instance.isgameoverload = false;
            //ProjectManager.instance.cameracontroller.audiosource.
            ProjectManager.instance.datamanager.LoadGameDataFromJson();
            ProjectManager.instance.datamanager.DataFileToGameManager();
            //ProjectManager.instance.cameracontroller.audiosource.Play();
        }

        if (ProjectManager.instance.isclearload == true)
        {
            //ProjectManager.instance.FindGameObject();
            ProjectManager.instance.isclearload = false;
            //ProjectManager.instance.cameracontroller.audiosource.Stop();
            ProjectManager.instance.datamanager.LoadGameDataFromJson();
            ProjectManager.instance.datamanager.DataFileToGameManager();
            //ProjectManager.instance.cameracontroller.audiosource.Play();
        }

        playerController = FindObjectOfType<PlayerController>();
        monster = FindObjectOfType<MonsterController>();
        //CanMove();
        //playerController.iscollisionwall = false;
        //knockbacktimer += Time.deltaTime;
        //if (knockbacktimer > knockbackduration)
        //{
        //    knockbacktimer = 0f;
        //}
        //Debug.Log("knockbacktimer" + knockbacktimer);
        //Debug.Log("knockbackduration" + knockbackduration);

        //플레이어 레벨을 프로젝트 매니저한테 주는 메서드 필요. 레벨에 따라 hp,mp,atk,def가 증가하니까
        //PlayerInfoToProjectManager();

        //플레이어가 장비를 꼈는지 안꼈는지는 항상 체크하고 있어야한다. 왜?

    }

    //충돌하면 이전 위치로
    public void CanMove()
    {
        if (playerController.iscollisionwall == true)
        {
            player.transform.position = previousposition;
        }
        else
        {
            previousposition = player.transform.position;
        }
    }

    //전투진행, 데미지계산
    public IEnumerator Combat(Collider2D col)
    {
        //ishitmonster = false;
        //몬스터한테 넉백
        KnockbackToMonster(col);

        HitSound();

        MonsterController monster = col.GetComponent<MonsterController>();
        monstermaxHP = monster.maxHP;
        
        float damage = CalculateDamageToMonster(col);
        targetmonster = true; // 타겟 포착
        if(damage >=0)
        {
            monster.HP -= (int)damage;
            monsterHP = monster.HP;
        }
        Debug.Log("몬스터 이름" + monster.name);
        Debug.Log("몬스터 체력" + monster.maxHP);
        Debug.Log("플레이어 골드" + Gold);
        if(monster.HP <= 0) // 몬스터가 죽으면
        {
            currentEXP += monster.EXP;
            Gold += monster.Gold;
            targetmonster = false;

            //경험치가 필요경험치를 초과하면 레벨업
            if(currentEXP >= maxEXP)
            {
                currentEXP -= maxEXP;
                PlayerLevelUP();
            }

            //StopCoroutine(combattomonster_co);

            //todo Sound 발생 프로젝트 매니저 monsterdie bool값 전달하고 다시 Soundmanager가 플젝매니저한테 받음
            Destroy(col.gameObject);
        }
        
        yield return new WaitForSeconds(0.3f);
        ishitmonster = false;
        yield break;

    }

    //넉백to몬스터
    public void KnockbackToMonster(Collider2D col)
    {

        Rigidbody2D colrb = col.GetComponent<Rigidbody2D>();

        Debug.Log("넉백되긴하는지");
        Vector2 colposition = new Vector2(col.transform.position.x, col.transform.position.y);
        Vector2 knockbackdirection = (col.transform.position - player.transform.position).normalized;

        //Debug.Log("knockbackdirection.x" + knockbackdirection.x);
        //Debug.Log("knockbackdirection.y" + knockbackdirection.y);
        if (knockbackdirection.x >= -1 && knockbackdirection.x < -0.5)
        {
            knockbackdirection.x = -1;
        }
        else if (knockbackdirection.x >= -0.5 && knockbackdirection.x < 0.5)
        {
            knockbackdirection.x = 0;
        }
        else if (knockbackdirection.x <= 1 && knockbackdirection.x >= 0.5)
        {
            knockbackdirection.x = 1;
        }

        if (knockbackdirection.y >= -1 && knockbackdirection.y < -0.5)
        {
            knockbackdirection.y = -1;
        }
        else if (knockbackdirection.y >= -0.5 && knockbackdirection.y < 0.5)
        {
            knockbackdirection.y = 0;
        }
        else if (knockbackdirection.y <= 1 && knockbackdirection.y >= 0.5)
        {
            knockbackdirection.y = 1;
        }

        // Debug.Log("knockbackdirection" + knockbackdirection);
        colrb.MovePosition(colposition + knockbackdirection * knockbackforce * Time.deltaTime);
        //col.gameObject.transform.position = new Vector3(colposition.x + knockbackdirection.x, colposition.y + knockbackdirection.y, 0);

        //yield return new WaitForSeconds(0.3f);

        //yield break;
    }

    //HitSound
    private void HitSound()
    {
        playerController.playeraudisource.Play();
    }

    //전투진행 몬스터가 플레이어한테
    public IEnumerator CombatToPlayer_co(Collider2D col)
    {
        //우선 충돌로 실행되는 코루틴이니 반드시 들어온다.
        //Hit이면 넉백에 데미지까지 Hit을 bool값으로 Hit의 조건
        if(IsHit(col))
        {
            Debug.Log("몬스터가 때렸다");
            MonsterController monster = col.GetComponent<MonsterController>();
            float damage = CalculateDamageToPlayer(col);
            if(damage >= 0)
            {
                currentHP -= (int)damage;
            }
            else
            {
                currentHP -= 1;
            }

            if(currentHP <= 0)
            {
                //todo.. 게임종료
                GameOver();
                Debug.Log("사망했다ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ");
            }

            KnockbackToPlayer(col);
        }

        yield return new WaitForSeconds(0.3f);
    }

    //몬스터가 플레이어를 공격에 성공했는지 아닌지
    private bool IsHit(Collider2D col)
    {
        Vector3 direction = col.transform.position - player.transform.position;
        //Debug.Log("x값 " + direction.x);
        //Debug.Log("y값 " + direction.y);
        if((direction.x >= -0.1f && direction.x <= 0.1f) || (direction.y >= -0.2f && direction.y <= 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //넉백To플레이어
    public void KnockbackToPlayer(Collider2D col)
    {
        Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();

        Debug.Log("넉백되긴하는지");
        Vector2 knockbackdirection = (player.transform.position - col.transform.position).normalized;

        Debug.Log("knockbackdirection.x" + knockbackdirection.x);
        Debug.Log("knockbackdirection.y" + knockbackdirection.y);
        Vector2 playerposition = new Vector2(player.transform.position.x, player.transform.position.y);
        player.transform.position = player.transform.position + new Vector3(knockbackdirection.x, knockbackdirection.y, 0) * 1.3f;
    }

    //데미지 계산식 플레이어한테
    private float CalculateDamageToPlayer(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (monster.ATK - DEF)*0.5f;
        return damage;
    }

    //데미지 계산식 몬스터한테
    private float CalculateDamageToMonster(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (ATK * 1.5f - monster.DEF);
        return damage;
    }

    //Bullet에 맞았을때
    public void HitBullet()
    {
        currentHP -= 5;
        if(currentHP <= 0)
        {
            GameOver();
        }
    }

    //파이어볼 사용
    public void UseFireMagic()
    {
        firemagicspawner.SpawnFireMagic();
        currentMP -= 2;
    }

    // FireMagic이 보스를 때리면
    public void HitBoss(Collider2D col)
    {
        boss.HP -= 20; 
        if(boss.HP <= 0)
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene("Clear");
        }
    }

    //게임오버
    private void GameOver()
    {
        ProjectManager.instance.cameracontroller.audiosource.Stop();
        SceneManager.LoadScene("GameOver");
    }

    //레벨업 메서드
    private void PlayerLevelUP()
    {
        level += 1;
        currentHP += 5;
        maxHP += 5;
        PlayerStatInitialize();
    }

    //플레이어스텟초기화
    private void PlayerStatInitialize()
    {
        if(level <= 1)
        {
            level = 1;
        } 
        maxHP = 20 + (5 * (level-1));
        maxMP = 20 + (5 * (level-1));
        ATK = 10 + (3 * (level - 1)) + sword.atk; 
        DEF = 10 + (3 * (level - 1)) + shield.def + armor.def;
        maxEXP = 50 + (10 * (level - 1));
        if(level == 1)
        {
            currentHP = maxHP;
            currentMP = maxMP;
        }
    }

    //무기 장착
    public void EquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK += sword.atk;
        sword.usethisitem = true;
    }

    //무기 해제
    public void UnEquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK -= sword.atk;
        sword.name = "";
        sword.atk = 0;
        sword.usethisitem = false;
    }

    //방패 장착
    public void EquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF += shield.def;
        shield.usethisitem = true;
    }

    //방패 해제
    public void UnEquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF -= shield.def;
        shield.name = "";
        shield.def = 0;
        shield.usethisitem = false;
    }

    //갑옷 장착
    public void EquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF += armor.def;
        armor.usethisitem = true;
    }

    //갑옷 해제
    public void UnEquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF -= armor.def;
        armor.name = "";
        armor.def = 0;
        armor.usethisitem = false;
    }

    //매직 장착
    public void EquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.usethisitem = true;
    }

    //매직 해제
    public void UnEquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.name = "";
        magic.usethisitem = false;
    }

    //악세사리 장착
    public void EquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.usethisitem = true;
    }

    //악세사리 해제
    public void UnEquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.name = "";
        accessory.usethisitem = false;
    }

    //아이템 장착
    public void EquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.usethisitem = true;
    }

    //아이템 해제
    public void UnEquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.name = "";
        item.usethisitem = false;
    }

    //문에 들어가고 나오기 위한 메서드
    public void ChangePlayerPosition(Collider2D col)
    {
        Debug.Log("콜의 이름" + col.name);
        //GameObject entrance = col.gameObject.transform.parent.gameObject;
        switch (col.name)
        {
            case "banoadoor_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(1).position + new Vector3(0, 2, 0);
                break;
            case "banoadoor_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(0).position + new Vector3(0, -3, 0);
                break;
            case "clinicdoor_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(3).position + new Vector3(0, 2, 0);
                break;
            case "clinicdoor_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(2).position + new Vector3(0, -3, 0);
                break;
            case "weaponshopdoor_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(5).position + new Vector3(0, 2, 0);
                break;
            case "weaponshopdoor_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(4).position + new Vector3(0, -3, 0);
                break;
            case "storedoor_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(7).position + new Vector3(0, 2, 0);
                break;
            case "storedoor_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(6).position + new Vector3(0, -3, 0);
                break;
            case "field_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(9).position + new Vector3(0, 2, 0);
                break;
            case "field_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                player.transform.position = entrance.transform.GetChild(8).position + new Vector3(0, -3, 0);
                break;
            case "ruin_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                ProjectManager.instance.cameracontroller.audiosource.Stop();
                ProjectManager.instance.isruin = true;
                ProjectManager.instance.isvillage = false;
                player.transform.position = entrance.transform.GetChild(11).position + new Vector3(0, 2, 0);
                break;
            case "ruin_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                ProjectManager.instance.cameracontroller.audiosource.Stop();
                ProjectManager.instance.isruin = false;
                ProjectManager.instance.isvillage = true;
                player.transform.position = entrance.transform.GetChild(10).position + new Vector3(0, -3, 0);
                break;
            case "bossroom_in":
                ProjectManager.instance.UImanager.StartFadeIn();
                ProjectManager.instance.cameracontroller.audiosource.Stop();
                ProjectManager.instance.isbossroom = true;
                ProjectManager.instance.isruin = false;
                player.transform.position = entrance.transform.GetChild(13).position + new Vector3(0, 2, 0);
                break;
            case "bossroom_out":
                ProjectManager.instance.UImanager.StartFadeIn();
                ProjectManager.instance.cameracontroller.audiosource.Stop();
                ProjectManager.instance.isbossroom = false;
                ProjectManager.instance.isruin = true;
                player.transform.position = entrance.transform.GetChild(12).position + new Vector3(0, -3, 0);
                break;
        }

    }
}
