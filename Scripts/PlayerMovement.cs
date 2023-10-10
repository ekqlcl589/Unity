using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    private const float moveSpeed = 5f; // 앞뒤 움직임의 속도
    private const float rotateSpeed = 180f; // 좌우 회전 속도
    private const float jumpForce = 700f; // 점프 힘

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private const float point = 0f;

    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행, 기본값은 0.02 
        Move();

        Rotate();

        Use();

        Roll();

        PickUp();

        //if (playerInput.jump) 점프 테스트 코드
        //    Jump();

        playerAnimator.SetFloat("Speed", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        Vector3 moveDist = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + moveDist);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(point, turn, point);
    }
    //입력값에 따라 캐릭터 점프
    private void Jump()
    {
        playerRigidbody.velocity = Vector3.zero;

        playerRigidbody.AddForce(new Vector3(point, jumpForce, point));

    }

    private void Use()
    {
        if (Input.GetKeyUp(KeyCode.E))
            playerAnimator.SetTrigger("Use");
    }
    private void Roll()
    {
        if (Input.GetKeyUp(KeyCode.T))
            playerAnimator.SetTrigger("Roll");
    }

    private void PickUp()
    {
        if (Input.GetKeyUp(KeyCode.Y))
            playerAnimator.SetTrigger("Pickup");
    }

}