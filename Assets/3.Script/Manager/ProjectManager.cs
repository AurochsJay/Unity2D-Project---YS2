using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance = null;

    //참조
    [SerializeField] public GameManager gamemanager;
    //[SerializeField] GameObject player;
    [SerializeField] PlayerController playercontroller;
    [SerializeField] MonsterController monstercontroller;
    [SerializeField] public UIManager UImanager;
    [SerializeField] public ItemManager itemmanager;
    [SerializeField] public EventManager eventmanager;
    [SerializeField] public DataManager datamanager;
    [SerializeField] public CameraController cameracontroller;


    //뭔가를 실행하는 동안 캐릭터가 움직이지 않게 하기 위한 bool값 설정.
    public bool isdialogue = false;
    public bool isentrance = false;
    public bool iscolliderobject = false;
    public bool useoptionmenu = false;

    // 아이템을 얻으면 얻은상태 bool로 만들기 -> 상점에서 사면, 박스에서 받으면
    // public bool havesword = true; 

    //아이템을 장착하면 Equip상태로 만들기.
    public bool isequipsword = false;
    public bool isequipshield = false;
    public bool isequiparmor = false;
    public bool isequipsmagic = false;
    public bool isequipsaccessory = false;

    //타이틀, 게임오버, 클리어
    public bool istitle = false;
    public bool istitleload = false;
    public bool isgameover = false;
    public bool isgameoverload = false;
    public bool isclear = false;
    public bool isclearload = false;

    //마을,폐허,보스방
    public bool isvillage = false;
    public bool isruin = false;
    public bool isbossroom = false;

    //싱글톤
    public static ProjectManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ProjectManager>();
                if(instance == null)
                {
                    GameObject go = new GameObject("ProjectManager");
                    instance = go.AddComponent<ProjectManager>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    //프로젝트 매니저에서 키입력을 관리하자.
    //esc키 enter키(마법쓰는거뺴고 x키와 비슷) z키(되돌리기,취소), x키(확인,마법사용,enter와 비슷) c키(아이템사용) v키(인벤창), b키(장비창) 입력 -> 플젝매니저 -> 각종 매니저에 지시전달
    private void Update()
    {
        CheckInputKey();

        //파일 찾기
        FindGameObject();

        //Debug.Log("데이터매니저에 들어있는 값 체력: " + datamanager.gamedata.playerdata.maxHP);
    }

    public void FindGameObject()
    {
        gamemanager = FindObjectOfType<GameManager>();
        playercontroller = FindObjectOfType<PlayerController>();
        monstercontroller = FindObjectOfType<MonsterController>(); ;
        UImanager = FindObjectOfType<UIManager>();
        itemmanager = FindObjectOfType<ItemManager>();
        eventmanager = FindObjectOfType<EventManager>();
        datamanager = FindObjectOfType<DataManager>();
        cameracontroller = FindObjectOfType<CameraController>();

        
    }

    private void CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //메뉴창 열기
            if (!useoptionmenu)
            {
                UImanager.ShowGameMenuUI();
            }
            else
            {
                //esc를 누르면 다른 메뉴창 닫아야지 여기서 말고 UImanager에서
                UImanager.CloseMenuUI();
            }

            
        }
            
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //대화창 확인
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !useoptionmenu)
        {
            //화면에 캐릭터 정보, 지역명 출력
            UImanager.ShowPlayerInfoUI();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            //되돌리기, 취소하기
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            //다음 대화창, 마법사용
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            //아이템창 열기
            UImanager.ShowInventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //장비창 열기
            UImanager.ShowEquipmentUI();
        }
    }

}
