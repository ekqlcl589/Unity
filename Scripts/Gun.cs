using System.Collections;
using UnityEngine;

// 총을 구현
public class Gun : MonoBehaviour
{
    // 총의 상태를 표현하는 데 사용할 타입을 선언
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄알집이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과

    private ParticleSystem muzzleEffect;

    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private ParticleSystem shellEffect;

    public GunData gunData; // 총의 현재 데이터

    private GunData data;

    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기

    private const float fireDistance = 50f; // 사정거리

    private int ammoRemain; // 남은 전체 탄알

    public int AmmoRemain { get { return ammoRemain; } private set { } }


    private int magAmmo; // 현재 탄알집에 남아 있는 탄알

    public int MagAmmo { get { return magAmmo; } private set { } }

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private const int lineRendererPosition = 2;

    private const float effectWaitTime = 0.03f;

    private const int init = 0;

    private void Awake()
    {
        // 사용할 컴포넌트의 참조 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponentInChildren<LineRenderer>();

        bulletLineRenderer.positionCount = lineRendererPosition;

        bulletLineRenderer.enabled = false;


        data = gunData;

        ammoRemain = data.startAmmoRemain;

        magAmmo = data.magCapacity;

        data.gun = GunData.gunType.gunNormal;

        shellEffect = shellEjectEffect;

        muzzleEffect = muzzleFlashEffect;
    }

    private void OnEnable()
    {
        // 총 상태 초기화

        state = State.Ready;

        lastFireTime = init;
    }

    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + data.timeBetFire) // 마지막 총 발사 시점에서 건데이터.타임벳파이어 이상의 시간이 지났을 때 == 현재 시간이 총을 최근에 발사한 시점 + 발사 간격 이후 인지 
        {
            // 마지막 총 발사 시점 갱신
            lastFireTime = Time.time;

            Shot();
        }
    }

    // 실제 발사 처리
    private void Shot()
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        // 레이캐스트(시작 지점, 방향, 충돌 정보 컨테이너, 사정거리
        if (Physics.Raycast(transform.position, transform.forward, out hit, fireDistance))
        {
            // 레이가 어떤 물체와 충돌한 경우

            // 충돌한 상대방으로 부터 IDamageable 오브젝트 가져오기 시도
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                // 상대방의 OnDamage 함수를 실행시켜 상대방에게 데미지 처리
                target.OnDamage(data.damage, hit.point, hit.normal);
            }

            // 레이가 충돌한 위치 저장
            hitPosition = hit.point;

        }
        else
        {
            // 레이가 충돌하지 않았으면, 탄알이 최대사정거리까지 날아갔을 떄의 위치를 충돌 위치로 사용
            hitPosition = transform.position + transform.forward * fireDistance;
        }

        // 발사 이펙트 재생
        StartCoroutine(ShotEffect(hitPosition));

        magAmmo--;

        if (magAmmo <= init)
        {
            state = State.Empty;
        }
    }

    // 발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 총구 화염 효과 재생
        muzzleEffect.Play();
        // 탄피 배출 효과 재생
        shellEffect.Play();
        //총격 소리 재생
        gunAudioPlayer.PlayOneShot(data.shotClip);
        //선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(init, transform.position);
        //선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);

        // 라인 렌더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(effectWaitTime);

        // 라인 렌더러를 비활성화하여 탄알 궤적을 지움
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload()
    {

        if (state == State.Reloading || ammoRemain <= init || magAmmo >= data.magCapacity)
        {
            return false;
        }

        // 재장전 처리 시작
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;

        gunAudioPlayer.PlayOneShot(data.reloadClip);

        // 재장전 소요 시간 만큼 처리 쉬기
        yield return new WaitForSeconds(data.reloadTime);

        // 탄창에 채울 탄알 계산
        int ammoToFill = data.magCapacity - magAmmo;

        if (ammoRemain < ammoToFill)
            ammoToFill = ammoRemain;

        magAmmo += ammoToFill;

        ammoRemain -= ammoToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }

    public void SetAddAmmo(int newAmmo)
    {
        ammoRemain = newAmmo;
    }
}