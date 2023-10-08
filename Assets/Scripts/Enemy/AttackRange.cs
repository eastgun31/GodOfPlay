using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Vector3 target;
    public List<GameObject> targets = new List<GameObject>();

    //Transform target;
    public UnitController p_unit;
    public E_unitMove parent;

    string player = "Player";
    
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<E_unitMove>();

        StartCoroutine("Find_Target");
    }

    // Update is called once per frame
    void Update()
    {
        //if (targets != null)
        //{
        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        target = targets[i].transform.position;
        //        p_unit = targets[i].GetComponent<UnitController>();
        //        if (p_unit.uhealth > 0 && parent.ehealth > 0)
        //        {
        //            parent.Attakc(target, p_unit);
        //            parent.e_State = E_unitMove.E_UnitState.Battle;
        //        }
        //        if(p_unit.uhealth <= 0)
        //        {
        //            p_unit = null;
        //            targets.Remove(targets[i]);
        //            parent.e_State = E_unitMove.E_UnitState.Idle;
        //        }
        //    }
        //}

        //if (target != null)
        //{
        //    Debug.Log("Å¸°ÙÃßÀû");
        //    Vector3 dir = target.position;
        //    parent.Attakc(dir, p_unit);
        //}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(player))
        {
            targets.Add(col.gameObject);
            //if(parent.targetUnit == null)
            //{
            //    target = null;
            //    p_unit = null;
            //}
        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        target = col.gameObject.transform;
    //        Debug.Log("Box Enemy : Target found");

    //        p_unit = col.gameObject.GetComponent<UnitController>();
    //    }
    //}

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(player))
        {
            targets.Remove(col.gameObject);
            //target = null;
            p_unit = null;
        }
        //Debug.Log("Box Enemy : Target lost");
    }

    IEnumerator Find_Target()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                target = targets[i].transform.position;
                p_unit = targets[i].GetComponent<UnitController>();
                if (p_unit.uhealth > 0 && parent.ehealth > 0)
                {
                    parent.Attakc(target, p_unit);
                    parent.e_State = E_unitMove.E_UnitState.Battle;
                }
                if (p_unit.uhealth <= 0)
                {
                    p_unit = null;
                    targets.Remove(targets[i]);
                    parent.e_State = E_unitMove.E_UnitState.Idle;
                }
            }
        }

        yield return wait;

        StartCoroutine("Find_Target");
    }


}
