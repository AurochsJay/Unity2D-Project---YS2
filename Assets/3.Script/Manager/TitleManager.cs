using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("TitleMenu")]
    [SerializeField] private GameObject titlelayout;
    [SerializeField] private GameObject selecttitlemenu;
    [SerializeField] private GameObject credit;

    int[] textsize = new int[4];
    int column = 0;
            

    void Start()
    {
        for (int i = 0; i < textsize.Length; i++)
        {
            textsize[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
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
        selecttitlemenu.transform.position = titlelayout.transform.GetChild(textsize[column]).transform.position;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X))
        {
            switch (textsize[column])
            {
                case 0: // 새게임
                    ProjectManager.instance.istitle = true;
                    SceneManager.LoadScene("MainGame");
                    break;
                case 1: // 불러오기
                    ProjectManager.instance.istitleload = true;
                    SceneManager.LoadScene("MainGame");
                    break;
                case 2: // 크레딧
                    credit.SetActive(true);
                    StartCoroutine(Credit_co());
                    break;
                case 3: // 게임종료
                    Application.Quit();
                    break;

            }

        }

    }

    private IEnumerator Credit_co()
    {
        yield return new WaitForSeconds(3f);
        credit.SetActive(false);
        yield break;
    }
}
