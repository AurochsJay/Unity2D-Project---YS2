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



    //FadeIn FadeOut 처리변수
    private Image image;
    private float fadeinTime = 1f; //페이드 타임
    private float fadeoutTime = 0.1f;
    private float accumTime = 0f; // 시간축적
    private Coroutine fade_co;

    //코루틴 처리
    Coroutine equipcoroutine;
    Coroutine invencoroutine;
    public Coroutine getcoroutine;
    public Coroutine shopcoroutine;
    public Coroutine checkshopcoroutine;
    Coroutine gamemenucoroutine;

    //상점 코루틴 처리변수
    private bool iscorunning = false;

    private void Start()
    {
        //Fade 변수 초기화
        image = fadeimage.GetComponent<Image>();
    }

    private void Update()
    {
        ShowPlayerSliderUI();
        ShowPlayerTextUI();
        ShowMonsterUI();
    }

    //플레이어 HP,MP Slider 표시
    private void ShowPlayerSliderUI()
    {
        slider_maxHP.value = ProjectManager.instance.gamemanager.maxHP;
        slider_HP.value = ProjectManager.instance.gamemanager.currentHP;
        slider_maxMP.value = ProjectManager.instance.gamemanager.maxMP;
        slider_MP.value = ProjectManager.instance.gamemanager.currentMP;
    }

    //플레이어 HP,MP,EXP,Gold 표시
    private void ShowPlayerTextUI()
    {
        HPtext.text = ProjectManager.instance.gamemanager.currentHP.ToString();
        MPtext.text = ProjectManager.instance.gamemanager.currentMP.ToString();
        EXPtext.text = (ProjectManager.instance.gamemanager.maxEXP - ProjectManager.instance.gamemanager.currentEXP).ToString();
        Goldtext.text = ProjectManager.instance.gamemanager.Gold.ToString();
    }


    //몬스터 체력 UI 표시 및 업데이트
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
        else //체력UI 비활성화
        {
            slider_monstermaxHP.gameObject.SetActive(false);
            slider_monsterHP.gameObject.SetActive(false);
        }
    }

    //장비창 표시
    public void ShowEquipmentUI()
    {
        Debug.Log("여긴 들어왔겠지");
        if (!equipmentUI.activeSelf)
        {
            //다른 표시창들 끄기
            playerinfoUI.SetActive(false);
            inventoryUI.SetActive(false);

            equipmentUI.SetActive(true);
            ProjectManager.instance.useoptionmenu = true;
            Debug.Log("여기도 들어왔겠지");
            equipcoroutine = StartCoroutine(SelectEquip_co());
        }
        else
        {
            equipmentUI.SetActive(false);
            ProjectManager.instance.useoptionmenu = false;
            StopCoroutine(equipcoroutine);
        }
    }

    //아이템창 표시
    public void ShowInventoryUI()
    {
        if (!inventoryUI.activeSelf)
        {
            //다른 표시창들 끄기
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

    //플레이어정보창 표시
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
        //fieldname.text = "란스마을";


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

    //게임메뉴창 표시
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

    //세이브창 표시
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

    //로드창 표시
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

    //메뉴창 닫기, esc키를 누르면 발생한다.
    public void CloseMenuUI()
    {
        equipmentUI.SetActive(false);
        inventoryUI.SetActive(false);
        gamemenuUI.SetActive(false);
        ProjectManager.instance.useoptionmenu = false;
    }


    #region 장비창UI
    //장비창을 열었을때 selectitem이 움직여야겠지. selectitem은 [specializefield]로 참조한다.
    public IEnumerator SelectEquip_co()
    {
        int[,] slotsize = new int[5,6];
        int row = 0; // 행으로 움직일때
        int column = 0; // 열로 움직일때
        for(int i = 0; i < 5; i ++)
        {
            for(int j = 0; j < 6; j++)
            {
                slotsize[i, j] = i*6+j;
                //가방의 배열을 만들때 장비를 가지고 있으면 이미지 출력, 없으면 이미지 출력 X
                CheckHaveEquipmentForSlotUI(slotsize[i, j]);
            }
        }
        
        //selectequip.transform.position = equipmentUI.transform.GetChild(0).position;
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //입력받는거야 뭐 하고, 문제는 어떻게 포지션을 옮길것이냐는거, 이중배열로 만들어서 움직여
                //이중배열 크기 가로 6 x 세로 5 사이즈로 행 row, 열 column
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
            //slotsize 이중배열의 값에 따라 switch-case로 넣기
            ShowEquipSlotInfo(slotsize[column, row]);

            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                //장비 착용, 장비 해제 메서드를 호출하긴 할껀데, GameManager에서 관리하는게 맞다.
                //플레이어의 공격력과 방어력에 관한거니까.
                //아니다 장비를 장착하는거니까 아이템매니저에서 usethisitem bool변수를 만들고
                //그 장비의 정보를 Gamemanager로 넘겨야지
                ProjectManager.instance.itemmanager.UseEquipment(slotsize[column,row]);
            }

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
            {
                //창 끄기
                equipmentUI.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            }
            Debug.Log("코루틴이 계속 돌아가는지 확인. Stop코루틴이 먹히는지");
            yield return null;
        }
        //Debug.Log("아마 코루틴이 계속 호출되겠지");
        yield return new WaitForSeconds(0.3f);
    }

    //장비창 EquipmentUI의 자식들(슬롯)에게 정보를 넘겨줘 // 자식한테 정보를 받아들일 공간을 만들고 거기에 할당한다.?
    //시작할때 넣어주면 되겠지.
    //public void SlotInfo(int childnumber)
    //{
    //    //ItemManager[] itemmangers = new ItemManager[equipmentUI.transform.childCount];
    //    //for(int i = 0; i < equipmentUI.transform.childCount; i++)
    //    //{
    //    //    itemmangers[i].swords[0] = ProjectManager.instance.itemmanager.swords[i];
    //    //}
    //    //childnumber가 0~5:검, 6~11:방패, 12~17:아머, 18~23:매직, 24~29:악세사리
    //    equipmentUI.transform.GetChild(0).GetComponent<ItemManager>().swords[0] = ProjectManager.instance.itemmanager.swords[0];
    //    equipmentUI.transform.GetChild(1).GetComponent<ItemManager>().swords[1] = ProjectManager.instance.itemmanager.swords[1];
    //    equipmentUI.transform.GetChild(2).GetComponent<ItemManager>().swords[2] = ProjectManager.instance.itemmanager.swords[2];
    //    equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[2] = ProjectManager.instance.itemmanager.swords[2];
    //    equipmentUI.transform.GetChild(childnumber).gameObject.AddComponent<ItemManager>();
    //    equipmentUI.transform.GetChild(childnumber).gameObject.AddComponent<ItemManager>();
    //    equipmentUI.transform.GetChild(0).gameObject.AddComponent<ItemManager>();
    //}

    //slot에 장비정보를 담았으니 이제 출력해. 언제? selectitem이 선택할때
    public void ShowEquipSlotInfo(int childnumber)
    {
        //Debug.Log("들어온 숫자" + childnumber);
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
                Debug.Log("아직 없는 아이템이기때문에 출력하지 않습니다.");
                break;
        }
        equipstr.text = ProjectManager.instance.gamemanager.ATK.ToString();
        equipdef.text = ProjectManager.instance.gamemanager.DEF.ToString();

        //equipimage = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].image;
        //equipname.text = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].name;
        //equipinfo.text = equipmentUI.transform.GetChild(childnumber).GetComponent<ItemManager>().swords[0].info;
    }

    //장비를 가지고 있으면 장비창에서 정보출력, 없으면 X
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

    //장비 정보 출력
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

    //아이템을 가지고 있지 않다면 
    private void AllocateEquipmentInfo_false()
    {
        equipimage.sprite = Resources.Load<Sprite>("ys2 Black image");
        equipname.text = "";
        equipinfo.text = "";
        equipmagic.text = "";
        equipinv.text = "";
    }

    //장비 가지고 있는지 체크
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
   

    //장비 아이콘 이미지 보이게, 안보이게
    private void CheckColorEquipment(int index, int childnumber)
    {
        Color color_origin = new Color();
        color_origin.r = 255;
        color_origin.g = 255;
        color_origin.b = 255;
        color_origin.a = 255;
        Color color_false = new Color();
        color_false.a = 0;

        if(childnumber >=0 && childnumber <=2) //무기
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
        else if(childnumber >= 6 && childnumber <= 7) //방패
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
        else if(childnumber >= 12 && childnumber <= 14) //갑옷
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
        else if(childnumber == 18) //매직
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
        else if(childnumber == 24) //악세
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

    #region 아이템창UI
    //아이템창
    public IEnumerator SelectInven_co()
    {
        int[,] slotsize = new int[7, 6];
        int row = 0; // 행으로 움직일때
        int column = 0; // 열로 움직일때
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                slotsize[i, j] = i * 6 + j;
                //가방의 배열을 만들때 아이템을 가지고 있으면 이미지 출력, 없으면 이미지 출력 X
                CheckHaveItemForSlotUI(slotsize[i,j]);
            }
        }

        //selectequip.transform.position = equipmentUI.transform.GetChild(0).position;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //이중배열 크기 가로 6 x 세로 7 사이즈로 행 row, 열 column
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
            //slotsize 이중배열의 값에 따라 switch-case로 넣기
            ShowInvenSlotInfo(slotsize[column, row]);

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                ProjectManager.instance.itemmanager.EquipItem(slotsize[column, row]);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
            {
                //창 끄기
                inventoryUI.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            }

            yield return null;
        }
        //Debug.Log("아마 코루틴이 계속 호출되겠지");
        yield return new WaitForSeconds(0.3f);
    }

    //아이템 가지고있는지 체크, 없으면 인벤토리 아이템 아이콘이미지 제거
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

    //아이템 아이콘 이미지 보이게, 안보이게
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

    //인벤토리 아이템정보 출력
    public void ShowInvenSlotInfo(int childnumber)
    {
        Debug.Log("들어온 숫자" + childnumber);
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
                //아이템이 있으면, 없으면
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
                Debug.Log("아직 없는 아이템이기때문에 출력하지 않습니다.");
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

    //아이템 정보 //출력하고자 하는 정보들을 할당하는 메서드
    private void AllocateInventoryInfo(int index)
    {
        invenimage.sprite = ProjectManager.instance.itemmanager.items[index].image;
        invenname.text = ProjectManager.instance.itemmanager.items[index].name;
        inveninfo.text = ProjectManager.instance.itemmanager.items[index].info;
    }

    //아이템을 가지고 있지 않다면 
    private void AllocateInventoryInfo_false()
    {
        invenimage.sprite = Resources.Load<Sprite>("ys2 Black image");
        invenname.text = "";
        inveninfo.text = "";
    }
    #endregion

    //Tab키 플레이어정보 계속 갱신해줘야함
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
            fieldname.text = "란스마을";

            yield return null;
        }
    }

    //충돌해서 아이템 얻었을때 뜨는 창
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
                //창 끄기
                getsomethingUI.SetActive(false);
                ProjectManager.instance.iscolliderobject = false;
                //필드에 있는 아이템 제거
                Destroy(col.gameObject);
                //StopCoroutine(getcoroutine());
                break;
            }
            yield return null;
        }

        Debug.Log("아이템 얻는 코루틴 계속 실행중인지");

        yield return null;
    }

    //NPC 만나면 상점UI 출력
    public IEnumerator ShowShopUI_co(Collider2D col)
    {
        shopUI.SetActive(true);
        if(col.gameObject.name == "기드")
        {

            npcimage.sprite = Resources.Load<Sprite>("Shop/ys2 기드 image");
            shopname.text = "기드의 무기점";
            shopgold.text = ProjectManager.instance.gamemanager.Gold.ToString() + " Gold";
            shoplayout.transform.GetChild(0).gameObject.SetActive(true);
            shoplayout.transform.GetChild(0).GetComponent<Text>().text = "무기를 산다";
            shoplayout.transform.GetChild(1).gameObject.SetActive(true);
            shoplayout.transform.GetChild(1).GetComponent<Text>().text = "방패를 산다";
            shoplayout.transform.GetChild(2).gameObject.SetActive(true);
            shoplayout.transform.GetChild(2).GetComponent<Text>().text = "갑옷을 산다";
            shoplayout.transform.GetChild(3).gameObject.SetActive(true);
            shoplayout.transform.GetChild(3).GetComponent<Text>().text = "떠난다";

            //4개의 배열 만들기
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

                //선택창 포지션 변경
                selecttext.transform.position = shoplayout.transform.GetChild(textsize[column]).transform.position;
                
                if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X)) && !iscorunning)
                {
                    iscorunning = true;
                    yield return checkshopcoroutine = StartCoroutine(CheckText_EquipmentShop(textsize[column], col));
                    yield return new WaitForSeconds(5f);
                    yield return shopcoroutine = StartCoroutine(ShowShopUI_co(col));
                    iscorunning = false;
                    //shoplayout.transform.GetChild(0).GetComponent<Text>().text = "무기를 산다";
                    //shoplayout.transform.GetChild(1).GetComponent<Text>().text = "방패를 산다";
                    //shoplayout.transform.GetChild(2).GetComponent<Text>().text = "갑옷을 산다";
                    //shoplayout.transform.GetChild(3).GetComponent<Text>().text = "떠난다";
                    //StopCoroutine(ShowShopUI_co(col));
                }

                yield return null;
            }
        }
        else if(col.gameObject.name == "제이드")
        {
            
        }
        else if(col.gameObject.name == "사노아")
        {

        }
        else if(col.gameObject.name == "바노아")
        {

        }

        yield return null;
    }

    private IEnumerator CheckText_EquipmentShop(int index, Collider2D col)
    {
        switch (index)
        {
            case 0: // 무기선택
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Short Sword를 산다.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Long Sword를 산다.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "Talwar를 산다.";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "뒤로가기";

                //4개의 배열 만들기
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

                    //선택창 포지션 변경
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize[column]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //무기 구매
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
            case 1: // 방패선택
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Wooden Shield를 산다.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Small Shield를 산다.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "뒤로가기";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "";

                //4개의 배열 만들기
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

                    //선택창 포지션 변경
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize1[column1]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //무기 구매
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
            case 2: // 갑옷구매
                shoplayout.transform.GetChild(0).GetComponent<Text>().text = "Chain Mail을 산다.";
                shoplayout.transform.GetChild(1).GetComponent<Text>().text = "Breast Plate를 산다.";
                shoplayout.transform.GetChild(2).GetComponent<Text>().text = "Plate Mail을 산다.";
                shoplayout.transform.GetChild(3).GetComponent<Text>().text = "뒤로가기";

                //4개의 배열 만들기
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

                    //선택창 포지션 변경
                    selecttext.transform.position = shoplayout.transform.GetChild(textsize2[column2]).position;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //무기 구매
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
            case 3: // 돌아가기
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

    //무기 구매
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
                    Debug.Log("돈이 없다");
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
                    Debug.Log("돈이 없다");
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
                    Debug.Log("돈이 없다");
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

    //방패 구매
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

    //갑옷 구매
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

    //게임메뉴 선택
    public IEnumerator SelectMenu_co()
    {
        //4개의 배열 만들기
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

            //선택창 포지션 변경
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
            case 0: // 저장하기
                ProjectManager.instance.datamanager.SaveGameDataToJson();
                gamemenuUI.gameObject.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            case 1: // 불러오기
                ProjectManager.instance.datamanager.LoadGameDataFromJson();
                ProjectManager.instance.datamanager.DataFileToGameManager();
                gamemenuUI.gameObject.SetActive(false);
                ProjectManager.instance.useoptionmenu = false;
                break;
            case 2: // 장비창
                ProjectManager.instance.UImanager.ShowEquipmentUI();
                gamemenuUI.gameObject.SetActive(false);
                break;
            case 3: // 아이템창
                ProjectManager.instance.UImanager.ShowInventoryUI();
                gamemenuUI.gameObject.SetActive(false);
                break;
            case 4: // 타이틀
                SceneManager.LoadScene("Title");
                break;
            case 5: // 돌아가기
                ProjectManager.instance.useoptionmenu = false;
                gamemenuUI.gameObject.SetActive(false);
                StopCoroutine(gamemenucoroutine);
                break;
            default:
                break;
        }

    }



    //Fadein Fadeout 메서드
    public void StartFadeIn() // 호출 함수 Fade In을 시작
    {
        if (fade_co != null)
        {
            StopCoroutine(FadeIn());
            StopCoroutine(FadeOut());
            fade_co = null;
            Debug.Log("fade_co가 != null이 true라서 일로 왔다");
        }

        fade_co = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn() // 코루틴을 통해 페이드 인 시간조절, fade in은 영상이 보이게
    {
        Debug.Log("페이드 들어왔나");

        Color color = image.color;

        accumTime = 0f;
        color.a = 1f;
        while (accumTime < fadeinTime)
        {
            color.a = 1f - accumTime / fadeinTime;
            image.color = color;
            yield return null;
            accumTime += Time.deltaTime;
            // Debug.Log("시간축적" + accumTime);
        }

        fadeimage.SetActive(false);
        ProjectManager.instance.isentrance = false;
        yield break;

    }

    private IEnumerator FadeOut() //fadeout은 영상이 안보이게
    {
        Debug.Log("페이드 나갔나");
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
        StartCoroutine(FadeIn()); //일정시간 켜졌다 꺼지도록 Fadein 코루틴 호출
    }
}
