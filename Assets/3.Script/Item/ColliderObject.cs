using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Type
{
    public bool isequipment;
    public bool isitem;
}

//�� ��ũ��Ʈ�� ������ �����ۿ� ���� �������� �������� ������ �� �ְ� �����ְ�
//�浹�ϴ� ������Ʈ�� ���� ��ũ��Ʈ�ϱ� �̺�Ʈ�ڵ鷯 ���� �����̴�.
public class ColliderObject : MonoBehaviour
{
    public Type somethingtype;

    private void Start()
    {
        if (this.gameObject.name == "chestbox_fire_magic")
        {
            this.somethingtype.isequipment = true;
        }
        else if(this.gameObject.name == "chestbox_cleria_ring")
        {
            this.somethingtype.isequipment = true;
        }
        else if(this.gameObject.name == "ancientslate")
        {
            this.somethingtype.isitem = true;
        }
        else if (this.gameObject.name == "herb")
        {
            this.somethingtype.isitem = true;
        }
        else if (this.gameObject.name == "apple")
        {
            this.somethingtype.isitem = true;
        }
        else if (this.gameObject.name == "roda")
        {
            this.somethingtype.isitem = true;
        }
        else if (this.gameObject.name == "mari")
        {
            this.somethingtype.isitem = true;
        }
    }
}
