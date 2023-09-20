using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AttackRange : MonoBehaviour
{
    Transform target;
    E_unitMove e_unit;
    public UnitController parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Debug.Log("Å¸°ÙÃßÀû");
            Vector3 dir = target.position;
            parent.Attack(dir, e_unit);
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            target = col.gameObject.transform;

            e_unit = col.gameObject.GetComponent<E_unitMove>();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        e_unit = null;
        target = null;
        //p_unit = null;
        Debug.Log("Box Enemy : Target lost");
    }

}
