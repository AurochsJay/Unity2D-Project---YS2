using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private GameManager gamemanager;
    private Rigidbody2D rb;
    private Animator animator;

    public float walkspeed = 8f;
    public float runspeed = 12f;
    public float waittime = 0.5f;
    public float accumtime = 0f;
    public bool isrun = false;
    public bool issinglepressed = false;
    public bool iscollisionwall = false;

    public AudioSource playeraudisource;
    

    private void Start()
    {
        //gamemanager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playeraudisource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float x;
        float y;
        if (ProjectManager.instance.isdialogue || ProjectManager.instance.isentrance || ProjectManager.instance.iscolliderobject || ProjectManager.instance.useoptionmenu)
        {
            x = 0;
            y = 0;
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        //Debug.Log("x값 " + x);
        //Debug.Log("y값 " + y);
        //else if(x != 0 || y != 0 )
        //{
        //    animator.SetBool("isWalk", true);
        //    animator.SetBool("isRun", false);
        //}
        //else
        //{
        //    animator.SetBool("isIdle", true);
        //    animator.SetBool("isWalk", false);
        //    animator.SetBool("isRun", false);
        //}

        Move(x, y);
        Direction(x, y);
    }

    private void Update()
    {
        CheckRun();
        gamemanager.CanMove();
        if (!ProjectManager.instance.isdialogue || !ProjectManager.instance.isentrance || !ProjectManager.instance.iscolliderobject || !ProjectManager.instance.useoptionmenu)
        {
            if(Input.GetKeyDown(KeyCode.X) && ProjectManager.instance.itemmanager.magics[0].usethisitem)
            {
                gamemanager.UseFireMagic();
            }
        }
    }

    private void Move(float x, float y)
    {
        Vector2 position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        float speed = isrun == true ? runspeed : walkspeed;
        if(!iscollisionwall)
        {
            rb.MovePosition(position + new Vector2(x, y) * speed * Time.deltaTime);
        }
        
    }

    private void Direction(float x, float y)
    {
        if(x != 0 || y != 0 )
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalk", true);
            if(isrun == true)
            {
               // Debug.Log("들어오긴 했나?");
                //isrun = true;
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", true);
            }
            animator.SetFloat("MoveX", x);
            animator.SetFloat("MoveY", y);
            animator.SetFloat("LastMoveX", 0);
            animator.SetFloat("LastMoveY", 0);
            animator.SetFloat("LastMoveX", x);
            animator.SetFloat("LastMoveY", y);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            isrun = false;
        }
    }

    private void CheckRun()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            issinglepressed = true;

        }
        accumtime += Time.deltaTime;
        if (issinglepressed == true && accumtime >= waittime)
        {
            issinglepressed = false;
            accumtime = 0f;
        }

        if (issinglepressed && accumtime <= waittime && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isrun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            iscollisionwall = true;
        }
        else
        {
            iscollisionwall = false;
        }

        //몬스터랑 충돌했을때
        if (col.CompareTag("Monster"))
        {
            Debug.Log("몬스터 충돌");
            //넉백메서드
            gamemanager.ishitmonster = true;
            //StartCoroutine(gamemanager.Knockback_co(col));
            gamemanager.combattomonster_co = StartCoroutine(gamemanager.Combat(col));
            gamemanager.combattoplayer_co = StartCoroutine(gamemanager.CombatToPlayer_co(col));
            

            //gamemanager.Knockback(col);
        }
        
        //NPC랑 충돌했을때
        if (col.CompareTag("NPC"))
        {
            ProjectManager.instance.isdialogue = true;
            ProjectManager.instance.UImanager.shopcoroutine = StartCoroutine(ProjectManager.instance.UImanager.ShowShopUI_co(col));
        }

        //문 출입구에 충돌했을때
        if(col.CompareTag("Entrance"))
        {
            ProjectManager.instance.isentrance = true;
            gamemanager.ChangePlayerPosition(col);
        }

        //보물상자에 충돌했을 때
        if(col.CompareTag("ColliderObject"))
        {
            ProjectManager.instance.iscolliderobject = true;
            ProjectManager.instance.eventmanager.GiveSomethingFromCollide(col);
        }

        //Bullet에 맞았을때
        if(col.CompareTag("Bullet"))
        {
            gamemanager.HitBullet();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        iscollisionwall = false;
    }

    
}
