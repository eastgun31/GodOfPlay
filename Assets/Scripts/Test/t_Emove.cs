using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Emove : MonoBehaviour
{
    float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Attack(Vector3 target, t_move targetUnit)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) <= 2f)
        {
            speed = 0;
            if(time > 5f)
            {
                time = 0;
                targetUnit.health -= 10f;
                Debug.Log("АјАн");
            }
        }
        else
        {
            speed = 5f;
        }

    }

}
