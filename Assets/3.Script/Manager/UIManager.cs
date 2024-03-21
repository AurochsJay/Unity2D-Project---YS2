using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject saveUI;
    [SerializeField] private GameObject loadUI;
    [SerializeField] private GameObject gamemenuUI;
    [SerializeField] private GameObject playerinfoUI;
    [SerializeField] public GameObject fadeimage;
    

    [Header("PlayerStateInfo")]
    [SerializeField] private Slider slider_maxHP;
    [SerializeField] private Slider slider_HP;
    [SerializeField] private Slider slider_maxMP;
    [SerializeField] private Slider slider_MP;
    [SerializeField] private Slider slider_monstermaxHP;
    [SerializeField] private Slider slider_monsterHP;
    [SerializeField] private Text HPtext;
    [SerializeField] private Text MPtext;
    [SerializeField] private Text EXPtext;
    [SerializeField] private Text Goldtext;

    [Header("TabInfo")]
    [SerializeField] private Text tapLV;
    [SerializeField] private Text tapHP;
    [SerializeField] private Text tapMP;
    [SerializeField] private Text tapSTR;
    [SerializeField] private Text tapDEF;
    [SerializeField] private Text tapEXP;
    [SerializeField] private Text tapsword;
    [SerializeField] private Text tapshield;
    [SerializeField] private Text taparmor;
    [SerializeField] private Text tapmagic;
    [SerializeField] private Text tapaccessory;
    [SerializeField] private Text tapitem;
    [SerializeField] private Text fieldname;

    [Header("EquipmentComponent")]
    [SerializeField] public GameObject equipmentlayout;
    [SerializeField] private GameObject selectequip;
    [SerializeField] private Image equipimage;
    [SerializeField] private Text equipname;
    [SerializeField] private Text equipinfo;
    [SerializeField] private Text equipstr;
    [SerializeField] private Text equipdef;
    [SerializeField] private Text equipmagic;
    [SerializeField] private Text equipinv;
    [SerializeField] public Image equipsworditem;
    [SerializeField] public Image equipshielditem;
    [SerializeField] public Image equiparmoritem;
    [SerializeField] public Image equipmagicitem;
    [SerializeField] public Image equipaccessoryitem;

    [Header("InventoryComponent")]
    [SerializeField] public GameObject inventorylayout;
    [SerializeField] private GameObject selectinven;
    [SerializeField] private Image invenimage;
    [SerializeField] private Text invenname;
    [SerializeField] private Text inveninfo;
    [SerializeField] public Image equipitem;

    [Header("GetSomething")]
    [SerializeField] public GameObject getsomethingUI;
    [SerializeField] public Text gettext;

    [Header("Shop")]
    [SerializeField] public GameObject shopUI;
    [SerializeField] private Image npcimage;
    [SerializeField] private Text shopname;
    [SerializeField] public GameObject shoplayout;
    [SerializeField] private Text shopgold;
    [SerializeField] private GameObject selecttext;

    [Header("GameMenu")]
    [SerializeField] public GameObject gamemenulayout;
    [SerializeField] private GameObject selectmenu;



    //FadeIn FadeOut ó������
    private Image image;
    private float fadeinTime = 1f; //���̵� Ÿ��
    private float fadeoutTime = 0.1f;
    private float accumTime = 0f; // �ð�����
    private Coroutine fade_co;

    //�ڷ�ƾ ó��
    Coroutine equipcoroutine;
    Coroutine invencoroutine;
    public Coroutine getcoroutine;
    public Coroutine shopcoroutine;
    public Coroutine checkshopcoroutine;
    Coroutine gamemenucoroutine;

    //���� �ڷ�ƾ ó������
    private bool iscorunning = false;

    private void Start()
    {
        //Fade ���� �ʱ�ȭ
        image = fadeimage.GetComponent<Image>();
    }

    private void Update()
    {
        ShowPlayerSliderUI();
        ShowPlayerTextUI();
        ShowMonsterUI();
    }

    //�÷��̾� HP,MP Slider ǥ��
    private void ShowPlayerSliderUI()
    {
        slider_maxHP.value = ProjectManager.instance.gamemanager.maxHP;
        slider_HP.value = ProjectManager.instance.gamemanager.currentHP;
        slider_maxMP.value = ProjectManager.instance.gamemanager.maxMP;
        slider_MP.value = ProjectManager.instance.gamemanager.currentMP;
    }

    //�÷��̾� HP,MP,EXP,Gold ǥ��
    private void ShowPlayerTextUI()
    {
        HPtext.text = ProjectManager.instance.gamemanager.currentHP.ToString();
        MPtext.text = ProjectManager.instance.gamemanager.currentMP.ToString();
        EXPtext.text = (ProjectManager.instance.gamemanager.maxEXP - ProjectManager.instance.gamemanager.currentEXP).ToString();
        Goldtext.text = ProjectManager.instance.gamemanager.Gold.ToString();
    }


    //���� ü�� UI ǥ�� �� ������Ʈ
    private void ShowMonsterUI()
    {
        if(ProjectManager.instance.gamemanager.targetmonster == true)
        {
            slider_monstermaxHP.gameObject.SetActive(true);
            slider_monsterHP.gameObject.SetActive(true);
            slider_monstermaxHP.value = ProjectManager.instance.gamemanager.monstermaxHP;
            slider_monsterHP.value = ProjectManager.instance.gamemanager.monsterHP;
        }
        else if (ProjectManager.instance.gamemanager.targetboss == true)
        {
            slider_monstermaxHP.gameObject.SetActive(true);
            slider_monsterHP.gameObject.SetActive(true);
            slider_monstermaxHP.value = ProjectManager.instance.gamemanager.boss.maxHP;
            slider_monsterHP.value = ProjectManager.instance.gamemanager.boss.HP;
        }
        else //ü��UI ��Ȱ��ȭ
        {
            slider_monstermaxHP.gameObject.SetActive(false);
            slider_monsterHP.gameObject.SetActive(false);
        }
    }

    //���â ǥ��
    public void ShowEquipmentUI()
    {
        Debug.Log("���� ���԰���");
        if (!equipmentUI.activeSelf)
        {
            //�ٸ� ǥ��â�� ����
            playerinfoUI.SetActive(false);
            inventoryUI.SetActive(false);

            equipmentUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
            Debug.Log("���⵵ ���԰���");
            equipcoroutine = StartCoroutine(SelectEquip_co());
        }
        else
        {
            equipmentUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
            StopCoroutine(equipcoroutine);
        }
    }

    //������â ǥ��
    public void ShowInventoryUI()
    {
        if (!inventoryUI.activeSelf)
        {
            //�ٸ� ǥ��â�� ����
            playerinfoUI.SetActive(false);
            equipmentUI.SetActive(false);

            inventoryUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
            invencoroutine = StartCoroutine(SelectInven_co());
        }
        else
        {
            inventoryUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
            StopCoroutine(invencoroutine);
        }
    }

    //�÷��̾�����â ǥ��
    public void ShowPlayerInfoUI()
    {
        //tapLV.text = "LV : " + ProjectManager.instance.gamemanager.level.ToString();
        //tapHP.text = "HP : " + ProjectManager.instance.gamemanager.currentHP.ToString() + " / " + ProjectManager.instance.gamemanager.maxHP.ToString();
        //tapMP.text = "MP : " + ProjectManager.instance.gamemanager.currentMP.ToString() + " / " + ProjectManager.instance.gamemanager.maxMP.ToString();
        //tapSTR.text = "STR : " + ProjectManager.instance.gamemanager.ATK.ToString();
        //tapDEF.text = "DEF : " + ProjectManager.instance.gamemanager.DEF.ToString();
        //tapEXP.text = "EXP : " + (ProjectManager.instance.gamemanager.maxEXP - ProjectManager.instance.gamemanager.currentEXP ).ToString();
        //tapsword.text = ProjectManager.instance.gamemanager.sword.name;
        //tapshield.text = ProjectManager.instance.gamemanager.shield.name;
        //taparmor.text = ProjectManager.instance.gamemanager.armor.name;
        //tapmagic.text = ProjectManager.instance.gamemanager.magic.name;
        //tapaccessory.text = ProjectManager.instance.gamemanager.accessory.name;
        ////tapitem.text = ProjectManager.instance.gamemanager.level.ToString();
        //fieldname.text = "��������";


        if (!playerinfoUI.activeSelf)
        {
            playerinfoUI.SetActive(true);
        }
        else
        {
            playerinfoUI.SetActive(false);
        }

        StartCoroutine(UpdatePlayerInfoUI_co());

    }

    //���Ӹ޴�â ǥ��
    public void ShowGameMenuUI()
    {
        if (!gamemenuUI.activeSelf)
        {
            gamemenuUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
            gamemenucoroutine = StartCoroutine(SelectMenu_co());
            //if(Input.GetKeyDown(KeyCode.Escape))
            //{
            //    gamemenuUI.SetActive(false);
            //    ProjectManager.instance.useoptionmenu = false;
            //}
        }
        else
        {
            gamemenuUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
        }
    }

    //���̺�â ǥ��
    public void ShowSaveUI()
    {
        if (!saveUI.activeSelf)
        {
            saveUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
        }
        else
        {
            saveUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
        }
    }

    //�ε�â ǥ��
    public void ShowLoadUI()
    {
        if (!loadUI.activeSelf)
        {
            loadUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
        }
        else
        {
            loadUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
        }
    }

    //�޴�â �ݱ�, escŰ�� ������ �߻��Ѵ�.
    public void CloseMenuUI()
    {
        equipmentUI.SetActive(false);
        inventoryUI.SetActive(false);
        gamemenuUI.SetActive(false);
        ProjectManager.instance.useoptionmenu = false;
    }


    #region ���âUI
    //���â�� �������� selectitem�� �������߰���. selectitem�� [specializefield]�� �����Ѵ�.
    public IEnumerator SelectEquip_co()
    {
        int[,] slotsize = new int[5,6];
        int row = 0; // ������ �����϶�
        int column = 0; // ���� �����϶�
        for(int i = 0; i < 5; i ++)
        {
            for(int j = 0; j < 6; j++)
            {
                slotsize[i, j] = i*6+j;
                //������ �迭�� ���鶧 ��� ������ ������ �̹��� ���, ������ �̹��� ��� X
                CheckHaveEquipmentForSlotUI(slotsize[i, j]);
            }
        }
        
        //selectequip.transform.position = equipmentUI.transform.GetChild(0).position;
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //�Է¹޴°ž� �� �ϰ�, ������ ��� �������� �ű���̳Ĵ°�, ���߹迭�� ���� ������
                //���߹迭 ũ�� ���� 6 x ���� 5 ������� �� row, �� column
                if (row < 5)
                {
                    row++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (row > 0)
                {
                    row--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (column > 0)
                {
                    column--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (column < 4)
                {
                    column++;
                }
            }

            selectequip.transform.position = equipmentlayout.transform.GetChild(slotsize[column, row]).position;
            //slotsize ���߹迭�� ���� ���� switch-case�� �ֱ�
            ShowEquipSlotInfo(slotsize[column, row]);

            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                //��� ����, ��� ���� �޼��带 ȣ���ϱ� �Ҳ���, GameManager���� �����ϴ°� �´�.
                //�÷��̾��� ���ݷ°� ���¿� ���ѰŴϱ�.
                //�ƴϴ� ��� �����ϴ°Ŵϱ� �����۸Ŵ������� usethisitem bool������ �����
                //�� ����� ������ Gamemanager�� �Ѱܾ���
                ProjectManager.instance.itemmanager.UseEquipment(slotsize[column,row]);
            }

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
            {
                //â ����
                equipmentUI.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            }
            Debug.Log("�ڷ�ƾ�� ��� ���ư����� Ȯ��. Stop�ڷ�ƾ�� ��������");
            yield return null;
        }
        //Debug.Log("�Ƹ� �ڷ�ƾ�� ��� ȣ��ǰ���");
        yield return new WaitForSeconds(0.3f);
    }

    //���â EquipmentUI�� �ڽĵ�(����)���� ������ �Ѱ��� // �ڽ����� ������ �޾Ƶ��� ������ ����� �ű⿡ �Ҵ��Ѵ�.?
    //�����Ҷ� �־��ָ� �ǰ���.
    //public void SlotInfo(int childnumber)
    //{
    //    //ItemManager[] itemmangers = new ItemManager[equipmentUI.transform.childCount];
    //    //for(int i = 0; i < equipmentUI.transform.childCount; i++)
    //    //{
    //    //    itemmangers[i].swords[0] = ProjectManager.instance.itemmanager.swords[i];
    //    //}
    //    //childnumber�� 0~5:��, 6~11:����, 12~17:�Ƹ�, 18~23:����, 24~29:�Ǽ��縮
    //    equipmentUI.transform.GetChild(0).GetComponent<ItemManager>().swords[0] = ProjectManager.instance.itemmanager.swords[0];
    //    equipmentUI.transform.GetChild(1).GetComponent<ItemManager>().swords[1] = ProjectManager.instance.itemmanager.swords[1];
    //    equipmentUI.transform.GetChild(2).GetComponent<ItemManager>().swords[2] = ProjectManager.instance.itemmanager.swords[2];
    //    equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[2] = ProjectManager.instance.itemmanager.swords[2];
    //    equipmentUI.transform.GetChild(childnumber).gameObject.AddComponent<ItemManager>();
    //    equipmentUI.transform.GetChild(childnumber).gameObject.AddComponent<ItemManager>();
    //    equipmentUI.transform.GetChild(0).gameObject.AddComponent<ItemManager>();
    //}

    //slot�� ��������� ������� ���� �����. ����? selectitem�� �����Ҷ�
    public void ShowEquipSlotInfo(int childnumber)
    {
        //Debug.Log("���� ����" + childnumber);
        switch (childnumber)
        {
            case 0:
                CheckHaveEquipmentForInfo(0);
                break;
            case 1:
                CheckHaveEquipmentForInfo(1);
                break;
            case 2:
                CheckHaveEquipmentForInfo(2);
                break;
            case 6:
                CheckHaveEquipmentForInfo(6);
                break;
            case 7:
                CheckHaveEquipmentForInfo(7);
                break;
            case 12:
                CheckHaveEquipmentForInfo(12);
                break;
            case 13:
                CheckHaveEquipmentForInfo(13);
                break;
            case 14:
                CheckHaveEquipmentForInfo(14);
                break;
            case 18:
                CheckHaveEquipmentForInfo(18);
                break;
            case 24:
                CheckHaveEquipmentForInfo(24);
                break;
            default:
                equipimage.sprite = Resources.Load<Sprite>("ys2 Black image");
                equipname.text = "";
                equipinfo.text = "";
                equipmagic.text = "";
                equipinv.text = "";
                Debug.Log("���� ���� �������̱⶧���� ������� �ʽ��ϴ�.");
                break;
        }
        equipstr.text = ProjectManager.instance.gamemanager.ATK.ToString();
        equipdef.text = ProjectManager.instance.gamemanager.DEF.ToString();

        //equipimage = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].image;
        //equipname.text = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].name;
        //equipinfo.text = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].info;
    }

    //��� ������ ������ ���â���� �������, ������ X
    private void CheckHaveEquipmentForInfo(int index)
    {
        if (index >= 0 && index <= 2)
        {
            if (ProjectManager.instance.itemmanager.swords[index].havethisitem)
            {
                AllocateEquipmentInfo(index);
            }
            else
            {
                AllocateEquipmentInfo_false();
            }
        }
        else if (index >= 6 && index <= 7)
        {
            if (ProjectManager.instance.itemmanager.shields[index-6].havethisitem)
            {
                AllocateEquipmentInfo(index);
            }
            else
            {
                AllocateEquipmentInfo_false();
            }
        }
        else if (index >= 12 && index <= 14)
        {
            if (ProjectManager.instance.itemmanager.armors[index-12].havethisitem)
            {
                AllocateEquipmentInfo(index);
            }
            else
            {
                AllocateEquipmentInfo_false();
            }
        }
        else if (index == 18)
        {
            if (ProjectManager.instance.itemmanager.magics[index-18].havethisitem)
            {
                AllocateEquipmentInfo(index);
            }
            else
            {
                AllocateEquipmentInfo_false();
            }
        }
        else if (index == 24)
        {
            if (ProjectManager.instance.itemmanager.accessories[index-24].havethisitem)
            {
                AllocateEquipmentInfo(index);
            }
            else
            {
                AllocateEquipmentInfo_false();
            }
        }

        
    }

    //��� ���� ���
    private void AllocateEquipmentInfo(int index)
    {
        if(index >=0 && index <= 2)
        {
            equipimage.sprite = ProjectManager.instance.itemmanager.swords[index].image;
            equipname.text = ProjectManager.instance.itemmanager.swords[index].name;
            equipinfo.text = ProjectManager.instance.itemmanager.swords[index].info;
        }
        else if(index >=6 && index <=7)
        {
            equipimage.sprite = ProjectManager.instance.itemmanager.shields[index - 6].image;
            equipname.text = ProjectManager.instance.itemmanager.shields[index - 6].name;
            equipinfo.text = ProjectManager.instance.itemmanager.shields[index - 6].info;
        }
        else if(index >= 12 && index <= 14)
        {
            equipimage.sprite = ProjectManager.instance.itemmanager.armors[index - 12].image;
            equipname.text = ProjectManager.instance.itemmanager.armors[index - 12].name;
            equipinfo.text = ProjectManager.instance.itemmanager.armors[index - 12].info;
        }
        else if(index == 18)
        {
            equipimage.sprite = ProjectManager.instance.itemmanager.magics[0].image;
            equipname.text = ProjectManager.instance.itemmanager.magics[0].name;
            equipinfo.text = ProjectManager.instance.itemmanager.magics[0].info;
        }
        else if(index == 24)
        {
            equipimage.sprite = ProjectManager.instance.itemmanager.accessories[0].image;
            equipname.text = ProjectManager.instance.itemmanager.accessories[0].name;
            equipinfo.text = ProjectManager.instance.itemmanager.accessories[0].info;
        }
    }

    //�������� ������ ���� �ʴٸ� 
    private void AllocateEquipmentInfo_false()
    {
        equipimage.sprite = Resources.Load<Sprite>("ys2 Black image");
        equipname.text = "";
        equipinfo.text = "";
        equipmagic.text = "";
        equipinv.text = "";
    }

    //��� ������ �ִ��� üũ
    private void CheckHaveEquipmentForSlotUI(int childnumber)
    {
        switch (childnumber)
        {
            case 0:
                CheckColorEquipment(0, childnumber);
                break;
            case 1:
                CheckColorEquipment(1, childnumber);
                break;
            case 2:
                CheckColorEquipment(2, childnumber);
                break;
            case 6:
                CheckColorEquipment(0, childnumber);
                break;
            case 7:
                CheckColorEquipment(1, childnumber);
                break;
            case 12:
                CheckColorEquipment(0, childnumber);
                break;
            case 13:
                CheckColorEquipment(1, childnumber);
                break;
            case 14:
                CheckColorEquipment(2, childnumber);
                break;
            case 18:
                CheckColorEquipment(0, childnumber);
                break;
            case 24:
                CheckColorEquipment(0, childnumber);
                break;
            default:
                break;
        }
    }
   

    //��� ������ �̹��� ���̰�, �Ⱥ��̰�
    private void CheckColorEquipment(int index, int childnumber)
    {
        Color color_origin = new Color();
        color_origin.r = 255;
        color_origin.g = 255;
        color_origin.b = 255;
        color_origin.a = 255;
        Color color_false = new Color();
        color_false.a = 0;

        if(childnumber >=0 && childnumber <=2) //����
        {
            if (ProjectManager.instance.itemmanager.swords[index].havethisitem)
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
            }
            else
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
            }
        }
        else if(childnumber >= 6 && childnumber <= 7) //����
        {
            if (ProjectManager.instance.itemmanager.shields[index].havethisitem)
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
            }
            else
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
            }
        }
        else if(childnumber >= 12 && childnumber <= 14) //����
        {
            if (ProjectManager.instance.itemmanager.armors[index].havethisitem)
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
            }
            else
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
            }
        }
        else if(childnumber == 18) //����
        {
            if (ProjectManager.instance.itemmanager.magics[index].havethisitem)
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
            }
            else
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
            }
        }
        else if(childnumber == 24) //�Ǽ�
        {
            if (ProjectManager.instance.itemmanager.accessories[index].havethisitem)
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
            }
            else
            {
                equipmentlayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
            }
        }
        
    }
    #endregion

    #region ������âUI
    //������â
    public IEnumerator SelectInven_co()
    {
        int[,] slotsize = new int[7, 6];
        int row = 0; // ������ �����϶�
        int column = 0; // ���� �����϶�
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                slotsize[i, j] = i * 6 + j;
                //������ �迭�� ���鶧 �������� ������ ������ �̹��� ���, ������ �̹��� ��� X
                CheckHaveItemForSlotUI(slotsize[i,j]);
            }
        }

        //selectequip.transform.position = equipmentUI.transform.GetChild(0).position;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //���߹迭 ũ�� ���� 6 x ���� 7 ������� �� row, �� column
                if (row < 5)
                {
                    row++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (row > 0)
                {
                    row--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (column > 0)
                {
                    column--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (column < 6)
                {
                    column++;
                }
            }

            selectinven.transform.position = inventorylayout.transform.GetChild(slotsize[column, row]).position;
            //slotsize ���߹迭�� ���� ���� switch-case�� �ֱ�
            ShowInvenSlotInfo(slotsize[column, row]);

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                ProjectManager.instance.itemmanager.EquipItem(slotsize[column, row]);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
            {
                //â ����
                inventoryUI.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            }

            yield return null;
        }
        //Debug.Log("�Ƹ� �ڷ�ƾ�� ��� ȣ��ǰ���");
        yield return new WaitForSeconds(0.3f);
    }

    //������ �������ִ��� üũ, ������ �κ��丮 ������ �������̹��� ����
    private void CheckHaveItemForSlotUI(int childnumber)
    {
        
        switch (childnumber)
        {
            case 0:
                CheckColorItem(0, childnumber);
                break;
            case 1:
                CheckColorItem(1, childnumber);
                break;
            case 2:
                CheckColorItem(2, childnumber);
                break;
            case 3:
                CheckColorItem(3, childnumber);
                break;
            case 4:
                CheckColorItem(4, childnumber);
                break;
            case 5:
                CheckColorItem(5, childnumber);
                break;
            case 6:
                CheckColorItem(6,childnumber);
                break;
            case 25:
                CheckColorItem(7, childnumber);
                break;
            case 31:
                CheckColorItem(8, childnumber);
                break;
            case 32:
                CheckColorItem(9, childnumber);
                break;
            case 33:
                CheckColorItem(10, childnumber);
                break;
            case 34:
                CheckColorItem(11, childnumber);
                break;
            default:
                break;
        }

    }

    //������ ������ �̹��� ���̰�, �Ⱥ��̰�
    private void CheckColorItem(int index, int childnumber)
    {
        Color color_origin = new Color();
        color_origin.r = 255;
        color_origin.g = 255;
        color_origin.b = 255;
        color_origin.a = 255;
        Color color_false = new Color();
        color_false.a = 0;
        if (ProjectManager.instance.itemmanager.items[index].havethisitem)
        {
            inventorylayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_origin;
        }
        else
        {
            inventorylayout.transform.GetChild(childnumber).GetComponent<Image>().color = color_false;
        }
    }

    //�κ��丮 ���������� ���
    public void ShowInvenSlotInfo(int childnumber)
    {
        Debug.Log("���� ����" + childnumber);
        switch (childnumber)
        {
            case 0:
                CheckHaveItemForInfo(0);
                break;
            case 1:
                CheckHaveItemForInfo(1);
                break;
            case 2:
                CheckHaveItemForInfo(2);
                break;
            case 3:
                CheckHaveItemForInfo(3);
                break;
            case 4:
                CheckHaveItemForInfo(4);
                break;
            case 5:
                CheckHaveItemForInfo(5);
                break;
            case 6:
                //�������� ������, ������
                CheckHaveItemForInfo(6);
                break;
            case 25:
                CheckHaveItemForInfo(7);
                break;
            case 31:
                CheckHaveItemForInfo(8);
                break;
            case 32:
                CheckHaveItemForInfo(9);
                break;
            case 33:
                CheckHaveItemForInfo(10);
                break;
            case 34:
                CheckHaveItemForInfo(11);
                break;
            default:
                AllocateInventoryInfo_false();
                Debug.Log("���� ���� �������̱⶧���� ������� �ʽ��ϴ�.");
                break;
        }
    }

    private void CheckHaveItemForInfo(int index)
    {
        if (ProjectManager.instance.itemmanager.items[index].havethisitem)
        {
            AllocateInventoryInfo(index);
        }
        else
        {
            AllocateInventoryInfo_false();
        }
    }

    //������ ���� //����ϰ��� �ϴ� �������� �Ҵ��ϴ� �޼���
    private void AllocateInventoryInfo(int index)
    {
        invenimage.sprite = ProjectManager.instance.itemmanager.items[index].image;
        invenname.text = ProjectManager.instance.itemmanager.items[index].name;
        inveninfo.text = ProjectManager.instance.itemmanager.items[index].info;
    }

    //�������� ������ ���� �ʴٸ� 
    private void AllocateInventoryInfo_false()
    {
        invenimage.sprite = Resources.Load<Sprite>("ys2 Black image");
        invenname.text = "";
        inveninfo.text = "";
    }
    #endregion

    //TabŰ �÷��̾����� ��� �����������
    private IEnumerator UpdatePlayerInfoUI_co()
    {
        while(true)
        {
            tapLV.text = "LV : " + ProjectManager.instance.gamemanager.level.ToString();
            tapHP.text = "HP : " + ProjectManager.instance.gamemanager.currentHP.ToString() + " / " + ProjectManager.instance.gamemanager.maxHP.ToString();
            tapMP.text = "MP : " + ProjectManager.instance.gamemanager.currentMP.ToString() + " / " + ProjectManager.instance.gamemanager.maxMP.ToString();
            tapSTR.text = "STR : " + ProjectManager.instance.gamemanager.ATK.ToString();
            tapDEF.text = "DEF : " + ProjectManager.instance.gamemanager.DEF.ToString();
            tapEXP.text = "EXP : " + (ProjectManager.instance.gamemanager.maxEXP - ProjectManager.instance.gamemanager.currentEXP).ToString();
            tapsword.text = ProjectManager.instance.gamemanager.sword.name;
            tapshield.text = ProjectManager.instance.gamemanager.shield.name;
            taparmor.text = ProjectManager.instance.gamemanager.armor.name;
            tapmagic.text = ProjectManager.instance.gamemanager.magic.name;
            tapaccessory.text = ProjectManager.instance.gamemanager.accessory.name;
            tapitem.text = ProjectManager.instance.gamemanager.item.name;
            fieldname.text = "��������";

            yield return null;
        }
    }

    //�浹�ؼ� ������ ������� �ߴ� â
    public IEnumerator ShowGetSomethingUI_co(Collider2D col)
    {
        getsomethingUI.SetActive(true);
        if (col.gameObject.name == "chestbox_fire_magic")
        {
            gettext.text = ProjectManager.instance.itemmanager.magics[0].name + " Get!";
        }
        else if (col.gameObject.name == "chestbox_cleria_ring")
        {
            gettext.text = ProjectManager.instance.itemmanager.accessories[0].name + " Get!";
        }
        else if (col.gameObject.name == "ancientslate")
        {
            gettext.text = ProjectManager.instance.itemmanager.items[6].name + " Get!";
        }
        else if (col.gameObject.name == "herb")
        {
            gettext.text = ProjectManager.instance.itemmanager.items[8].name + " Get!";
        }
        else if (col.gameObject.name == "apple")
        {
            gettext.text = ProjectManager.instance.itemmanager.items[9].name + " Get!";
        }
        else if (col.gameObject.name == "roda")
        {
            gettext.text = ProjectManager.instance.itemmanager.items[10].name + " Get!";
        }
        else if (col.gameObject.name == "mari")
        {
            gettext.text = ProjectManager.instance.itemmanager.items[11].name + " Get!";
        }
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
            {
                //â ����
                getsomethingUI.SetActive(false);
                ProjectManager.instance.iscolliderobject = false;
                //�ʵ忡 �ִ� ������ ����
                Destroy(col.gameObject);
                //StopCoroutine(getcoroutine());
                break;
            }
            yield return null;
        }

        Debug.Log("������ ��� �ڷ�ƾ ��� ����������");

        yield return null;
    }

    //NPC ������ ����UI ���
    public IEnumerator ShowShopUI_co(Collider2D col)
    {
        shopUI.SetActive(true);
        if(col.gameObject.name == "���")
        {

            npcimage.sprite = Resources.Load<Sprite>("Shop/ys2 ��� image");
            shopname.text = "����� ������";
            shopgold.text = ProjectManager.instance.gamemanager.Gold.ToString() + " Gold";
            shoplayout.transform.GetChild(0).gameObject.SetActive(true);
            shoplayout.transform.GetChild(0).GetComponent<Text>().text = "���⸦ ���";
            shoplayout.transform.GetChild(1).gameObject.SetActive(true);
            shoplayout.transform.GetChild(1).GetComponent<Text>().text = "���и� ���";
            shoplayout.transform.GetChild(2).gameObject.SetActive(true);
            shoplayout.transform.GetChild(2).GetComponent<Text>().text = "������ ���";
            shoplayout.transform.GetChild(3).gameObject.SetActive(true);
            shoplayout.transform.GetChild(3).GetComponent<Text>().text = "������";

            //4���� �迭 �����
            int[] textsize = new int[4];
            int column = 0;
            for(int i = 0; i<textsize.Length; i++)
            {
                textsize[i] = i;
            }

            while(true)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(column > 0)
                    {
                        column--;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(column < 3)
                    {
                        column++;
                    }
                }

                //����â ������ ����
                selecttext.transform.position = shoplayout.transform.GetChild(textsize[column]).transform.position;
                
                if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X)) && !iscorunning)
                {
                    iscorunning = true;
                    yield return checkshopcoroutine = StartCoroutine(CheckText_EquipmentShop(textsize[column], col));
                    yield return new WaitForSeconds(5f);
                    yield return shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                    iscorunning = false;
                    //shoplayout.transform.GetChild(0).GetComponent<Text>().text = "���⸦ ���";
                    //shoplayout.transform.GetChild(1).GetComponent<Text>().text = "���и� ���";
                    //shoplayout.transform.GetChild(2).GetComponent<Text>().text = "������ ���";
                    //shoplayout.transform.GetChild(3).GetComponent<Text>().text = "������";
                    //StopCoroutine(ShowShopUI_co(col));
                }

                yield return null;
            }
        }
        else if(col.gameObject.name == "���̵�")
        {
            
        }
        else if(col.gameObject.name == "����")
        {

        }
        else if(col.gameObject.name == "�ٳ��")
        {

        }

        yield return null;
    }

    private IEnumerator CheckText_EquipmentShop(int index, Collider2D col)
    {
        switch (index)
        {
            case 0: // ���⼱��
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Short Sword�� ���.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Long Sword�� ���.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "Talwar�� ���.";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "�ڷΰ���";

                //4���� �迭 �����
                int[] textsize = new int[4];
                int column = 0;
                for (int i = 0; i < textsize.Length; i++)
                {
                    textsize[i] = i;
                }

                while (true)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (column > 0)
                        {
                            column--;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (column < 3)
                        {
                            column++;
                        }
                    }

                    //����â ������ ����
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize[column]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //���� ����
                        PurchaseSword(textsize[column], col);
                        //shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                        iscorunning = false;
                        yield break;
                        //shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                        //StopCoroutine(checkshopcoroutine);
                        break;
                    }

                    yield return null;
                }

                break;
            case 1: // ���м���
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Wooden Shield�� ���.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Small Shield�� ���.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "�ڷΰ���";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "";

                //4���� �迭 �����
                int[] textsize1 = new int[4];
                int column1 = 0;
                for (int i = 0; i < textsize1.Length; i++)
                {
                    textsize1[i] = i;
                }

                while (true)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (column1 > 0)
                        {
                            column1--;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (column1 < 3)
                        {
                            column1++;
                        }
                    }

                    //����â ������ ����
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize1[column1]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //���� ����
                        PurchaseShield(textsize1[column1], col);
                        iscorunning = false;
                        yield break;
                        //shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                        //StopCoroutine(checkshopcoroutine);
                        break;
                    }

                    yield return null;
                }
                break;
            case 2: // ���ʱ���
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Chain Mail�� ���.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Breast Plate�� ���.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "Plate Mail�� ���.";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "�ڷΰ���";

                //4���� �迭 �����
                int[] textsize2 = new int[4];
                int column2 = 0;
                for (int i = 0; i < textsize2.Length; i++)
                {
                    textsize2[i] = i;
                }

                while (true)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (column2 > 0)
                        {
                            column2--;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (column2 < 3)
                        {
                            column2++;
                        }
                    }

                    //����â ������ ����
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize2[column2]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //���� ����
                        PurchaseArmor(textsize2[column2], col);
                        iscorunning = false;
                        yield break;
                        //shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                        //StopCoroutine(checkshopcoroutine);
                        break;
                    }

                    yield return null;
                }

                break;
            case 3: // ���ư���
                //StopCoroutine(checkshopcoroutine);
                //StopCoroutine(ShowShopUI_co(col));
                iscorunning = false;
                ProjectManager.instance.isdialogue = false;
                shopUI.SetActive(false);
                StopCoroutine(shopcoroutine);
                yield break;
                break;
        }

    }

    //���� ����
    private void PurchaseSword(int index, Collider2D col)
    {
        switch (index)
        {
            case 0:
                if (ProjectManager.instance.gamemanager.Gold >= 100)
                {
                    ProjectManager.instance.gamemanager.Gold -= 100;
                    ProjectManager.instance.itemmanager.swords[index].havethisitem = true;
                }
                else
                {
                    Debug.Log("���� ����");
                }
                break;
            case 1:
                if (ProjectManager.instance.gamemanager.Gold >= 200)
                {
                    ProjectManager.instance.gamemanager.Gold -= 200;
                    ProjectManager.instance.itemmanager.swords[index].havethisitem = true;
                }
                else
                {
                    Debug.Log("���� ����");
                }
                break;
            case 2:
                if (ProjectManager.instance.gamemanager.Gold >= 500)
                {
                    ProjectManager.instance.gamemanager.Gold -= 500;
                    ProjectManager.instance.itemmanager.swords[index].havethisitem = true;
                }
                else
                {
                    Debug.Log("���� ����");
                }
                break;
            case 3:
                StopCoroutine(ShowShopUI_co(col));
                shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                StopCoroutine(checkshopcoroutine);
                break;
            default:
                break;
        }
    }

    //���� ����
    private void PurchaseShield(int index, Collider2D col)
    {
        switch (index)
        {
            case 0:
                if (ProjectManager.instance.gamemanager.Gold >= 100)
                {
                    ProjectManager.instance.gamemanager.Gold -= 100;
                    ProjectManager.instance.itemmanager.shields[index].havethisitem = true;
                }
                break;
            case 1:
                if (ProjectManager.instance.gamemanager.Gold >= 200)
                {
                    ProjectManager.instance.gamemanager.Gold -= 200;
                    ProjectManager.instance.itemmanager.shields[index].havethisitem = true;
                }
                break;
            case 2:
                shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                StopCoroutine(checkshopcoroutine);
                break;
            default:

                break;
        }
    }

    //���� ����
    private void PurchaseArmor(int index, Collider2D col)
    {
        switch (index)
        {
            case 0:
                if (ProjectManager.instance.gamemanager.Gold >= 100)
                {
                    ProjectManager.instance.gamemanager.Gold -= 100;
                    ProjectManager.instance.itemmanager.armors[index].havethisitem = true;
                }
                break;
            case 1:
                if (ProjectManager.instance.gamemanager.Gold >= 200)
                {
                    ProjectManager.instance.gamemanager.Gold -= 200;
                    ProjectManager.instance.itemmanager.armors[index].havethisitem = true;
                }
                break;
            case 2:
                if (ProjectManager.instance.gamemanager.Gold >= 500)
                {
                    ProjectManager.instance.gamemanager.Gold -= 500;
                    ProjectManager.instance.itemmanager.armors[index].havethisitem = true;
                }
                break;
            case 3:
                shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                StopCoroutine(checkshopcoroutine);
                break;
            default:
                break;
        }
    }

    //���Ӹ޴� ����
    public IEnumerator SelectMenu_co()
    {
        //4���� �迭 �����
        int[] textsize = new int[6];
        int column = 0;
        for (int i = 0; i < textsize.Length; i++)
        {
            textsize[i] = i;
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (column > 0)
                {
                    column--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (column < 5)
                {
                    column++;
                }
            }

            //����â ������ ����
            selectmenu.transform.position = gamemenulayout.transform.GetChild(textsize[column]).position;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X))
            {
                CheckGameMenu(textsize[column]);
                break;
            }

            yield return null;
        }
        yield return null;
    }

    private void CheckGameMenu(int index)
    {
        switch (index)
        {
            case 0: // �����ϱ�
                ProjectManager.instance.datamanager.SaveGameDataToJson();
                gamemenuUI.gameObject.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            case 1: // �ҷ�����
                ProjectManager.instance.datamanager.LoadGameDataFromJson();
                ProjectManager.instance.datamanager.DataFileToGameManager();
                gamemenuUI.gameObject.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            case 2: // ���â
                ProjectManager.instance.UImanager.ShowEquipmentUI();
                gamemenuUI.gameObject.SetActive(false);
                break;
            case 3: // ������â
                ProjectManager.instance.UImanager.ShowInventoryUI();
                gamemenuUI.gameObject.SetActive(false);
                break;
            case 4: // Ÿ��Ʋ
                SceneManager.LoadScene("Title");
                break;
            case 5: // ���ư���
                ProjectManager.instance.useoptionmenu = false;
                gamemenuUI.gameObject.SetActive(false);
                StopCoroutine(gamemenucoroutine);
                break;
            default:
                break;
        }

    }



    //Fadein Fadeout �޼���
    public void StartFadeIn() // ȣ�� �Լ� Fade In�� ����
    {
        if (fade_co != null)
        {
            StopCoroutine(FadeIn());
            StopCoroutine(FadeOut());
            fade_co = null;
            Debug.Log("fade_co�� != null�� true�� �Ϸ� �Դ�");
        }

        fade_co = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn() // �ڷ�ƾ�� ���� ���̵� �� �ð�����, fade in�� ������ ���̰�
    {
        Debug.Log("���̵� ���Գ�");

        Color color = image.color;

        accumTime = 0f;
        color.a = 1f;
        while (accumTime < fadeinTime)
        {
            color.a = 1f - accumTime / fadeinTime;
            image.color = color;
            yield return null;
            accumTime += Time.deltaTime;
            // Debug.Log("�ð�����" + accumTime);
        }

        fadeimage.SetActive(false);
        ProjectManager.instance.isentrance = false;
        yield break;

    }

    private IEnumerator FadeOut() //fadeout�� ������ �Ⱥ��̰�
    {
        Debug.Log("���̵� ������");
        fadeimage.SetActive(true);
        Color color = image.color;

        accumTime = 0f;
        color.a = 1f;
        image.color = color;
        //while(accumTime < fadeoutTime)
        //{
        //    color.a =  1f - fadeoutTime / accumTime;
        //    image.color = color;
        //    //yield return null;
        //    accumTime += Time.deltaTime;
        //}

        yield return new WaitForSeconds(0.4f);
        StartCoroutine(FadeIn()); //�����ð� ������ �������� Fadein �ڷ�ƾ ȣ��
    }
}
