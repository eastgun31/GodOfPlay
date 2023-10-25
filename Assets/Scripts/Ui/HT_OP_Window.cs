using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HT_OP_Window : MonoBehaviour
{
    public GameObject HTP_ON;
    public GameObject OP_ON;
    public GameObject access_restrictions;
    public int A;

    public GameObject[] how;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if ((HTP_ON.activeSelf == true) || (OP_ON.activeSelf == true))
        {
            access_restrictions.SetActive(true);
        }
        else
        {
            access_restrictions.SetActive(false);
        }
    }

    public void Button()
    {
        A++;

        if (A == 1)
        {
            how[0].SetActive(false);
            how[1].SetActive(true);
            how[2].SetActive(false);
            how[3].SetActive(false);
            how[4].SetActive(false);
            how[5].SetActive(false);
            how[6].SetActive(false);
        }
        else if (A == 2)
        {
            how[0].SetActive(false);
            how[1].SetActive(false);
            how[2].SetActive(true);
            how[3].SetActive(false);
            how[4].SetActive(false);
            how[5].SetActive(false);
            how[6].SetActive(false);
        }
        else if (A == 3)
        {
            how[0].SetActive(false);
            how[1].SetActive(false);
            how[2].SetActive(false);
            how[3].SetActive(true);
            how[4].SetActive(false);
            how[5].SetActive(false);
            how[6].SetActive(false);
        }
        else if (A == 4)
        {
            how[0].SetActive(false);
            how[1].SetActive(false);
            how[2].SetActive(false);
            how[3].SetActive(false);
            how[4].SetActive(true);
            how[5].SetActive(false);
            how[6].SetActive(false);
        }
        else if (A == 5)
        {
            how[0].SetActive(false);
            how[1].SetActive(false);
            how[2].SetActive(false);
            how[3].SetActive(false);
            how[4].SetActive(false);
            how[5].SetActive(true);
            how[6].SetActive(false);
        }
        else if (A <= 6)
        {
            how[0].SetActive(false);
            how[1].SetActive(false);
            how[2].SetActive(false);
            how[3].SetActive(false);
            how[4].SetActive(false);
            how[5].SetActive(false);
            how[6].SetActive(true);
        }

    }

    public void Off()
    {
        HTP_ON.SetActive(false);
        A = 0;
        how[0].SetActive(true);
        how[1].SetActive(false);
        how[2].SetActive(false);
        how[3].SetActive(false);
        how[4].SetActive(false);
        how[5].SetActive(false);
        how[6].SetActive(false);
    }
}

