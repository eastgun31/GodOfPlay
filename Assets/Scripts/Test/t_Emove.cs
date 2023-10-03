using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Emove : MonoBehaviour
{
    float speed;
    float time;
    public t_move firsttargets;

    enum E_UnitState
    {
        Battle, Idle, find_Target
    }

    E_UnitState e_unitBattle;

    // Start is called before the first frame update
    void Start()
    {
        e_unitBattle = E_UnitState.Idle;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //switch(e_unitBattle)
        //{
        //    case E_UnitState.Idle:
        //        E_Idle();
        //        break;
        //    case E_UnitState.Battle:
        //        E_Battle();
        //        break;
        //    case E_UnitState.find_Target:
        //        break;
        //}
    }

    public void Attack(Vector3 target, t_move targetUnit)
    {
        firsttargets = targetUnit;
        e_unitBattle = E_UnitState.find_Target;
        E_FindTarget(target, firsttargets);

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x + 1f, transform.position.y, target.z +1f), speed * Time.deltaTime);

        //if(Vector3.Distance(transform.position, target) <= 2f)
        //{
        //    speed = 0;
        //    if(time > 2f)
        //    {
        //        time = 0;
        //        targetUnit.health -= 10f;
        //        Debug.Log("공격");
        //    }
        //}
        //else
        //{
        //    speed = 5f;
        //}

    }

    void E_Idle()
    {

    }

    void E_FindTarget(Vector3 target, t_move targets)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= 3f)
        {
            speed = 0;
            e_unitBattle = E_UnitState.find_Target;
            StartCoroutine(E_Battle(targets));
            
            //if (time > 2f)
            //{
            //    time = 0;
            //    targetUnit.health -= 10f;
            //    Debug.Log("공격");
            //}
        }
        else
        {
            speed = 5f;
        }

    }

    IEnumerator E_Battle(t_move targets)
    {
        if(targets.health > 0)
        {
            targets.health -= 5f;
            Debug.Log("공격");

            yield return new WaitForSeconds(1f);

            StartCoroutine(E_Battle(targets));
        }
        else if(targets.health <= 0)
        {
            firsttargets = null;
            Debug.Log("적 죽음");
        }

    }

}
