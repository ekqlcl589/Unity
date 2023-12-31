using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour, IItem
{
    public LayerMask whatIsTarget; // 플레이어만 회복 시켜야 함

    private LivingEntity targetEntiry;

    private const float healing = 7f; // x 체온
    private const float timeBetHeal = 1f; // 체력 회복 간격

    private const float navMeshRange = 5f;
    private float lastHealTime; // 마지막 체력 회복 시점

    private const float waitForSecond = 0.25f;
    private bool hasTarget
    {
        get
        {
            if (targetEntiry != null && !targetEntiry.Dead)
                return true;

            return false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdateHealing());
    }

    // Update is called once per frame

    private IEnumerator UpdateHealing() // ㄴㄴ 체온으로 변경
    {
        if (hasTarget)
        {
            // life.RestoreHealth(health);
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, navMeshRange, whatIsTarget);

            for (int i = 0; i < colliders.Length; i++)
            {
                LivingEntity live = colliders[i].GetComponent<LivingEntity>();
                live.RestoreTemperature(healing);
                // 컴포넌트가 존재하고 해당 컴포넌트가 살아 있다면
                if (live != null && !live.Dead)
                {
                    targetEntiry = live;
                    break;
                }
            }
        }

        yield return new WaitForSeconds(waitForSecond);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player") && Time.time >= lastHealTime + timeBetHeal)
        {
            lastHealTime = Time.time;

            Auto(other.gameObject);
        }

    }

    public void Auto(GameObject gameObject)
    {
        LivingEntity life = gameObject.GetComponent<LivingEntity>();

        // LivingEntity컴포넌트가 있다면
        if (life != null)
        {
            if (life.Temperature >= life.MaxTemperature)
                return;
            // 체온 회복 실행
            life.RestoreTemperature(healing);
        }

    }
    public void Use(GameObject gameObject)
    {

    }
    public bool Used(GameObject target)
    {
        return true;
    }

}
