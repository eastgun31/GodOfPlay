using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value;   //점령지 넘버링 ABCDE 차례대로 01234
    public int pointcheck; //중립상태 0 ,적점령시 1, 플레이어가 점령시 2
    public float time;

    public GameObject playerhome;
    public List<E_unitMove> e_unit = new List<E_unitMove>();
    public List<UnitController> p_unit = new List<UnitController>();
    public GameObject[] points;

    public float p_distance; //범위내 플레이어유닛
    public float e_distance; //범위내 적유닛

    int random;

    private int ppoint = 0;

    string enemy = "Enemy";
    string player = "Player";

    public GameObject GobjWhite;
    public GameObject GobjBlue;
    public GameObject GobjRed;

    

    // Start is called before the first frame update
    void Start()
    {
        p_distance = 100f;
        e_distance = 100f;

        SetGameObjectActive(GobjRed, false);
        SetGameObjectActive(GobjWhite, true);
        SetGameObjectActive(GobjBlue, false);

        StartCoroutine(PointCheckCoroutine());
    }

    // Update is called once per frame
    private IEnumerator PointCheckCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            if (pointcheck != ppoint)
            {
                if (pointcheck == 1)
                {
                    SetGameObjectActive(GobjRed, true);
                    SetGameObjectActive(GobjWhite, false);
                    SetGameObjectActive(GobjBlue, false);
                }
                else if (pointcheck == 2) //pointcheck가 0이었다가 2가 되면 재화 획득
                {
                    if (ppoint == 0)
                    {
                        GameManager.instance.gold += 200;
                    }
                    else if (ppoint == 1)
                    {
                        GameManager.instance.gold += 100;
                    }
                    else if (ppoint == 3)
                    {
                        GameManager.instance.gold += 100;
                    }

                    SetGameObjectActive(GobjRed, false);
                    SetGameObjectActive(GobjWhite, false);
                    SetGameObjectActive(GobjBlue, true);
                }
            }

            ppoint = pointcheck;
            yield return wait; // 다음 프레임까지 대기 -> 1초로 바꿈
        }
    }

    void SetGameObjectActive(GameObject gameObject, bool isActive)
    {
        {
            if (gameObject != null)
                gameObject.SetActive(isActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemy))
        {
            e_unit.Add(other.gameObject.GetComponent<E_unitMove>());

            StartCoroutine("eUnitNewPoint", other.gameObject);
            random = Random.Range(0, 5);
        }

        if (other.CompareTag(player))
        {
            p_unit.Add(other.gameObject.GetComponent<UnitController>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(enemy)) //적군 점령지에 있는지 체크
        {
            e_distance = Vector3.Distance(other.transform.position, transform.position);

            for (int i = 0; i < e_unit.Count; i++)
            {
                if (e_unit[i].ehealth <= 0)
                {
                    e_unit.Remove(e_unit[i]);
                }
            }

        }
        if (other.CompareTag(player)) //아군 점령지에 있는지 체크
        {
            p_distance = Vector3.Distance(other.transform.position, transform.position);

            for (int i = 0; i < p_unit.Count; i++)
            {
                if (p_unit[i].uhealth <= 0)
                {
                    p_unit.Remove(p_unit[i]);
                }
            }
        }

        if (e_distance <= 10f && p_distance > 10f) //적 점령시
        {
            time += Time.deltaTime;

            if (time > 15f)
            {
                pointcheck = 1;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }

                time = 0;
            }
        }

        if (p_distance <= 10f && e_distance > 10f)
        {
            time += Time.deltaTime;

            if (time > 15f) //플레이어가 점령시
            {
                time = 0;
                pointcheck = 2;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        GameManager.instance.attacking = false;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        GameManager.instance.attacking = false;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        GameManager.instance.attacking = false;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        GameManager.instance.attacking = false;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        GameManager.instance.attacking = false;
                        break;
                }
            }
        }

        if (pointcheck == 1 && p_distance <= 10f) //플레이어가 적 거점 뺏었을시
        {
            GameManager.instance.attacking = true;

            switch (value)
            {
                case 0:
                    GameManager.instance.attackPoint = 0;
                    break;
                case 1:
                    GameManager.instance.attackPoint = 1;
                    break;
                case 2:
                    GameManager.instance.attackPoint = 2;
                    break;
                case 3:
                    GameManager.instance.attackPoint = 3;
                    break;
                case 4:
                    GameManager.instance.attackPoint = 4;
                    break;
            }
        }


        if (e_distance <= 10f && p_distance <= 10f) //거점에서 전투시
        {
            time += Time.deltaTime;

            if (time > 10f)
            {
                pointcheck = 3;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }

                time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemy))
        {
            time = 0;
            e_distance = 100f;
        }

        if (other.CompareTag(player))
        {
            time = 0;
            GameManager.instance.attacking = false;
            p_distance = 100f;
        }
    }

    IEnumerator eUnitNewPoint(GameObject other) //적군 새로운 목표지정
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (pointcheck == 1)
        {
            if (GameManager.instance.attacking == true)
            {
                for (int i = 0; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(points[GameManager.instance.attackPoint].transform.position);
                    e_unit.Remove(e_unit[i]);
                }
            }
            else if (GameManager.instance.check[random] == 0 || GameManager.instance.check[random] == 2)
            {
                for (int i = 3; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(points[random].transform.position);
                    e_unit.Remove(e_unit[i]);
                }
            }
            else if (GameManager.instance.check[0] == 1 && GameManager.instance.check[1] == 1 && GameManager.instance.check[2] == 1 && GameManager.instance.check[3] == 1 && GameManager.instance.check[4] == 1)
            {
                for (int i = 0; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(playerhome.transform.position);
                    e_unit.Remove(e_unit[i]);
                }
            }
        }

        yield return wait;

        StartCoroutine("eUnitNewPoint", other.gameObject);
    }
}
