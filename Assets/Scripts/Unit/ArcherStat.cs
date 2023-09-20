using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArcherStat : MonoBehaviour
{
    bool isDie = false;

    public float ArcherHp = 100;
    public float power = 10;

    float originalDamage; // 능력치 변경 이전의 데미지
    float buffDuration = 5f; // 스킬 지속 시간
    bool isBuffActive = false; // 스킬 활성화 여부
    float buffEndTime; // 스킬 종료 시간

    //부활 전 대기시간
    float resurrectionTime = 5f;
    float resurrectionTimer = 0f;

    void Start()
    {
        isDie = false;
        originalDamage = power;
    }

    // Update is called once per frame
    void Update()
    {
        if (ArcherHp <= 0)
        {
            isDie = true; // 죽음 표시
            resurrectionTimer += Time.deltaTime;

            if (resurrectionTimer >= resurrectionTime)
            {
                // 최대 대기 시간 초과 시 유닛 오브젝트 삭제
                Destroy(gameObject);
            }
        }
        else
        {
            resurrectionTimer = 0f; // 생존할 경우 타이머 초기화
        }

        if (isBuffActive && Time.time >= buffEndTime)
        {
            EndBuffSkill();
        }

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "PowerBuff")
    //    {
    //        UseBuffSkill();
    //        Debug.Log("접촉");
    //    }
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.tag == "PowerBuff")
    //    {
    //        UseBuffSkill();
    //        Debug.Log("접촉");
    //    }
    //}

    void UseBuffSkill()
    {
        if (!isBuffActive)
        {
            // 스킬을 활성화하고 능력치를 증가
            isBuffActive = true;
            buffEndTime = Time.time + buffDuration;

            

            float buffDamage = originalDamage * 2;

            UpdateUnitDamage(buffDamage);

            Debug.Log("스텟 상승");
        }
    }

    void EndBuffSkill()
    {
        if (isBuffActive)
        {
            // 스킬을 비활성화하고 원래 능력치로 되돌림
            isBuffActive = false;

            // 실제 유닛의 데미지를 원래대로 돌려놓습니다.
            UpdateUnitDamage(originalDamage);

            Debug.Log("스텟 복구");
        }
    }

    void UpdateUnitDamage(float newDamage)
    {
        power = newDamage;
        Debug.Log("스텟 변동");
    }

    void Die()
    {
        this.gameObject.layer = 7;  //DieUnit변경
    }

    void Resurrection()
    {
        this.gameObject.layer = 6;
        isDie = false;
        resurrectionTimer = 0f;

        ArcherHp = 50;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
