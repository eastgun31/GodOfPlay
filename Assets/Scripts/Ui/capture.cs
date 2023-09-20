using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capture : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    public GameObject A_Red;
    public GameObject B_Red;
    public GameObject C_Red;
    public GameObject D_Red;
    public GameObject E_Red;

    public GameObject A_Blue;
    public GameObject B_Blue;
    public GameObject C_Blue;
    public GameObject D_Blue;
    public GameObject E_Blue;

    public GameObject A_3;
    public GameObject B_3;
    public GameObject C_3;
    public GameObject D_3;
    public GameObject E_3;

    public int[] check = { 0, 0, 0, 0, 0 };   //거점, 0 중립, 1 적이점령, 2플레이어, 3거점에서 전투

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check = FindAnyObjectByType<GameManager>().check;

        #region A
        if (check[0] == 0)
        {
            A.SetActive(true);
            A_Red.SetActive(false);
            A_Blue.SetActive(false);
            A_3.SetActive(false);
        }
        else if (check[0] == 1)
        {
            A.SetActive(false);
            A_Red.SetActive(true);
            A_Blue.SetActive(false);
            A_3.SetActive(false);
        }
        else if (check[0] == 2)
        {
            A.SetActive(false);
            A_Red.SetActive(false);
            A_Blue.SetActive(true);
            A_3.SetActive(false);
        }
        else if (check[0] == 3)
        {
            A.SetActive(false);
            A_Red.SetActive(false);
            A_Blue.SetActive(false);
            A_3.SetActive(true);
        }
        #endregion

        #region B
        if (check[1] == 0)
        {
            B.SetActive(true);
            B_Red.SetActive(false);
            B_Blue.SetActive(false);
            B_3.SetActive(false);
        }
        else if (check[1] == 1)
        {
            B.SetActive(false);
            B_Red.SetActive(true);
            B_Blue.SetActive(false);
            B_3.SetActive(false);
        }
        else if (check[1] == 2)
        {
            B.SetActive(false);
            B_Red.SetActive(false);
            B_Blue.SetActive(true);
            B_3.SetActive(false);
        }
        else if (check[1] == 3)
        {
            B.SetActive(false);
            B_Red.SetActive(false);
            B_Blue.SetActive(false);
            B_3.SetActive(true);
        }
        #endregion

        #region C
        if (check[2] == 0)
        {
            C.SetActive(true);
            C_Red.SetActive(false);
            C_Blue.SetActive(false);
            C_3.SetActive(false);
        }
        else if (check[2] == 1)
        {
            C.SetActive(false);
            C_Red.SetActive(true);
            C_Blue.SetActive(false);
            C_3.SetActive(false);
        }
        else if (check[2] == 2)
        {
            C.SetActive(false);
            C_Red.SetActive(false);
            C_Blue.SetActive(true);
            C_3.SetActive(false);
        }
        else if (check[2] == 3)
        {
            C.SetActive(false);
            C_Red.SetActive(false);
            C_Blue.SetActive(false);
            C_3.SetActive(true);
        }
        #endregion

        #region D
        if (check[3] == 0)
        {
            D.SetActive(true);
            D_Red.SetActive(false);
            D_Blue.SetActive(false);
            D_3.SetActive(false);
        }
        else if (check[3] == 1)
        {
            D.SetActive(false);
            D_Red.SetActive(true);
            D_Blue.SetActive(false);
            D_3.SetActive(false);
        }
        else if (check[3] == 2)
        {
            D.SetActive(false);
            D_Red.SetActive(false);
            D_Blue.SetActive(true);
            D_3.SetActive(false);
        }
        else if (check[3] == 3)
        {
            D.SetActive(false);
            D_Red.SetActive(false);
            D_Blue.SetActive(false);
            D_3.SetActive(true);
        }
        #endregion

        #region E
        if (check[4] == 0)
        {
            E.SetActive(true);
            E_Red.SetActive(false);
            E_Blue.SetActive(false);
            E_3.SetActive(false);
        }
        else if (check[4] == 1)
        {
            E.SetActive(false);
            E_Red.SetActive(true);
            E_Blue.SetActive(false);
            E_3.SetActive(false);
        }
        else if (check[4] == 2)
        {
            E.SetActive(false);
            E_Red.SetActive(false);
            E_Blue.SetActive(true);
            E_3.SetActive(false);
        }
        else if (check[4] == 3)
        {
            E.SetActive(false);
            E_Red.SetActive(false);
            E_Blue.SetActive(false);
            E_3.SetActive(true);
        }
        #endregion

    }
}
