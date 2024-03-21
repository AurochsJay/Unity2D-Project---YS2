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
        //�����Ϸ��� ���� �ʱ�ȭ
        //gamedata.InitializePlayerInfo();
        //gamedata.InitializeEquipmentData();

    }

    [ContextMenu("To Json Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� ��ɾ� ����
    public void SaveGameDataToJson()
    {
        //�����Ҷ� ���� �Ҵ�
        gamedata.InitializePlayerInfo();
        gamedata.InitializeEquipmentData();

        //ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�.
        string jsonData = JsonUtility.ToJson(gamedata, true);
        //�����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, "GameData.json");
        //���� ���� �� ����
        File.WriteAllText(path, jsonData);
        Debug.Log("����Ǿ����ϴ�");
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

            //���̵��ξƿ�
            ProjectManager.instance.UImanager.StartFadeIn();
            ////����� ������ ���� �ѱ��
            //DataFileToGameManager();

        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }

    //����� ������ ���� �ѱ��
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

        //�÷��̾ ��� �����ϰ� �־����� ����
        ProjectManager.instance.gamemanager.sword.usethisitem = gamedata.equipmentdata.usesword;
        ProjectManager.instance.gamemanager.shield.usethisitem = gamedata.equipmentdata.useshield;
        ProjectManager.instance.gamemanager.armor.usethisitem = gamedata.equipmentdata.usearmor;
        ProjectManager.instance.gamemanager.magic.usethisitem = gamedata.equipmentdata.usemagic;
        ProjectManager.instance.gamemanager.accessory.usethisitem = gamedata.equipmentdata.useaccessory;

        //�÷��̾ ��� ������ �ִ��� ����
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
    //�÷��̾� ����
    public PlayerData playerdata = new PlayerData();

    [ContextMenu("�÷��̾� ���� ������Ʈ")]
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

    //�÷��̾� ���
    public EquipmentData equipmentdata;

    [ContextMenu("������ ���� ������Ʈ")]
    public void InitializeEquipmentData()
    {
        //�÷��̾ ��� �����ϰ� �־����� ����
        equipmentdata.usesword = ProjectManager.instance.gamemanager.sword.usethisitem;
        equipmentdata.useshield = ProjectManager.instance.gamemanager.shield.usethisitem;
        equipmentdata.usearmor = ProjectManager.instance.gamemanager.armor.usethisitem;
        equipmentdata.usemagic = ProjectManager.instance.gamemanager.magic.usethisitem;
        equipmentdata.useaccessory = ProjectManager.instance.gamemanager.accessory.usethisitem;

        //�÷��̾ ��� ������ �ִ��� ����
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

//�÷��̾� ����
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

    //�÷��̾� �ִ� ��ġ
    public bool isvillage;
    public bool isruin;
    public bool isbossroom;

    //public List<Sword> swords;

    //�÷��̾ ȹ���� ���� ������ bool������?
    //todo ������ȹ���Ѱ� ��Ÿ���� ����
}

//�÷��̾� ���
[System.Serializable]
public class EquipmentData
{
    //�÷��̾ ��� �����ϰ� �־����� üũ
    public bool usesword;// = ProjectManager.instance.gamemanager.sword.usethisitem;
    public bool useshield;// = ProjectManager.instance.gamemanager.shield.usethisitem;
    public bool usearmor;// = ProjectManager.instance.gamemanager.armor.usethisitem;
    public bool usemagic;// = ProjectManager.instance.gamemanager.magic.usethisitem;
    public bool useaccessory;// = ProjectManager.instance.gamemanager.accessory.usethisitem;

    //�÷��̾ ��� ȹ���߾����� üũ
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

    //[ContextMenu("������ ���� ������Ʈ")]
    //public void InitializeEquipmentData()
    //{
    //    //�÷��̾ ��� �����ϰ� �־����� ����
    //    sword.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    shield.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    armor.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    magic.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;
    //    accessory.usethisitem = ProjectManager.instance.gamemanager.sword.usethisitem;

    //    //�÷��̾ ��� ������ �ִ��� ����
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
