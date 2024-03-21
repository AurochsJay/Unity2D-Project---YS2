using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance = null;

    //����
    [SerializeField] public GameManager gamemanager;
    //[SerializeField] GameObject player;
    [SerializeField] PlayerController playercontroller;
    [SerializeField] MonsterController monstercontroller;
    [SerializeField] public UIManager UImanager;
    [SerializeField] public ItemManager itemmanager;
    [SerializeField] public EventManager eventmanager;
    [SerializeField] public DataManager datamanager;
    [SerializeField] public CameraController cameracontroller;


    //������ �����ϴ� ���� ĳ���Ͱ� �������� �ʰ� �ϱ� ���� bool�� ����.
    public bool isdialogue = false;
    public bool isentrance = false;
    public bool iscolliderobject = false;
    public bool useoptionmenu = false;

    // �������� ������ �������� bool�� ����� -> �������� ���, �ڽ����� ������
    // public bool havesword = true; 

    //�������� �����ϸ� Equip���·� �����.
    public bool isequipsword = false;
    public bool isequipshield = false;
    public bool isequiparmor = false;
    public bool isequipsmagic = false;
    public bool isequipsaccessory = false;

    //Ÿ��Ʋ, ���ӿ���, Ŭ����
    public bool istitle = false;
    public bool istitleload = false;
    public bool isgameover = false;
    public bool isgameoverload = false;
    public bool isclear = false;
    public bool isclearload = false;

    //����,����,������
    public bool isvillage = false;
    public bool isruin = false;
    public bool isbossroom = false;

    //�̱���
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

    //������Ʈ �Ŵ������� Ű�Է��� ��������.
    //escŰ enterŰ(�������°ŕ��� xŰ�� ���) zŰ(�ǵ�����,���), xŰ(Ȯ��,�������,enter�� ���) cŰ(�����ۻ��) vŰ(�κ�â), bŰ(���â) �Է� -> �����Ŵ��� -> ���� �Ŵ����� ��������
    private void Update()
    {
        CheckInputKey();

        //���� ã��
        FindGameObject();

        //Debug.Log("�����͸Ŵ����� ����ִ� �� ü��: " + datamanager.gamedata.playerdata.maxHP);
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
            //�޴�â ����
            if (!useoptionmenu)
            {
                UImanager.ShowGameMenuUI();
            }
            else
            {
                //esc�� ������ �ٸ� �޴�â �ݾƾ��� ���⼭ ���� UImanager����
                UImanager.CloseMenuUI();
            }

            
        }
            
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //��ȭâ Ȯ��
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !useoptionmenu)
        {
            //ȭ�鿡 ĳ���� ����, ������ ���
            UImanager.ShowPlayerInfoUI();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            //�ǵ�����, ����ϱ�
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            //���� ��ȭâ, �������
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            //������â ����
            UImanager.ShowInventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //���â ����
            UImanager.ShowEquipmentUI();
        }
    }

}
