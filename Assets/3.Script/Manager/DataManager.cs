using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public GameData gamedata;
    public PlayerData playerdata = new PlayerData();
    public EquipmentData equipmentdata = new EquipmentData();
    //public PlayerData playerdata;
    //public SwordData sworddata;

    private void Start()
    {
        //저장하려는 파일 초기화
        //gamedata.InitializePlayerInfo();
        //gamedata.InitializeEquipmentData();

    }

    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 명령어 생성
    public void SaveGameDataToJson()
    {
        //저장할때 정보 할당
        gamedata.InitializePlayerInfo();
        gamedata.InitializeEquipmentData();

        //ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다.
        string jsonData = JsonUtility.ToJson(gamedata, true);
        //데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "GameData.json");
        //파일 생성 및 저장
        File.WriteAllText(path, jsonData);
        Debug.Log("저장되었습니다");
    }

    [ContextMenu("Load Json Data")]
    public void LoadGameDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "GameData.json");
        if (File.Exists(path))
        {
            ProjectManager.instance.cameracontroller.audiosource.Stop();

            string jsonData = File.ReadAllText(path);
            gamedata = JsonUtility.FromJson<GameData>(jsonData);

            //페이드인아웃
            ProjectManager.instance.UImanager.StartFadeIn();
            ////저장된 데이터 파일 넘기기
            //DataFileToGameManager();

        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }

    //저장된 데이터 파일 넘기기
    public void DataFileToGameManager()
    {
        ProjectManager.instance.gamemanager.player.gameObject.transform.position = gamedata.playerdata.playerposition;
        ProjectManager.instance.gamemanager.level = gamedata.playerdata.level;
        ProjectManager.instance.gamemanager.ATK = gamedata.playerdata.ATK;
        ProjectManager.instance.gamemanager.DEF = gamedata.playerdata.DEF;
        ProjectManager.instance.gamemanager.currentHP = gamedata.playerdata.currentHP;
        ProjectManager.instance.gamemanager.maxHP = gamedata.playerdata.maxHP;
        ProjectManager.instance.gamemanager.currentMP = gamedata.playerdata.currentMP;
        ProjectManager.instance.gamemanager.maxMP = gamedata.playerdata.maxMP;
        ProjectManager.instance.gamemanager.currentEXP = gamedata.playerdata.currentEXP;
        ProjectManager.instance.gamemanager.maxEXP = gamedata.playerdata.maxEXP;
        ProjectManager.instance.gamemanager.Gold = gamedata.playerdata.Gold;

        ProjectManager.instance.isvillage = gamedata.playerdata.isvillage;
        ProjectManager.instance.isruin = gamedata.playerdata.isruin;
        ProjectManager.instance.isbossroom = gamedata.playerdata.isbossroom;

        //플레이어가 장비를 착용하고 있었는지 여부
        ProjectManager.instance.gamemanager.sword.usethisitem = gamedata.equipmentdata.usesword;
        ProjectManager.instance.gamemanager.shield.usethisitem = gamedata.equipmentdata.useshield;
        ProjectManager.instance.gamemanager.armor.usethisitem = gamedata.equipmentdata.usearmor;
        ProjectManager.instance.gamemanager.magic.usethisitem = gamedata.equipmentdata.usemagic;
        ProjectManager.instance.gamemanager.accessory.usethisitem = gamedata.equipmentdata.useaccessory;

        //플레이어가 장비를 가지고 있는지 여부
        ProjectManager.instance.itemmanager.swords[0].havethisitem = gamedata.equipmentdata.havesword0;
        ProjectManager.instance.itemmanager.swords[1].havethisitem = gamedata.equipmentdata.havesword1;
        ProjectManager.instance.itemmanager.swords[2].havethisitem = gamedata.equipmentdata.havesword2;
        ProjectManager.instance.itemmanager.shields[0].havethisitem = gamedata.equipmentdata.haveshield0;
        ProjectManager.instance.itemmanager.shields[1].havethisitem = gamedata.equipmentdata.haveshield1;
        ProjectManager.instance.itemmanager.armors[0].havethisitem = gamedata.equipmentdata.havearmor0;
        ProjectManager.instance.itemmanager.armors[1].havethisitem = gamedata.equipmentdata.havearmor1;
        ProjectManager.instance.itemmanager.armors[2].havethisitem = gamedata.equipmentdata.havearmor2;
        ProjectManager.instance.itemmanager.magics[0].havethisitem = gamedata.equipmentdata.havemagic0;
        ProjectManager.instance.itemmanager.accessories[0].havethisitem = gamedata.equipmentdata.haveaccessory2;
    }

}

[System.Serializable]
public class GameData
{
    //플레이어 정보
    public PlayerData playerdata = new PlayerData();

    [ContextMenu("플레이어 정보 업데이트")]
    public void InitializePlayerInfo()
    {
        playerdata.playerposition = ProjectManager.instance.gamemanager.player.gameObject.transform.position;
        playerdata.level = ProjectManager.instance.gamemanager.level;
        playerdata.ATK = ProjectManager.instance.gamemanager.ATK;
        playerdata.DEF = ProjectManager.instance.gamemanager.DEF;
        playerdata.currentHP = ProjectManager.instance.gamemanager.currentHP;
        playerdata.maxHP = ProjectManager.instance.gamemanager.maxHP;
        playerdata.currentMP = ProjectManager.instance.gamemanager.currentMP;
        playerdata.maxMP = ProjectManager.instance.gamemanager.maxMP;
        playerdata.currentEXP = ProjectManager.instance.gamemanager.currentEXP;
        playerdata.maxEXP = ProjectManager.instance.gamemanager.maxEXP;
        playerdata.Gold = ProjectManager.instance.gamemanager.Gold;

        playerdata.isvillage = ProjectManager.instance.isvillage;
        playerdata.isruin = ProjectManager.instance.isruin;
        playerdata.isbossroom = ProjectManager.instance.isbossroom;
}

    //플레이어 장비
    public EquipmentData equipmentdata;

    [ContextMenu("아이템 정보 업데이트")]
    public void InitializeEquipmentData()
    {
        //플레이어가 장비를 착용하고 있었는지 여부
        equipmentdata.usesword = ProjectManager.instance.gamemanager.sword.usethisitem;
        equipmentdata.useshield = ProjectManager.instance.gamemanager.shield.usethisitem;
        equipmentdata.usearmor = ProjectManager.instance.gamemanager.armor.usethisitem;
        equipmentdata.usemagic = ProjectManager.instance.gamemanager.magic.usethisitem;
        equipmentdata.useaccessory = ProjectManager.instance.gamemanager.accessory.usethisitem;

        //플레이어가 장비를 가지고 있는지 여부
        equipmentdata.havesword0 = ProjectManager.instance.itemmanager.swords[0].havethisitem;
        equipmentdata.havesword1 = ProjectManager.instance.itemmanager.swords[1].havethisitem;
        equipmentdata.havesword2 = ProjectManager.instance.itemmanager.swords[2].havethisitem;
        equipmentdata.haveshield0 = ProjectManager.instance.itemmanager.shields[0].havethisitem;
        equipmentdata.haveshield1 = ProjectManager.instance.itemmanager.shields[1].havethisitem;
        equipmentdata.havearmor0 = ProjectManager.instance.itemmanager.armors[0].havethisitem;
        equipmentdata.havearmor1 = ProjectManager.instance.itemmanager.armors[1].havethisitem;
        equipmentdata.havearmor2 = ProjectManager.instance.itemmanager.armors[2].havethisitem;
        equipmentdata.havemagic0 = ProjectManager.instance.itemmanager.magics[0].havethisitem;
        equipmentdata.haveaccessory2 = ProjectManager.instance.itemmanager.accessories[0].havethisitem;

    }

}

//플레이어 정보
[System.Serializable]
public class PlayerData
{
    public Vector3 playerposition;
    public int level;// = ProjectManager.instance.gamemanager.level;
    public int ATK;// = ProjectManager.instance.gamemanager.ATK; 
    public int DEF;// = ProjectManager.instance.gamemanager.DEF;
    public int currentHP;// = ProjectManager.instance.gamemanager.currentHP;
    public int maxHP;// = ProjectManager.instance.gamemanager.maxHP;
    public int currentMP;// = ProjectManager.instance.gamemanager.currentMP;
    public int maxMP;// = ProjectManager.instance.gamemanager.maxMP;
    public int currentEXP;// = ProjectManager.instance.gamemanager.currentEXP;
    public int maxEXP;// = ProjectManager.instance.gamemanager.maxEXP;
    public int Gold;// = ProjectManager.instance.gamemanager.Gold;

    //플레이어 있는 위치
    public bool isvillage;
    public bool isruin;
    public bool isbossroom;

    //public List<Sword> swords;

    //플레이어가 획득한 장비와 아이템 bool값으로?
    //todo 아이템획득한걸 나타내는 변수
}

//플레이어 장비
[System.Serializable]
public class EquipmentData
{
    //플레이어가 장비를 착용하고 있었는지 체크
    public bool usesword;// = ProjectManager.instance.gamemanager.sword.usethisitem;
    public bool useshield;// = ProjectManager.instance.gamemanager.shield.usethisitem;
    public bool usearmor;// = ProjectManager.instance.gamemanager.armor.usethisitem;
    public bool usemagic;// = ProjectManager.instance.gamemanager.magic.usethisitem;
    public bool useaccessory;// = ProjectManager.instance.gamemanager.accessory.usethisitem;

    //플레이어가 장비를 획득했었는지 체크
    public bool havesword0;// = ProjectManager.instance.itemmanager.swords[0].havethisitem;
    public bool havesword1;// = ProjectManager.instance.itemmanager.swords[1].havethisitem;
    public bool havesword2;// = ProjectManager.instance.itemmanager.swords[2].havethisitem;
    public bool haveshield0;// = ProjectManager.instance.itemmanager.shields[0].havethisitem;
    public bool haveshield1;// = ProjectManager.instance.itemmanager.shields[1].havethisitem;
    public bool havearmor0;// = ProjectManager.instance.itemmanager.armors[0].havethisitem;
    public bool havearmor1;// = ProjectManager.instance.itemmanager.armors[1].havethisitem;
    public bool havearmor2;// = ProjectManager.instance.itemmanager.armors[2].havethisitem;
    public bool havemagic0;// = ProjectManager.instance.itemmanager.magics[0].havethisitem;
    public bool haveaccessory2;// = ProjectManager.instance.itemmanager.accessories[0].havethisitem;

    //public Sword sword = new Sword();
    //public Shield shield = new Shield();
    //public Armor armor = new Armor();
    //public Magic magic = new Magic();
    //public Accessory accessory = new Accessory();

    //public Sword[] swords = new Sword[3];
    //public Shield[] shields = new Shield[2];
    //public Armor[] armors = new Armor[3];
    //public Magic[] magics = new Magic[1];
    //public Accessory[] accessories = new Accessory[1];

    //[ContextMenu("아이템 정보 업데이트")]
    //public void InitializeEquipmentData()
    //{
    //    //플레이어가 장비를 착용하고 있었는지 여부
    //    sword.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    shield.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    armor.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    magic.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    accessory.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;

    //    //플레이어가 장비를 가지고 있는지 여부
    //    for(int i = 0; i < 3; i++)
    //    {
    //        swords[i].havethisitem = ProjectManager.instance.itemmanager.swords[i].havethisitem;
    //        armors[i].havethisitem = ProjectManager.instance.itemmanager.armors[i].havethisitem;
    //    }
    //    for (int i = 0; i < 2; i++)
    //    {
    //        shields[i].havethisitem = ProjectManager.instance.itemmanager.shields[i].havethisitem;
    //    }
    //    magics[0].havethisitem = ProjectManager.instance.itemmanager.magics[0].havethisitem;
    //    accessories[0].havethisitem = ProjectManager.instance.itemmanager.accessories[0].havethisitem;
    //}

}
