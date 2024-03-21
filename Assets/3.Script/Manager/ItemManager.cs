using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Sword
{
    public string name;
    public int atk;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

public struct Shield
{
    public string name;
    public int def;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

public struct Armor
{
    public string name;
    public int def;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

public struct Magic
{
    public string name;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

public struct Accessory
{
    public string name;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

//������
public struct Item
{
    public string name;
    public string info;
    public Sprite image;
    public bool havethisitem;
    public bool usethisitem;
}

public class ItemManager : MonoBehaviour
{
    public Sword[] swords;
    public Shield[] shields;
    public Armor[] armors;
    public Magic[] magics;
    public Accessory[] accessories;
    public Item[] items;


    private void Start()
    {
        InitiailzeEquipment();
        InitializeItem();
    }

    private void InitiailzeEquipment()
    {
        swords = new Sword[3];
        shields = new Shield[2];
        armors = new Armor[3];
        magics = new Magic[1];
        accessories = new Accessory[1];

        swords[0].name = "Short Sword";
        swords[0].atk = 5;
        swords[0].info = "�۰� ���� ����. Ư���� �����غ����� �ʴ´�.";
        swords[0].image = Resources.Load<Sprite>("Equipment/ys2 Short Sword image");
        swords[0].havethisitem = false;
        swords[0].usethisitem = false;
        swords[1].name = "Long Sword";
        swords[1].atk = 10;
        swords[1].info = "�ռҵ�.";
        swords[1].image = Resources.Load<Sprite>("Equipment/ys2 Long Sword image");
        swords[1].havethisitem = false;
        swords[1].usethisitem = false;
        swords[2].name = "Talwar";
        swords[2].atk = 20;
        swords[2].info = "Ÿ��.";
        swords[2].image = Resources.Load<Sprite>("Equipment/ys2 Talwar image");
        swords[2].havethisitem = false;
        swords[2].usethisitem = false;


        shields[0].name = "Wooden Shield";
        shields[0].def = 3;
        shields[0].info = "���� ���� ����";
        shields[0].image = Resources.Load<Sprite>("Equipment/ys2 Wooden Shield image");
        shields[0].havethisitem = false;
        shields[0].usethisitem = false;
        shields[1].name = "Small Shield";
        shields[1].def = 7;
        shields[1].info = "���� ���� ����";
        shields[1].image = Resources.Load<Sprite>("Equipment/ys2 Small Shield image");
        shields[1].havethisitem = false;
        shields[1].usethisitem = false;

        armors[0].name = "Chain Mail";
        armors[0].def = 4;
        armors[0].info = "����";
        armors[0].image = Resources.Load<Sprite>("Equipment/ys2 Chain Mail image");
        armors[0].havethisitem = false;
        armors[0].usethisitem = false;
        armors[1].name = "Breast Plate";
        armors[1].def = 9;
        armors[1].info = "���� ����";
        armors[1].image = Resources.Load<Sprite>("Equipment/ys2 Breast Plate image");
        armors[1].havethisitem = false;
        armors[1].usethisitem = false;
        armors[2].name = "Plate Mail";
        armors[2].def = 15;
        armors[2].info = "���� ���� ����";
        armors[2].image = Resources.Load<Sprite>("Equipment/ys2 Plate Mail image");
        armors[2].havethisitem = false;
        armors[2].usethisitem = false;

        magics[0].name = "Fire Magic";
        magics[0].info = "���̾� ����";
        magics[0].image = Resources.Load<Sprite>("Equipment/ys2 Fire Magic image");
        magics[0].havethisitem = false;
        magics[0].usethisitem = false;

        accessories[0].name = "Cleria Ring";
        accessories[0].info = "����";
        accessories[0].image = Resources.Load<Sprite>("Equipment/ys2 Cleria Ring image");
        accessories[0].havethisitem = false;
        accessories[0].usethisitem = false;

    }

    private void InitializeItem()
    {
        items = new Item[12];

        items[0].name = "�ϴ��� ��";
        items[0].info = "�ϴ��� �� å";
        items[0].image = Resources.Load<Sprite>("Item/ys2 �ϴ��� �� image");
        items[0].havethisitem = true;
        items[0].usethisitem = false;
        items[1].name = "����� ��";
        items[1].info = "��ٰ� �� å";
        items[1].image = Resources.Load<Sprite>("Item/ys2 ����� �� image");
        items[1].havethisitem = true;
        items[1].usethisitem = false;
        items[2].name = "�ٺ��� ��";
        items[2].info = "�ٺ� �� å";
        items[2].image = Resources.Load<Sprite>("Item/ys2 �ٺ��� �� image");
        items[2].havethisitem = true;
        items[2].usethisitem = false;
        items[3].name = "�޻��� ��";
        items[3].info = "�޻簡 �� å";
        items[3].image = Resources.Load<Sprite>("Item/ys2 �޻��� �� image");
        items[3].havethisitem = true;
        items[3].usethisitem = false;
        items[4].name = "�׸��� ��";
        items[4].info = "�׸��� �� å";
        items[4].image = Resources.Load<Sprite>("Item/ys2 �׸��� �� image");
        items[4].havethisitem = true;
        items[4].usethisitem = false;
        items[5].name = "��Ʈ�� ��";
        items[5].info = "��Ʈ�� �� å";
        items[5].image = Resources.Load<Sprite>("Item/ys2 ��Ʈ�� �� image");
        items[5].havethisitem = true;
        items[5].usethisitem = false;
        items[6].name = "����� ����";
        items[6].info = "��뿡 ���� ����ó�� ���δ�. ������ �� �� ����.";
        items[6].image = Resources.Load<Sprite>("Item/ys2 ����� ���� image");
        items[6].havethisitem = false;
        items[6].usethisitem = false;
        items[7].name = "�ٿ����� ����";
        items[7].info = "�ٿ����� �� ����. � ������ �����ִ�.";
        items[7].image = Resources.Load<Sprite>("Item/ys2 �ٿ����� ���� image");
        items[7].havethisitem = false;
        items[7].usethisitem = false;
        items[8].name = "���";
        items[8].info = "ü���� ȸ���ϴµ� ���̴� ����";
        items[8].image = Resources.Load<Sprite>("Item/ys2 ��� image");
        items[8].havethisitem = false;
        items[8].usethisitem = false;
        items[9].name = "���";
        items[9].info = "����ϰ� ���� ���. ���־�δ�.";
        items[9].image = Resources.Load<Sprite>("Item/ys2 ��� image");
        items[9].havethisitem = false;
        items[9].usethisitem = false;
        items[10].name = "�δ� ����";
        items[10].info = "����ϰ� �߰ߵǴ� ����. ��ô �ǰ����� �� ����.";
        items[10].image = Resources.Load<Sprite>("Item/ys2 �δ� ���� image");
        items[10].havethisitem = false;
        items[10].usethisitem = false;
        items[11].name = "���� ��";
        items[11].info = "���ǿ��� �߰ߵǴ� ��. ��򰡿� ���Ǵ��� ��ó�� �Ҹ�Ȯ�ϴ�.";
        items[11].image = Resources.Load<Sprite>("Item/ys2 ���� �� image");
        items[11].havethisitem = false;
        items[11].usethisitem = false;
        
    }

    //UIâ���� �������, slotnumber 0~5:���� 6~11:����, 12~17:����, 18~23:����, 24~29:�Ǽ�
    public void UseEquipment(int slotnumber)
    {
        //���⸦ ������ �ٸ� ���⸦ �����ϰ� ������ �װ��� �����ؾ��Ѵ�.
        //������ ���� �� �����ϰ� ���������� �����Ѱ� �����ϱ�.?
        //slotnumber == ������ ������ ��ȣ
        if(slotnumber >= 0 && slotnumber<=2)
        {
            //���⸦ ������ �־�� ������ �� �ִ�.
            if(swords[slotnumber].havethisitem)
            {
                //������ ���⸦ �̹� �����ϰ� �ִٸ�
                if (swords[slotnumber].usethisitem == true)
                {
                    //�������� ����
                    swords[slotnumber].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipSword(slotnumber);
                    //�������� �����ϰ� Equipǥ�� ����
                    ProjectManager.instance.UImanager.equipsworditem.gameObject.SetActive(false);
                }
                else
                {
                    //�ٸ� ���Ⱑ �����Ǿ� �ִٸ� �����ؾ���.
                    for (int i = 0; i <= 2; i++)
                    {
                        if (swords[i].usethisitem == true && i != slotnumber)
                        {
                            swords[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipSword(i);
                        }
                    }

                    //������ ���� ����
                    swords[slotnumber].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipSword(slotnumber);
                    //������ ���� �����ϰ� Equipǥ��
                    ProjectManager.instance.UImanager.equipsworditem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equipsworditem.gameObject.SetActive(true);
                }
            }
        }
        //����
        else if(slotnumber >=6 && slotnumber <= 7)
        {
            //���и� ������ �־�� ����
            if(shields[slotnumber-6].havethisitem)
            {
                if (shields[slotnumber - 6].usethisitem == true)
                {
                    shields[slotnumber - 6].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipShield(slotnumber - 6);
                    ProjectManager.instance.UImanager.equipshielditem.gameObject.SetActive(false);
                }
                else
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        if (shields[i].usethisitem == true && i != slotnumber - 6)
                        {
                            shields[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipShield(i);
                        }
                    }

                    shields[slotnumber - 6].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipShield(slotnumber - 6);
                    ProjectManager.instance.UImanager.equipshielditem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equipshielditem.gameObject.SetActive(true);
                }
            }
        }
        //����
        else if (slotnumber >= 12 && slotnumber <= 14)
        {
            if(armors[slotnumber-12].havethisitem)
            {
                if (armors[slotnumber - 12].usethisitem == true)
                {
                    armors[slotnumber - 12].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipArmor(slotnumber - 12);
                    ProjectManager.instance.UImanager.equiparmoritem.gameObject.SetActive(false);
                }
                else
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        if (armors[i].usethisitem == true && i != slotnumber - 12)
                        {
                            armors[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipArmor(i);
                        }
                    }

                    armors[slotnumber - 12].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipArmor(slotnumber - 12);
                    ProjectManager.instance.UImanager.equiparmoritem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equiparmoritem.gameObject.SetActive(true);
                }
            }
        }
        //����
        else if (slotnumber >= 18 && slotnumber <= 18)
        {
            if(magics[slotnumber-18].havethisitem)
            {
                if (magics[slotnumber - 18].usethisitem == true)
                {
                    magics[slotnumber - 18].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipMagic(slotnumber - 18);
                    ProjectManager.instance.UImanager.equipmagicitem.gameObject.SetActive(false);
                }
                else
                {
                    for (int i = 0; i <= 0; i++)
                    {
                        if (magics[i].usethisitem == true && i != slotnumber - 18)
                        {
                            magics[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipMagic(i);
                        }
                    }

                    magics[slotnumber - 18].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipMagic(slotnumber - 18);
                    ProjectManager.instance.UImanager.equipmagicitem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equipmagicitem.gameObject.SetActive(true);
                }
            }
        }
        //�Ǽ��縮
        else if (slotnumber >= 24 && slotnumber <= 24)
        {
            if(accessories[slotnumber-24].havethisitem)
            {
                if (accessories[slotnumber - 24].usethisitem == true)
                {
                    accessories[slotnumber - 24].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipAccessory(slotnumber - 24);
                    ProjectManager.instance.UImanager.equipaccessoryitem.gameObject.SetActive(false);
                }
                else
                {
                    for (int i = 0; i <= 0; i++)
                    {
                        if (accessories[i].usethisitem == true && i != slotnumber - 24)
                        {
                            accessories[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipAccessory(i);
                        }
                    }

                    accessories[slotnumber - 24].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipAccessory(slotnumber - 24);
                    ProjectManager.instance.UImanager.equipaccessoryitem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equipaccessoryitem.gameObject.SetActive(true);
                }
            }
        }
    }

    //UIâ����, slotnumber 0~5:å 6~11:(6:����), 12~17:1��, 18~23:1��, 24~29:1��, 30~35:1��, 36~41:1��
    //����ġ-case�� ����
    public void EquipItem(int slotnumber)
    {
        //slotnumber == ������ �������� ��ȣ
        switch(slotnumber)
        {
            case 0:
                CheckItem(slotnumber, slotnumber);
                break;
            case 1:
                CheckItem(slotnumber, slotnumber);
                break;
            case 2:
                CheckItem(slotnumber, slotnumber);
                break;
            case 3:
                CheckItem(slotnumber, slotnumber);
                break;
            case 4:
                CheckItem(slotnumber, slotnumber);
                break;
            case 5:
                CheckItem(slotnumber, slotnumber);
                break;
            case 6:
                //todo �������� ������ �־�� ����,������ ����
                if(items[slotnumber].havethisitem)
                {
                    CheckItem(slotnumber, slotnumber);
                }
                break;
            case 25:
                if (items[7].havethisitem)
                {
                    CheckItem(7, slotnumber);
                }
                break;
            case 31:
                if (items[8].havethisitem)
                {
                    CheckItem(8, slotnumber);
                }
                break;
            case 32:
                if (items[9].havethisitem)
                {
                    CheckItem(9, slotnumber);
                }
                break;
            case 33:
                if (items[10].havethisitem)
                {
                    CheckItem(10, slotnumber);
                }
                break;
            case 34:
                if (items[11].havethisitem)
                {
                    CheckItem(11, slotnumber);
                }
                break;
            default:
                break;
                
        }
    }

    //������ ����ߴ��� üũ
    private void CheckItem(int index, int slotnumber)
    {
        //������ �������� �̹� �����ϰ� �ִٸ�
        if (items[index].usethisitem == true)
        {
            //���������� ����
            items[index].usethisitem = false;
            ProjectManager.instance.gamemanager.UnEquipItem(index);
            //���������� �����ϰ� Equipǥ�� ����
            ProjectManager.instance.UImanager.equipitem.gameObject.SetActive(false);
        }
        else
        {
            //�ٸ� �������� �����Ǿ� �ִٸ� �����ؾ���.
            for (int i = 0; i <= 11; i++)
            {
                if (items[i].usethisitem == true && i != index)
                {
                    items[i].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipItem(i);
                }
            }

            //������ ������ ����
            items[index].usethisitem = true;
            ProjectManager.instance.gamemanager.EquipItem(index);
            //������ ������ �����ϰ� Equipǥ��
            ProjectManager.instance.UImanager.equipitem.transform.position = ProjectManager.instance.UImanager.inventorylayout.transform.GetChild(slotnumber).position;
            ProjectManager.instance.UImanager.equipitem.gameObject.SetActive(true);
        }
    }

}
