using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loby_Page : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Page()
    {
        ON.SetActive(true);
        OFF.SetActive(false);
    }
}
