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

    //���̾����
    [SerializeField] private FireMagicSpawner firemagicspawner;

    //�÷��̾� ����
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

    //�÷��̾� ��� ����
    public Sword sword;
    public Shield shield;
    public Armor armor;
    public Magic magic;
    public Accessory accessory;
    public Item item;

    //�÷��̾ �浹�� �༮�� ���� ������ ����;��Ѵ�. UI�� ǥ�����ֱ� ����, save,load �ϱ�����
    public int monstermaxHP;
    public int monsterHP;
    public bool targetmonster = false; // ���Ͱ� �������� �ʾҴٸ� UI�� ���� �ȵ�.
    public bool targetboss = false;

    //���� ��ġ
    public Vector3 previousposition;

    //�˹麯��
    public float knockbackduration = 1f; //�˹�ð�
    public float knockbacktimer; // �˹�ð� ���
    public float knockbackforce = 100f;

    //�÷��̾ �¾Ҵ���
    public bool ishitplayer = false;

    //���Ͱ� �¾Ҵ���
    public bool ishitmonster = false;

    //�ڷ�ƾ ����
    public Coroutine combattomonster_co;
    public Coroutine combattoplayer_co;

    private void Awake()
    {
        //player = GetComponent<GameObject>();
        //playerController = new PlayerController();
        //playerController = GetComponent<PlayerController>();
        monster = GetComponent<MonsterController>();

        // playerController�� null���� Ȯ��
        if (playerController == null)
        {
            Debug.LogError("PlayerController�� ã�� �� �����ϴ�. player ���� ������Ʈ�� PlayerController ������Ʈ�� ����Ǿ� �ִ��� Ȯ���ϼ���.");
        }

        previousposition = player.transform.position;

        //�÷��̾� ���� �ʱ�ȭ
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

        //�÷��̾� ������ ������Ʈ �Ŵ������� �ִ� �޼��� �ʿ�. ������ ���� hp,mp,atk,def�� �����ϴϱ�
        //PlayerInfoToProjectManager();

        //�÷��̾ ��� ������ �Ȳ������� �׻� üũ�ϰ� �־���Ѵ�. ��?

    }

    //�浹�ϸ� ���� ��ġ��
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

    //��������, ���������
    public IEnumerator Combat(Collider2D col)
    {
        //ishitmonster = false;
        //�������� �˹�
        KnockbackToMonster(col);

        HitSound();

        MonsterController monster = col.GetComponent<MonsterController>();
        monstermaxHP = monster.maxHP;
        
        float damage = CalculateDamageToMonster(col);
        targetmonster = true; // Ÿ�� ����
        if(damage >=0)
        {
            monster.HP -= (int)damage;
            monsterHP = monster.HP;
        }
        Debug.Log("���� �̸�" + monster.name);
        Debug.Log("���� ü��" + monster.maxHP);
        Debug.Log("�÷��̾� ���" + Gold);
        if(monster.HP <= 0) // ���Ͱ� ������
        {
            currentEXP += monster.EXP;
            Gold += monster.Gold;
            targetmonster = false;

            //����ġ�� �ʿ����ġ�� �ʰ��ϸ� ������
            if(currentEXP >= maxEXP)
            {
                currentEXP -= maxEXP;
                PlayerLevelUP();
            }

            //StopCoroutine(combattomonster_co);

            //todo Sound �߻� ������Ʈ �Ŵ��� monsterdie bool�� �����ϰ� �ٽ� Soundmanager�� �����Ŵ������� ����
            Destroy(col.gameObject);
        }
        
        yield return new WaitForSeconds(0.3f);
        ishitmonster = false;
        yield break;

    }

    //�˹�to����
    public void KnockbackToMonster(Collider2D col)
    {

        Rigidbody2D colrb = col.GetComponent<Rigidbody2D>();

        Debug.Log("�˹�Ǳ��ϴ���");
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

    //�������� ���Ͱ� �÷��̾�����
    public IEnumerator CombatToPlayer_co(Collider2D col)
    {
        //�켱 �浹�� ����Ǵ� �ڷ�ƾ�̴� �ݵ�� ���´�.
        //Hit�̸� �˹鿡 ���������� Hit�� bool������ Hit�� ����
        if(IsHit(col))
        {
            Debug.Log("���Ͱ� ���ȴ�");
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
                //todo.. ��������
                GameOver();
                Debug.Log("����ߴ٤���������������������������");
            }

            KnockbackToPlayer(col);
        }

        yield return new WaitForSeconds(0.3f);
    }

    //���Ͱ� �÷��̾ ���ݿ� �����ߴ��� �ƴ���
    private bool IsHit(Collider2D col)
    {
        Vector3 direction = col.transform.position - player.transform.position;
        //Debug.Log("x�� " + direction.x);
        //Debug.Log("y�� " + direction.y);
        if((direction.x >= -0.1f && direction.x <= 0.1f) || (direction.y >= -0.2f && direction.y <= 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�˹�To�÷��̾�
    public void KnockbackToPlayer(Collider2D col)
    {
        Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();

        Debug.Log("�˹�Ǳ��ϴ���");
        Vector2 knockbackdirection = (player.transform.position - col.transform.position).normalized;

        Debug.Log("knockbackdirection.x" + knockbackdirection.x);
        Debug.Log("knockbackdirection.y" + knockbackdirection.y);
        Vector2 playerposition = new Vector2(player.transform.position.x, player.transform.position.y);
        player.transform.position = player.transform.position + new Vector3(knockbackdirection.x, knockbackdirection.y, 0) * 1.3f;
    }

    //������ ���� �÷��̾�����
    private float CalculateDamageToPlayer(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (monster.ATK - DEF)*0.5f;
        return damage;
    }

    //������ ���� ��������
    private float CalculateDamageToMonster(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (ATK * 1.5f - monster.DEF);
        return damage;
    }

    //Bullet�� �¾�����
    public void HitBullet()
    {
        currentHP -= 5;
        if(currentHP <= 0)
        {
            GameOver();
        }
    }

    //���̾ ���
    public void UseFireMagic()
    {
        firemagicspawner.SpawnFireMagic();
        currentMP -= 2;
    }

    // FireMagic�� ������ ������
    public void HitBoss(Collider2D col)
    {
        boss.HP -= 20; 
        if(boss.HP <= 0)
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene("Clear");
        }
    }

    //���ӿ���
    private void GameOver()
    {
        ProjectManager.instance.cameracontroller.audiosource.Stop();
        SceneManager.LoadScene("GameOver");
    }

    //������ �޼���
    private void PlayerLevelUP()
    {
        level += 1;
        currentHP += 5;
        maxHP += 5;
        PlayerStatInitialize();
    }

    //�÷��̾���ʱ�ȭ
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

    //���� ����
    public void EquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK += sword.atk;
        sword.usethisitem = true;
    }

    //���� ����
    public void UnEquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK -= sword.atk;
        sword.name = "";
        sword.atk = 0;
        sword.usethisitem = false;
    }

    //���� ����
    public void EquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF += shield.def;
        shield.usethisitem = true;
    }

    //���� ����
    public void UnEquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF -= shield.def;
        shield.name = "";
        shield.def = 0;
        shield.usethisitem = false;
    }

    //���� ����
    public void EquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF += armor.def;
        armor.usethisitem = true;
    }

    //���� ����
    public void UnEquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF -= armor.def;
        armor.name = "";
        armor.def = 0;
        armor.usethisitem = false;
    }

    //���� ����
    public void EquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.usethisitem = true;
    }

    //���� ����
    public void UnEquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.name = "";
        magic.usethisitem = false;
    }

    //�Ǽ��縮 ����
    public void EquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.usethisitem = true;
    }

    //�Ǽ��縮 ����
    public void UnEquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.name = "";
        accessory.usethisitem = false;
    }

    //������ ����
    public void EquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.usethisitem = true;
    }

    //������ ����
    public void UnEquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.name = "";
        item.usethisitem = false;
    }

    //���� ���� ������ ���� �޼���
    public void ChangePlayerPosition(Collider2D col)
    {
        Debug.Log("���� �̸�" + col.name);
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
