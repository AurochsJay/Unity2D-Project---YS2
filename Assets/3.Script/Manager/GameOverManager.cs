using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("GameOverMenu")]
    [SerializeField] private GameObject gameovermenu;
    [SerializeField] private GameObject gameoverlayout;
    [SerializeField] private GameObject selectgameovermenu;

    int[] textsize = new int[2];
    int column = 0;


    void Start()
    {
        for (int i = 0; i < textsize.Length; i++)
        {
            textsize[i] = i;
        }

        StartCoroutine(GameOverMethod());
    }

    private IEnumerator GameOverMethod()
    {
        yield return new WaitForSeconds(1f);
        gameovermenu.SetActive(true);

        while(true)
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
                if (column < 1)
                {
                    column++;
                }
            }

            //선택창 포지션 변경
            selectgameovermenu.transform.position = gameoverlayout.transform.GetChild(textsize[column]).transform.position;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X))
            {
                switch (textsize[column])
                {
                    case 0: // 불러오기
                        ProjectManager.instance.isgameoverload = true;
                        SceneManager.LoadScene("MainGame");
                        break;
                    case 1: // 타이틀
                        SceneManager.LoadScene("Title");
                        break;


                }

            }

            yield return null;
        }
        
    }
}
