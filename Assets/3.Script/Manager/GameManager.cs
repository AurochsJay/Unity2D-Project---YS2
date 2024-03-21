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

    //督戚嬢古送
    [SerializeField] private FireMagicSpawner firemagicspawner;

    //巴傾戚嬢 舛左
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

    //巴傾戚嬢 舌搾 舛左
    public Sword sword;
    public Shield shield;
    public Armor armor;
    public Magic magic;
    public Accessory accessory;
    public Item item;

    //巴傾戚嬢亜 中宜廃 橿汐税 佼什斗 舛左研 握壱人醤廃陥. UI拭 妊獣背爽奄 是背, save,load 馬奄是背
    public int monstermaxHP;
    public int monsterHP;
    public bool targetmonster = false; // 佼什斗亜 走舛鞠走 省紹陥檎 UI拭 句酔檎 照喫.
    public bool targetboss = false;

    //戚穿 是帖
    public Vector3 previousposition;

    //核拷痕呪
    public float knockbackduration = 1f; //核拷獣娃
    public float knockbacktimer; // 核拷獣娃 域至
    public float knockbackforce = 100f;

    //巴傾戚嬢亜 限紹澗走
    public bool ishitplayer = false;

    //佼什斗亜 限紹澗走
    public bool ishitmonster = false;

    //坪欠鴇 痕呪
    public Coroutine combattomonster_co;
    public Coroutine combattoplayer_co;

    private void Awake()
    {
        //player = GetComponent<GameObject>();
        //playerController = new PlayerController();
        //playerController = GetComponent<PlayerController>();
        monster = GetComponent<MonsterController>();

        // playerController亜 null昔走 溌昔
        if (playerController == null)
        {
            Debug.LogError("PlayerController亜 達聖 呪 蒸柔艦陥. player 惟績 神崎詮闘拭 PlayerController 陳匂獲闘亜 尻衣鞠嬢 赤澗走 溌昔馬室推.");
        }

        previousposition = player.transform.position;

        //巴傾戚嬢 什倒 段奄鉢
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

        //巴傾戚嬢 傾婚聖 覗稽詮闘 古艦煽廃砺 爽澗 五辞球 琶推. 傾婚拭 魚虞 hp,mp,atk,def亜 装亜馬艦猿
        //PlayerInfoToProjectManager();

        //巴傾戚嬢亜 舌搾研 下澗走 照下澗走澗 牌雌 端滴馬壱 赤嬢醤廃陥. 訊?

    }

    //中宜馬檎 戚穿 是帖稽
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

    //穿燈遭楳, 汽耕走域至
    public IEnumerator Combat(Collider2D col)
    {
        //ishitmonster = false;
        //佼什斗廃砺 核拷
        KnockbackToMonster(col);

        HitSound();

        MonsterController monster = col.GetComponent<MonsterController>();
        monstermaxHP = monster.maxHP;
        
        float damage = CalculateDamageToMonster(col);
        targetmonster = true; // 展為 匂鐸
        if(damage >=0)
        {
            monster.HP -= (int)damage;
            monsterHP = monster.HP;
        }
        Debug.Log("佼什斗 戚硯" + monster.name);
        Debug.Log("佼什斗 端径" + monster.maxHP);
        Debug.Log("巴傾戚嬢 茨球" + Gold);
        if(monster.HP <= 0) // 佼什斗亜 宋生檎
        {
            currentEXP += monster.EXP;
            Gold += monster.Gold;
            targetmonster = false;

            //井蝿帖亜 琶推井蝿帖研 段引馬檎 傾婚穣
            if(currentEXP >= maxEXP)
            {
                currentEXP -= maxEXP;
                PlayerLevelUP();
            }

            //StopCoroutine(combattomonster_co);

            //todo Sound 降持 覗稽詮闘 古艦煽 monsterdie bool葵 穿含馬壱 陥獣 Soundmanager亜 巴詮古艦煽廃砺 閤製
            Destroy(col.gameObject);
        }
        
        yield return new WaitForSeconds(0.3f);
        ishitmonster = false;
        yield break;

    }

    //核拷to佼什斗
    public void KnockbackToMonster(Collider2D col)
    {

        Rigidbody2D colrb = col.GetComponent<Rigidbody2D>();

        Debug.Log("核拷鞠延馬澗走");
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

    //穿燈遭楳 佼什斗亜 巴傾戚嬢廃砺
    public IEnumerator CombatToPlayer_co(Collider2D col)
    {
        //酔識 中宜稽 叔楳鞠澗 坪欠鴇戚艦 鋼球獣 級嬢紳陥.
        //Hit戚檎 核拷拭 汽耕走猿走 Hit聖 bool葵生稽 Hit税 繕闇
        if(IsHit(col))
        {
            Debug.Log("佼什斗亜 凶携陥");
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
                //todo.. 惟績曽戟
                GameOver();
                Debug.Log("紫諺梅陥たたたたたたたたたたたたたた");
            }

            KnockbackToPlayer(col);
        }

        yield return new WaitForSeconds(0.3f);
    }

    //佼什斗亜 巴傾戚嬢研 因維拭 失因梅澗走 焼観走
    private bool IsHit(Collider2D col)
    {
        Vector3 direction = col.transform.position - player.transform.position;
        //Debug.Log("x葵 " + direction.x);
        //Debug.Log("y葵 " + direction.y);
        if((direction.x >= -0.1f && direction.x <= 0.1f) || (direction.y >= -0.2f && direction.y <= 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //核拷To巴傾戚嬢
    public void KnockbackToPlayer(Collider2D col)
    {
        Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();

        Debug.Log("核拷鞠延馬澗走");
        Vector2 knockbackdirection = (player.transform.position - col.transform.position).normalized;

        Debug.Log("knockbackdirection.x" + knockbackdirection.x);
        Debug.Log("knockbackdirection.y" + knockbackdirection.y);
        Vector2 playerposition = new Vector2(player.transform.position.x, player.transform.position.y);
        player.transform.position = player.transform.position + new Vector3(knockbackdirection.x, knockbackdirection.y, 0) * 1.3f;
    }

    //汽耕走 域至縦 巴傾戚嬢廃砺
    private float CalculateDamageToPlayer(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (monster.ATK - DEF)*0.5f;
        return damage;
    }

    //汽耕走 域至縦 佼什斗廃砺
    private float CalculateDamageToMonster(Collider2D col)
    {
        MonsterController monster = col.GetComponent<MonsterController>();
        float damage = (ATK * 1.5f - monster.DEF);
        return damage;
    }

    //Bullet拭 限紹聖凶
    public void HitBullet()
    {
        currentHP -= 5;
        if(currentHP <= 0)
        {
            GameOver();
        }
    }

    //督戚嬢瑳 紫遂
    public void UseFireMagic()
    {
        firemagicspawner.SpawnFireMagic();
        currentMP -= 2;
    }

    // FireMagic戚 左什研 凶軒檎
    public void HitBoss(Collider2D col)
    {
        boss.HP -= 20; 
        if(boss.HP <= 0)
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene("Clear");
        }
    }

    //惟績神獄
    private void GameOver()
    {
        ProjectManager.instance.cameracontroller.audiosource.Stop();
        SceneManager.LoadScene("GameOver");
    }

    //傾婚穣 五辞球
    private void PlayerLevelUP()
    {
        level += 1;
        currentHP += 5;
        maxHP += 5;
        PlayerStatInitialize();
    }

    //巴傾戚嬢什倒段奄鉢
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

    //巷奄 舌鐸
    public void EquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK += sword.atk;
        sword.usethisitem = true;
    }

    //巷奄 背薦
    public void UnEquipSword(int index)
    {
        sword = ProjectManager.instance.itemmanager.swords[index];
        ATK -= sword.atk;
        sword.name = "";
        sword.atk = 0;
        sword.usethisitem = false;
    }

    //号鳶 舌鐸
    public void EquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF += shield.def;
        shield.usethisitem = true;
    }

    //号鳶 背薦
    public void UnEquipShield(int index)
    {
        shield = ProjectManager.instance.itemmanager.shields[index];
        DEF -= shield.def;
        shield.name = "";
        shield.def = 0;
        shield.usethisitem = false;
    }

    //逢進 舌鐸
    public void EquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF += armor.def;
        armor.usethisitem = true;
    }

    //逢進 背薦
    public void UnEquipArmor(int index)
    {
        armor = ProjectManager.instance.itemmanager.armors[index];
        DEF -= armor.def;
        armor.name = "";
        armor.def = 0;
        armor.usethisitem = false;
    }

    //古送 舌鐸
    public void EquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.usethisitem = true;
    }

    //古送 背薦
    public void UnEquipMagic(int index)
    {
        magic = ProjectManager.instance.itemmanager.magics[index];
        magic.name = "";
        magic.usethisitem = false;
    }

    //焦室紫軒 舌鐸
    public void EquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.usethisitem = true;
    }

    //焦室紫軒 背薦
    public void UnEquipAccessory(int index)
    {
        accessory = ProjectManager.instance.itemmanager.accessories[index];
        accessory.name = "";
        accessory.usethisitem = false;
    }

    //焼戚奴 舌鐸
    public void EquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.usethisitem = true;
    }

    //焼戚奴 背薦
    public void UnEquipItem(int index)
    {
        item = ProjectManager.instance.itemmanager.items[index];
        item.name = "";
        item.usethisitem = false;
    }

    //庚拭 級嬢亜壱 蟹神奄 是廃 五辞球
    public void ChangePlayerPosition(Collider2D col)
    {
        Debug.Log("紬税 戚硯" + col.name);
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
