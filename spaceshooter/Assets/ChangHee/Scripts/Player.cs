using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 (인스펙터에서 조절 가능)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;     // 중력 없애기 (우주 공간이므로)
        rb.freezeRotation = true; // 물리 충돌로 인한 회전 방지
    }

    void Update()
    {
        // 키보드 입력 받기 (WASD 또는 방향키)
        float moveX = Input.GetAxisRaw("Horizontal"); // 좌우: -1, 0, 1
        float moveY = Input.GetAxisRaw("Vertical");   // 상하: -1, 0, 1

        // 이동 방향 계산 후 속도 적용
        Vector2 direction = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // 화면 밖으로 나가지 않도록 제한
        ClampToScreen();
    }

    void ClampToScreen()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        // 카메라 화면 경계 계산
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        // 현재 위치를 화면 안으로 제한
        float x = Mathf.Clamp(transform.position.x, -halfW, halfW);
        float y = Mathf.Clamp(transform.position.y, -halfH, halfH);

        transform.position = new Vector3(x, y, 0f);
    }
}
