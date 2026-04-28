using UnityEngine;

// =============================================
// Player 스크립트
// 역할: 플레이어 캐릭터를 키보드로 움직이게 함
// =============================================

public class Player : MonoBehaviour
{
    // 인스펙터(유니티 화면)에서 숫자를 바꾸면 속도가 달라짐
    public float moveSpeed = 5f;

    // 멈춰 있을 때 보여줄 기본 이미지
    public Sprite spriteCenter;

    // 오른쪽/왼쪽 이동 애니메이션 프레임(순서대로 0,1,2,3 넣기)
    public Sprite[] rightSprites = new Sprite[4];
    public Sprite[] leftSprites = new Sprite[4];

    // 프레임이 바뀌는 속도 (작을수록 더 빨리 바뀜)
    public float frameTime = 0.08f;

    // Rigidbody2D = 물리 이동을 담당하는 컴포넌트 (충돌, 이동 등)
    private Rigidbody2D rb;

    // SpriteRenderer = 스프라이트(그림)를 화면에 보여주는 컴포넌트
    private SpriteRenderer sr;

    // 키보드 입력값을 저장하는 변수
    // moveX: 좌우 입력 (-1 = 왼쪽, 0 = 안누름, 1 = 오른쪽)
    // moveY: 상하 입력 (-1 = 아래,   0 = 안누름, 1 = 위)
    private float moveX;
    private float moveY;

    // 애니메이션 진행용 변수
    private float frameTimer;
    private int frameIndex;

    // 현재 바라보는 방향 저장 (-1: 왼쪽, 0: 중앙, 1: 오른쪽)
    private int currentDir;

    // -----------------------------------------------
    // Start: 게임이 시작될 때 딱 한 번만 실행됨
    // -----------------------------------------------
    void Start()
    {
        // 이 오브젝트에 붙어있는 Rigidbody2D를 가져옴
        rb = GetComponent<Rigidbody2D>();

        // 이 오브젝트에 붙어있는 SpriteRenderer를 가져옴
        sr = GetComponent<SpriteRenderer>();

        // 중력을 0으로 설정 (우주 공간이라 중력 없음)
        rb.gravityScale = 0f;

        // 물리 충돌 시 캐릭터가 빙글빙글 돌지 않도록 회전 고정
        rb.freezeRotation = true;

        // 시작할 때 기본 모습으로 설정
        if (spriteCenter != null) sr.sprite = spriteCenter;
    }

    // -----------------------------------------------
    // Update: 매 프레임마다 계속 실행됨 (1초에 60번 정도)
    // 역할: 키보드 입력을 읽어서 저장
    // -----------------------------------------------
    void Update()
    {
        // A/D 키 또는 방향키 좌우를 누르면 -1 또는 1이 들어옴
        moveX = Input.GetAxisRaw("Horizontal");

        // W/S 키 또는 방향키 상하를 누르면 -1 또는 1이 들어옴
        moveY = Input.GetAxisRaw("Vertical");

        // 좌우 입력에 따라 스프라이트(프레임 애니메이션) 변경
        UpdateSpriteByDirection();
    }

    void UpdateSpriteByDirection()
    {
        // 오른쪽 이동: rightSprites를 0→1→2→3 순서로 반복
        if (moveX > 0)
        {
            if (currentDir != 1)
            {
                currentDir = 1;
                frameIndex = 0;
                frameTimer = 0f;
            }

            PlayFrameAnimation(rightSprites);
            return;
        }

        // 왼쪽 이동: leftSprites를 0→1→2→3 순서로 반복
        if (moveX < 0)
        {
            if (currentDir != -1)
            {
                currentDir = -1;
                frameIndex = 0;
                frameTimer = 0f;
            }

            PlayFrameAnimation(leftSprites);
            return;
        }

        // 좌우 입력이 없으면 기본 이미지로 복귀
        currentDir = 0;
        frameIndex = 0;
        frameTimer = 0f;
        if (spriteCenter != null) sr.sprite = spriteCenter;
    }

    void PlayFrameAnimation(Sprite[] frames)
    {
        // 프레임 배열이 비어 있으면 아무 것도 하지 않음
        if (frames == null || frames.Length == 0) return;

        // 현재 프레임을 먼저 보여줌
        if (frames[frameIndex] != null) sr.sprite = frames[frameIndex];

        // 일정 시간이 지나면 다음 프레임으로 넘김
        frameTimer += Time.deltaTime;
        if (frameTimer >= frameTime)
        {
            frameTimer = 0f;
            frameIndex++;

            // 마지막 프레임 이후에는 다시 0번으로 돌아감
            if (frameIndex >= frames.Length) frameIndex = 0;
        }
    }

    // -----------------------------------------------
    // FixedUpdate: 물리 처리 전용 (고정된 시간마다 실행)
    // 역할: 실제로 캐릭터를 이동시킴
    // ※ 물리 이동은 Update 대신 FixedUpdate에서 해야 함
    // -----------------------------------------------
    void FixedUpdate()
    {
        // 입력값으로 이동 방향 벡터 만들기
        // normalized = 대각선 이동 시 속도가 빨라지지 않도록 크기를 1로 맞춤
        Vector2 direction = new Vector2(moveX, moveY).normalized;

        // 다음 프레임에 이동할 위치 계산
        // rb.position = 현재 위치
        // direction * moveSpeed * Time.fixedDeltaTime = 이번 프레임에 이동할 거리
        Vector2 nextPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        // 화면 밖으로 나가지 않도록 위치를 제한한 뒤 이동
        rb.MovePosition(ClampToScreen(nextPos));

        // 속도를 0으로 초기화 (관성 때문에 경계를 뚫고 나가는 것 방지)
        rb.linearVelocity = Vector2.zero;
    }

    // -----------------------------------------------
    // ClampToScreen: 화면 경계를 벗어나지 않게 위치를 조정
    // 받는 값: 이동하려는 위치 (pos)
    // 돌려주는 값: 화면 안으로 제한된 위치
    // -----------------------------------------------
    Vector2 ClampToScreen(Vector2 pos)
    {
        // 메인 카메라를 가져옴
        Camera cam = Camera.main;

        // 카메라가 없으면 그냥 원래 위치 그대로 반환
        if (cam == null) return pos;

        // 카메라 화면 세로 절반 크기 (orthographicSize = 카메라 높이 절반)
        float halfH = cam.orthographicSize;

        // 카메라 화면 가로 절반 크기 (세로 × 화면비율 = 가로)
        float halfW = halfH * cam.aspect;

        // 카메라의 현재 X, Y 위치
        float camX = cam.transform.position.x;
        float camY = cam.transform.position.y;

        // 스프라이트(그림) 크기를 가져와서 경계를 안쪽으로 줄임
        // → 캐릭터 중심이 아닌 캐릭터 가장자리가 화면 끝에 딱 맞도록
        float spriteHalfW = 0f;
        float spriteHalfH = 0f;
        if (sr != null)
        {
            spriteHalfW = sr.bounds.extents.x; // 스프라이트 가로 절반
            spriteHalfH = sr.bounds.extents.y; // 스프라이트 세로 절반
        }

        // Mathf.Clamp(값, 최솟값, 최댓값) = 값이 범위를 벗어나면 범위 안으로 잘라줌
        pos.x = Mathf.Clamp(pos.x, camX - halfW + spriteHalfW, camX + halfW - spriteHalfW);
        pos.y = Mathf.Clamp(pos.y, camY - halfH + spriteHalfH, camY + halfH - spriteHalfH);

        return pos;
    }
}
