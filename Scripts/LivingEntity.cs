using System;
using UnityEngine;

// 생명체로서 동작할 게임 오브젝트들을 위한 뼈대를 제공
// 체력, 데미지 받아들이기, 사망 기능, 사망 이벤트를 제공
public class LivingEntity : MonoBehaviour, IDamageable {
    protected const float maxHealth = 100f;
    protected const float startingHealth = 100f; // 시작 체력
    protected const float startAnimalHealth = 20f;
    public float health { get; protected set; } // 현재 체력

    protected const float maxHunger = 100f;
    protected const float startingHunger = 100f;
    private int DieHealth = 0;

    protected const float hungryDecreasePoint = 5;
    protected float hungryDecreaseTime;
    protected float currentHungryDecreaseTime;

    public float Hunger { get; protected set; }

    protected const float maxTemperature = 100f;
    protected const float startingTemperature = 100f;

    protected const float temperatureDecreasePoint = 5;
    protected float temperatureDecreaseTime;
    protected float currentTemperatureDecreaseTime;
    public float Temperature { get; protected set; }

    protected const float naveMeshDefaultSpeed = 1f;
    protected const float naveMeshStopSpeed = 0f;
    protected const float naveMeshSlowSpeed = 0.5f;

    private const defaultAnimalNavMeshRange = 10f;

    protected const float waitForSecond = 0.5f;
    public bool dead { get; protected set; } // 사망 상태

    public event Action onDeath; // 사망시 발동할 이벤트

    // 생명체가 활성화될때 상태를 리셋
    protected virtual void OnEnable() {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
        Hunger = startingHunger;
        Temperature = startingTemperature;
    }

    // 데미지를 입는 기능, IDamageable 상속 
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // 충돌 시 데미지, 충돌 위치, 방향
        health -= damage;

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= DieHealth && !dead)
        {
            GameManager.instance.AddZombieCount(1);

            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth) {
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }

        if (health >= maxHealth)
            return;
        else
             health += newHealth;
        // 체력 추가
    }

    public virtual void RestoreHunger(float newHunger)
    {
        if (dead)
            return;

        if (Hunger >= maxHunger)
            return;
        else
            Hunger += newHunger;
    }

    public virtual void Diminish(float newHunger)
    {
        if (dead)
            return;

        if (Hunger <= DieHealth)
            return;
        else
            Hunger -= newHunger;
    }

    public virtual void RestoreTemperature(float newTemper)
    {
        if (dead)
            return;

        if (Hunger >= maxHunger)
            return;
        else
            Temperature += newTemper;
    }

    public virtual void DownTemperature(float newTemper)
    {
        if (dead)
            return;

        if (Hunger <= DieHealth)
            return;
        Temperature -= newTemper;

    }
    // 사망 처리
    public virtual void Die() {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망 상태를 참으로 변경
        dead = true;
    }
}