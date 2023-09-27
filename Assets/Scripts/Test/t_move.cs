using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_move : MonoBehaviour
{
    public float health;
    float speed;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        speed = 5f;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        pos.x += hori * Time.deltaTime * speed;
        pos.z += ver * Time.deltaTime * speed;

        transform.position = pos;
    }
}
