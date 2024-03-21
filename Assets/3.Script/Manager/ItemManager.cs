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

//아이템
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
        swords[0].info = "작고 값싼 무기. 특별히 강력해보이지 않는다.";
        swords[0].image = Resources.Load<Sprite>("Equipment/ys2 Short Sword image");
        swords[0].havethisitem = false;
        swords[0].usethisitem = false;
        swords[1].name = "Long Sword";
        swords[1].atk = 10;
        swords[1].info = "롱소드.";
        swords[1].image = Resources.Load<Sprite>("Equipment/ys2 Long Sword image");
        swords[1].havethisitem = false;
        swords[1].usethisitem = false;
        swords[2].name = "Talwar";
        swords[2].atk = 20;
        swords[2].info = "타왈.";
        swords[2].image = Resources.Load<Sprite>("Equipment/ys2 Talwar image");
        swords[2].havethisitem = false;
        swords[2].usethisitem = false;


        shields[0].name = "Wooden Shield";
        shields[0].def = 3;
        shields[0].info = "작은 나무 방패";
        shields[0].image = Resources.Load<Sprite>("Equipment/ys2 Wooden Shield image");
        shields[0].havethisitem = false;
        shields[0].usethisitem = false;
        shields[1].name = "Small Shield";
        shields[1].def = 7;
        shields[1].info = "작은 나무 방패";
        shields[1].image = Resources.Load<Sprite>("Equipment/ys2 Small Shield image");
        shields[1].havethisitem = false;
        shields[1].usethisitem = false;

        armors[0].name = "Chain Mail";
        armors[0].def = 4;
        armors[0].info = "갑빠";
        armors[0].image = Resources.Load<Sprite>("Equipment/ys2 Chain Mail image");
        armors[0].havethisitem = false;
        armors[0].usethisitem = false;
        armors[1].name = "Breast Plate";
        armors[1].def = 9;
        armors[1].info = "좋은 갑빠";
        armors[1].image = Resources.Load<Sprite>("Equipment/ys2 Breast Plate image");
        armors[1].havethisitem = false;
        armors[1].usethisitem = false;
        armors[2].name = "Plate Mail";
        armors[2].def = 15;
        armors[2].info = "아주 좋은 갑빠";
        armors[2].image = Resources.Load<Sprite>("Equipment/ys2 Plate Mail image");
        armors[2].havethisitem = false;
        armors[2].usethisitem = false;

        magics[0].name = "Fire Magic";
        magics[0].info = "파이아 매직";
        magics[0].image = Resources.Load<Sprite>("Equipment/ys2 Fire Magic image");
        magics[0].havethisitem = false;
        magics[0].usethisitem = false;

        accessories[0].name = "Cleria Ring";
        accessories[0].info = "반지";
        accessories[0].image = Resources.Load<Sprite>("Equipment/ys2 Cleria Ring image");
        accessories[0].havethisitem = false;
        accessories[0].usethisitem = false;

    }

    private void InitializeItem()
    {
        items = new Item[12];

        items[0].name = "하달의 서";
        items[0].info = "하달이 쓴 책";
        items[0].image = Resources.Load<Sprite>("Item/ys2 하달의 서 image");
        items[0].havethisitem = true;
        items[0].usethisitem = false;
        items[1].name = "토바의 서";
        items[1].info = "토바가 쓴 책";
        items[1].image = Resources.Load<Sprite>("Item/ys2 토바의 서 image");
        items[1].havethisitem = true;
        items[1].usethisitem = false;
        items[2].name = "다비의 서";
        items[2].info = "다비가 쓴 책";
        items[2].image = Resources.Load<Sprite>("Item/ys2 다비의 서 image");
        items[2].havethisitem = true;
        items[2].usethisitem = false;
        items[3].name = "메사의 서";
        items[3].info = "메사가 쓴 책";
        items[3].image = Resources.Load<Sprite>("Item/ys2 메사의 서 image");
        items[3].havethisitem = true;
        items[3].usethisitem = false;
        items[4].name = "겜마의 서";
        items[4].info = "겜마가 쓴 책";
        items[4].image = Resources.Load<Sprite>("Item/ys2 겜마의 서 image");
        items[4].havethisitem = true;
        items[4].usethisitem = false;
        items[5].name = "팩트의 서";
        items[5].info = "팩트가 쓴 책";
        items[5].image = Resources.Load<Sprite>("Item/ys2 팩트의 서 image");
        items[5].havethisitem = true;
        items[5].usethisitem = false;
        items[6].name = "고대의 석판";
        items[6].info = "고대에 쓰인 석판처럼 보인다. 내용을 알 수 없다.";
        items[6].image = Resources.Load<Sprite>("Item/ys2 고대의 석판 image");
        items[6].havethisitem = false;
        items[6].usethisitem = false;
        items[7].name = "바오나의 편지";
        items[7].info = "바오나가 쓴 편지. 어떤 내용이 적혀있다.";
        items[7].image = Resources.Load<Sprite>("Item/ys2 바오나의 편지 image");
        items[7].havethisitem = false;
        items[7].usethisitem = false;
        items[8].name = "허브";
        items[8].info = "체력을 회복하는데 쓰이는 약초";
        items[8].image = Resources.Load<Sprite>("Item/ys2 허브 image");
        items[8].havethisitem = false;
        items[8].usethisitem = false;
        items[9].name = "사과";
        items[9].info = "평범하게 생긴 사과. 맛있어보인다.";
        items[9].image = Resources.Load<Sprite>("Item/ys2 사과 image");
        items[9].havethisitem = false;
        items[9].usethisitem = false;
        items[10].name = "로다 열매";
        items[10].info = "희귀하게 발견되는 열매. 무척 건강해질 것 같다.";
        items[10].image = Resources.Load<Sprite>("Item/ys2 로다 열매 image");
        items[10].havethisitem = false;
        items[10].usethisitem = false;
        items[11].name = "마리 꽃";
        items[11].info = "들판에서 발견되는 꽃. 어딘가에 사용되는지 출처가 불명확하다.";
        items[11].image = Resources.Load<Sprite>("Item/ys2 마리 꽃 image");
        items[11].havethisitem = false;
        items[11].usethisitem = false;
        
    }

    //UI창에서 장비장착, slotnumber 0~5:무기 6~11:방패, 12~17:갑옷, 18~23:마법, 24~29:악세
    public void UseEquipment(int slotnumber)
    {
        //무기를 꼈을때 다른 무기를 장착하고 있으면 그것은 해제해야한다.
        //장착한 무기 다 해제하고 마지막으로 선택한거 장착하기.?
        //slotnumber == 지정한 무기의 번호
        if(slotnumber >= 0 && slotnumber<=2)
        {
            //무기를 가지고 있어야 장착할 수 있다.
            if(swords[slotnumber].havethisitem)
            {
                //지정한 무기를 이미 장착하고 있다면
                if (swords[slotnumber].usethisitem == true)
                {
                    //기존무기 해제
                    swords[slotnumber].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipSword(slotnumber);
                    //기존무기 해제하고 Equip표시 해제
                    ProjectManager.instance.UImanager.equipsworditem.gameObject.SetActive(false);
                }
                else
                {
                    //다른 무기가 장착되어 있다면 해제해야함.
                    for (int i = 0; i <= 2; i++)
                    {
                        if (swords[i].usethisitem == true && i != slotnumber)
                        {
                            swords[i].usethisitem = false;
                            ProjectManager.instance.gamemanager.UnEquipSword(i);
                        }
                    }

                    //지정한 무기 장착
                    swords[slotnumber].usethisitem = true;
                    ProjectManager.instance.gamemanager.EquipSword(slotnumber);
                    //지정한 무기 장착하고 Equip표시
                    ProjectManager.instance.UImanager.equipsworditem.transform.position = ProjectManager.instance.UImanager.equipmentlayout.transform.GetChild(slotnumber).position;
                    ProjectManager.instance.UImanager.equipsworditem.gameObject.SetActive(true);
                }
            }
        }
        //방패
        else if(slotnumber >=6 && slotnumber <= 7)
        {
            //방패를 가지고 있어야 장착
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
        //갑옷
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
        //매직
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
        //악세사리
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

    //UI창에서, slotnumber 0~5:책 6~11:(6:석판), 12~17:1줄, 18~23:1줄, 24~29:1줄, 30~35:1줄, 36~41:1줄
    //스위치-case로 쓰자
    public void EquipItem(int slotnumber)
    {
        //slotnumber == 지정한 아이템의 번호
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
                //todo 아이템을 가지고 있어야 장착,해제가 가능
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

    //아이템 장비했는지 체크
    private void CheckItem(int index, int slotnumber)
    {
        //지정한 아이템을 이미 장착하고 있다면
        if (items[index].usethisitem == true)
        {
            //기존아이템 해제
            items[index].usethisitem = false;
            ProjectManager.instance.gamemanager.UnEquipItem(index);
            //기존아이템 해제하고 Equip표시 해제
            ProjectManager.instance.UImanager.equipitem.gameObject.SetActive(false);
        }
        else
        {
            //다른 아이템이 장착되어 있다면 해제해야함.
            for (int i = 0; i <= 11; i++)
            {
                if (items[i].usethisitem == true && i != index)
                {
                    items[i].usethisitem = false;
                    ProjectManager.instance.gamemanager.UnEquipItem(i);
                }
            }

            //지정한 아이템 장착
            items[index].usethisitem = true;
            ProjectManager.instance.gamemanager.EquipItem(index);
            //지정한 아이템 장착하고 Equip표시
            ProjectManager.instance.UImanager.equipitem.transform.position = ProjectManager.instance.UImanager.inventorylayout.transform.GetChild(slotnumber).position;
            ProjectManager.instance.UImanager.equipitem.gameObject.SetActive(true);
        }
    }

}
