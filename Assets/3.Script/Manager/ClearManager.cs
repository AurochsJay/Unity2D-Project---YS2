using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    [Header("clearMenu")]
    [SerializeField] private GameObject clearmenu;
    [SerializeField] private GameObject clearlayout;
    [SerializeField] private GameObject selectclearmenu;

    int[] textsize = new int[2];
    int column = 0;


    void Start()
    {
        for (int i = 0; i < textsize.Length; i++)
        {
            textsize[i] = i;
        }

        StartCoroutine(ClearMethod_co());
    }

    private IEnumerator ClearMethod_co()
    {
        yield return new WaitForSeconds(1f);
        clearmenu.SetActive(true);

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
                if (column < 1)
                {
                    column++;
                }
            }

            //선택창 포지션 변경
            selectclearmenu.transform.position = clearlayout.transform.GetChild(textsize[column]).transform.position;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X))
            {
                switch (textsize[column])
                {
                    case 0: // 불러오기
                        ProjectManager.instance.isclearload = true;
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
