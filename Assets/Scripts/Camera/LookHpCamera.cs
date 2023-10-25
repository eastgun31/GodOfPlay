using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHpCamera : MonoBehaviour
{
    public Camera hpCamera; //hp바 화면 보게 하는 코드
    void Start()
    {
        hpCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }



    // Update is called once per frame

    void Update()
    {
        transform.LookAt(transform.position + hpCamera.transform.rotation * Vector3.back,

         hpCamera.transform.rotation * Vector3.down);
    }
}
