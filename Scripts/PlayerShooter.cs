﻿using UnityEngine;
using UnityEngine.EventSystems;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour
{

    public Gun GetGunData() { return gun; }

    public Gun gun; // 사용할 총
    public Gun gunSpecial;
    public Transform gunPivot; // 총 배치의 기준점
                                                 // public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
                                                 // public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private PlayerInput playerInput; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {

    }

    private void OnEnable()
    {
        // 슈터가 활성화될 때 총도 함께 활성화
         gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // 슈터가 비활성화될 때 총도 함께 비활성화
         gun.gameObject.SetActive(false);
    }
    private void Update()
    {
        // 입력을 감지하고 총 발사하거나 재장전
        if (playerInput.fire)
        {
            
#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;
            else
            {
                gun.Fire();
            
                playerAnimator.SetTrigger("Shoot"); // 트리거

            }
#elif UNITY_ANDROID // or iOS 
            if (EventSystem.current.IsPointerOverGameObject(0) == true)
                return;
            else
            {
                gun.Fire();
            
                playerAnimator.SetTrigger("Shoot"); // 트리거

            }

#endif

            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;
            else
            {
                if (0 == GameManager.instance.WeaponNum)
                    gun.Fire();
                else
                    gunSpecial.Fire();

                playerAnimator.SetTrigger("Shoot"); // 트리거

            }

        }
        else if (playerInput.reload)
        {
            if (0 == GameManager.instance.WeaponNum)
            {
                if (gun.Reload())
                {
                    playerAnimator.SetBool("Reloading", true);
                }
                else
                    playerAnimator.SetBool("Reloading", false);
            }
            else
            {
                if (gunSpecial.Reload())
                {
                    playerAnimator.SetBool("Reloading", true);
                }
                else
                    playerAnimator.SetBool("Reloading", false);

            }

        }

        UpdateUI();
    }

    // 탄약 UI 갱신
    private void UpdateUI()
    {
        if (gun != null && UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            if (0 == GameManager.instance.WeaponNum)
                UIManager.instance.UpdateAmmoText(gun.MagAmmo, gun.AmmoRemain);
            else
                UIManager.instance.UpdateAmmoText(gunSpecial.MagAmmo, gunSpecial.AmmoRemain);

        }
    }

    #region 애니메이터의 IK 갱신 정보
    //private void OnAnimatorIK(int layerIndex) {
    //    // 총의 기준점 gunPivot을 3D 모델의 오른쪽 팔꿈치 위치로 이동
    //    gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
    //
    //    //ik를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
    //
    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);
    //
    //    //ik를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
    //
    //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    //
    //}
    #endregion
}