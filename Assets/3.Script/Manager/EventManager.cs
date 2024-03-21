using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //private ColliderObject colliderobject = new ColliderObject();


    //장비도 아이템에 속해있는데 분류를 정확하게 하지 않았다.
    //장비와 아이템 두 종류를 합친것을 Something이라고 하자.
    //다음에 할때는 분류를 더 명확하게 구분해서 메서드이름을 명명하자.
    //보물상자가 가지고 있는 아이템이라기 보다 줄 아이템을 보물상자의 번호에 따라 아이템을 주는 메서드
    //플레이어의 충돌로 일어난 이벤트 == 보물상자,석판,사과,열매 등등
    public void GiveSomethingFromCollide(Collider2D col)
    {
        //저 매개변수는 플레이어가 충돌할때 태그에 따라서 들어올꺼니까 어떤녀석이 부딪혔는지 알수있겠지.
        CheckTypeFromCollide(col);
        //아이템을 얻는동안 플레이어는 움직일 수 없고 ui창을 띄워준다 (todo 사운드도)
        ProjectManager.instance.UImanager.getcoroutine = StartCoroutine(ProjectManager.instance.UImanager.ShowGetSomethingUI_co(col));

    }

    //플레이어가 상점에서 Something을 살때 일어난 이벤트
    public void GiveSomethingFromShop()
    {

    }

    //장비인지 아이템인지 판별하는 메서드가 필요하지 않을까?
    public void CheckTypeFromCollide(Collider2D col)
    {
        if (col.GetComponent<ColliderObject>().somethingtype.isequipment) //장비인지 아이템인지 구분할려면 부딪힌 녀석의 정보(스크립트)가 필요하다.
        {
            GetEquipmentFromCollide(col);
        }
        else
        {
            GetItemFromCollide(col);
        }
    }

    //충돌로부터 장비를 얻는 메서드
    public void GetEquipmentFromCollide(Collider2D col)
    {
        if (col.gameObject.name == "chestbox_fire_magic")
        {
            //장비를 얻었다 -> 아이템매니저에서 관리.
            ProjectManager.instance.itemmanager.magics[0].havethisitem = true;
        }
        else if (col.gameObject.name == "chestbox_cleria_ring")
        {
            ProjectManager.instance.itemmanager.accessories[0].havethisitem = true;
        }
    }

    //충돌로부터 아이템을 얻는 메서드
    public void GetItemFromCollide(Collider2D col)
    {
        if (col.gameObject.name == "ancientslate")
        {
            ProjectManager.instance.itemmanager.items[6].havethisitem = true;
        }
        else if (col.gameObject.name == "herb")
        {
            ProjectManager.instance.itemmanager.items[8].havethisitem = true;
        }
        else if (col.gameObject.name == "apple")
        {
            ProjectManager.instance.itemmanager.items[9].havethisitem = true;
        }
        else if (col.gameObject.name == "roda")
        {
            ProjectManager.instance.itemmanager.items[10].havethisitem = true;
        }
        else if (col.gameObject.name == "mari")
        {
            ProjectManager.instance.itemmanager.items[11].havethisitem = true;
        }
    }

    //상점에서 장비를 얻는 메서드
    public void GetEquipmentFromShop()
    {

    }

    //상점에서 아이템을 얻는 메서드
    public void GetItemFromShop()
    {

    }

}
