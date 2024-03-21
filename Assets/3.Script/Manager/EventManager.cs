using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //private ColliderObject colliderobject = new ColliderObject();


    //��� �����ۿ� �����ִµ� �з��� ��Ȯ�ϰ� ���� �ʾҴ�.
    //���� ������ �� ������ ��ģ���� Something�̶�� ����.
    //������ �Ҷ��� �з��� �� ��Ȯ�ϰ� �����ؼ� �޼����̸��� �������.
    //�������ڰ� ������ �ִ� �������̶�� ���� �� �������� ���������� ��ȣ�� ���� �������� �ִ� �޼���
    //�÷��̾��� �浹�� �Ͼ �̺�Ʈ == ��������,����,���,���� ���
    public void GiveSomethingFromCollide(Collider2D col)
    {
        //�� �Ű������� �÷��̾ �浹�Ҷ� �±׿� ���� ���ò��ϱ� ��༮�� �ε������� �˼��ְ���.
        CheckTypeFromCollide(col);
        //�������� ��µ��� �÷��̾�� ������ �� ���� uiâ�� ����ش� (todo ���嵵)
        ProjectManager.instance.UImanager.getcoroutine = StartCoroutine(ProjectManager.instance.UImanager.ShowGetSomethingUI_co(col));

    }

    //�÷��̾ �������� Something�� �춧 �Ͼ �̺�Ʈ
    public void GiveSomethingFromShop()
    {

    }

    //������� ���������� �Ǻ��ϴ� �޼��尡 �ʿ����� ������?
    public void CheckTypeFromCollide(Collider2D col)
    {
        if (col.GetComponent<ColliderObject>().somethingtype.isequipment) //������� ���������� �����ҷ��� �ε��� �༮�� ����(��ũ��Ʈ)�� �ʿ��ϴ�.
        {
            GetEquipmentFromCollide(col);
        }
        else
        {
            GetItemFromCollide(col);
        }
    }

    //�浹�κ��� ��� ��� �޼���
    public void GetEquipmentFromCollide(Collider2D col)
    {
        if (col.gameObject.name == "chestbox_fire_magic")
        {
            //��� ����� -> �����۸Ŵ������� ����.
            ProjectManager.instance.itemmanager.magics[0].havethisitem = true;
        }
        else if (col.gameObject.name == "chestbox_cleria_ring")
        {
            ProjectManager.instance.itemmanager.accessories[0].havethisitem = true;
        }
    }

    //�浹�κ��� �������� ��� �޼���
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

    //�������� ��� ��� �޼���
    public void GetEquipmentFromShop()
    {

    }

    //�������� �������� ��� �޼���
    public void GetItemFromShop()
    {

    }

}
