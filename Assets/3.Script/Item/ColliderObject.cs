using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Type
{
    public bool isequipment;
    public bool isitem;
}

//이 스크립트는 뭐랄까 아이템에 관한 정보보다 아이템을 구분할 수 있게 도와주고
//충돌하는 오브젝트에 대한 스크립트니까 이벤트핸들러 같은 느낌이다.
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
