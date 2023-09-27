using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_move : MonoBehaviour
{
    public Collider[] colliders;
   public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("targeting_P", 0.0f,0.2f);
        StartCoroutine(target_P());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void tartgeting_P()
    //{
    //    colliders = Physics.OverlapSphere(transform.position, 5f);

    //    if (colliders.Length > 0)
    //    {
    //        for (int i = 0; i < colliders.Length; i++)
    //        {
    //            if (colliders[i].tag == "Player")
    //            {
    //                target = colliders[i].gameObject.transform;
    //                break;
    //            }
    //            else
    //            {
    //                target = null;
    //            }
    //        }
    //    }
    //}

    IEnumerator target_P()
    {
        colliders = Physics.OverlapSphere(transform.position, 5f);

        if(colliders.Length > 0)
        {
            for(int i = 0; i<colliders.Length; i++)
            {
                if(colliders[i].tag == "Player")
                {
                    target = colliders[i].gameObject.transform;
                    break;
                }
                else
                {
                    target = null;
                }
            }
        }

        yield return new WaitForSeconds(0.1f);
    }

}
