using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Transform[] targets;
    private Transform target;
    UnitController p_unit;

    public List<UnitController> p_units = new List<UnitController>();
    
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
    //        p_units.Add(col.GetComponent<UnitController>());
    //        for(int i = 0; i < p_units.Count; i++)
    //        {
    //            targets[i] = col.gameObject.transform;
    //        }
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
        if(col.CompareTag("Player"))
        {
            target = null;
            p_unit = null;
            //if(p_unit == null && target == null)
            //    parent.MovePoint(parent.lastDesti);
        }
        //p_unit = null;
        Debug.Log("Box Enemy : Target lost");
        //parent.MovePoint(parent.lastDesti);
    }

}
