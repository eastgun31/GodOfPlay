using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    Transform target;
    UnitController p_unit;
    public E_unitMove parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<E_unitMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Debug.Log("Å¸°ÙÃßÀû");
            Vector3 dir = target.position;
            parent.Attakc(dir, p_unit);
        }

    }

    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "player")
    //    {
    //        //if(parent.targetUnit == null)
    //        //{
    //        //    target = null;
    //        //    p_unit = null;
    //        //}
    //    }
    //}

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            target = col.gameObject.transform;
            Debug.Log("Box Enemy : Target found");

            p_unit = col.gameObject.GetComponent<UnitController>();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        target = null;
        //p_unit = null;
        Debug.Log("Box Enemy : Target lost");
    }

}
