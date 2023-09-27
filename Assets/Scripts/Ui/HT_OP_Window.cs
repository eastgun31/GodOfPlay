using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HT_OP_Window : MonoBehaviour
{
    public GameObject HTP_ON;
    public GameObject OP_ON;
    public GameObject access_restrictions;
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
}
