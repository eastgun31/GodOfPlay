using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniMap_Controller : MonoBehaviour
{

    public float camSpeed = 50f;

    float minX = 18;
    float maxX = 212;
    float minZ = 18;
    float maxZ = 212;


    void Update()
    {

        FastCam();
        Vector3 pos = transform.position;


        //À§
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += camSpeed * Time.deltaTime;
        }
        //¾Æ·¡
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        //¿Þ
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= camSpeed * Time.deltaTime;
        }
        //¿À
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    void FastCam()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            camSpeed = 100;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            camSpeed = 50;
        }
    }
}
