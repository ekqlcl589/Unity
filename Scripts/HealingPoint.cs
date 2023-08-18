using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour, IItem
{
    public LayerMask whatIsTarget; // �÷��̾ ȸ�� ���Ѿ� ��
    
    private LivingEntity targetEntiry;

    public float healing = 7f; // x ü��
    public float timeBetHeal = 1f; // ü�� ȸ�� ����
    private float lastHealTime; // ������ ü�� ȸ�� ����

    private bool hasTarget
    {
        get
        {
            if (targetEntiry != null && !targetEntiry.dead)
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

    private IEnumerator UpdateHealing() // ���� ü������ ����
    {
        if(hasTarget)
        {
           // life.RestoreHealth(health);
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, whatIsTarget);
           
            for (int i = 0; i < colliders.Length; i++)
            {
                LivingEntity live = colliders[i].GetComponent<LivingEntity>();
                live.RestoreTemperature(healing);
                // ������Ʈ�� �����ϰ� �ش� ������Ʈ�� ��� �ִٸ�
                if (live != null && !live.dead)
                {
                    targetEntiry = live;
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.25f);
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

        // LivingEntity������Ʈ�� �ִٸ�
        if (life != null)
        {
            if (life.Temperature >= life.maxTemperature)
                return;
            // ü�� ȸ�� ����
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
