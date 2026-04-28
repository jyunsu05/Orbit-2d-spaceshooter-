using UnityEngine;

/// <summary>
/// 플레이어의 점수, 파워, 붐 카운트를 관리하는 싱글턴 클래스입니다.
/// 씬 어딘가의 빈 오브젝트(예: "GameManager")에 한 번만 붙이면 됩니다.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // ───────────────────────────────────────────────
    // 싱글턴: 씬 전체에서 단 하나의 인스턴스만 존재합니다.
    // 다른 스크립트에서 PlayerStats.Instance.AddScore() 처럼 접근합니다.
    // ───────────────────────────────────────────────
    public static PlayerStats Instance { get; private set; }

    [Header("현재 상태 (읽기 전용 참고용)")]
    [SerializeField] private int score = 0;      // 현재 점수
    [SerializeField] private int power = 1;      // 현재 파워 (기본값 1, 최대 3)
    [SerializeField] private int boomCount = 0;  // 현재 붐 사용 가능 횟수 (최대 3)

    // 외부에서 현재 값을 읽을 수 있도록 프로퍼티로 공개합니다.
    public int Score     => score;
    public int Power     => power;
    public int BoomCount => boomCount;

    // 파워와 붐의 최대치 상수
    public const int MaxPower = 3;
    public const int MaxBoom  = 3;

    void Awake()
    {
        // 씬에 이미 인스턴스가 있으면 중복 오브젝트를 제거합니다.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ───────────────────────────────────────────────
    // 점수 추가
    // ───────────────────────────────────────────────
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"[PlayerStats] 점수 +{amount} → 현재 점수: {score}");
    }

    // ───────────────────────────────────────────────
    // 파워 1 증가 (최대 3 초과 불가)
    // ───────────────────────────────────────────────
    public void AddPower()
    {
        if (power < MaxPower)
        {
            power++;
            Debug.Log($"[PlayerStats] 파워 +1 → 현재 파워: {power}");
        }
        else
        {
            Debug.Log($"[PlayerStats] 파워 이미 MAX ({MaxPower})");
        }
    }

    // ───────────────────────────────────────────────
    // 붐 카운트 1 증가 (최대 3 초과 불가)
    // ───────────────────────────────────────────────
    public void AddBoom()
    {
        if (boomCount < MaxBoom)
        {
            boomCount++;
            Debug.Log($"[PlayerStats] 붐 +1 → 현재 붐: {boomCount}");
        }
        else
        {
            Debug.Log($"[PlayerStats] 붐 이미 MAX ({MaxBoom})");
        }
    }
}
