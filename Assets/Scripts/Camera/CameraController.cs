using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSpeed = 50f;
    public float scrollSpeed = 50f;

    public float minY = 15f;
    public float maxY = 35f;

    float minX = 0;
    float maxX = 230;
    float minZ = 0;
    float maxZ = 230;

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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        if (pos.y > minY && pos.y < maxY)
            pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(22, pos.y, 8); ;
        }
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
