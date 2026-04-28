using UnityEngine;

/// <summary>
/// 아이템 프리팹(ItemCoin, ItemPower, ItemBoom)에 공통으로 붙이는 충돌 처리 스크립트입니다.
/// Inspector에서 ItemType을 프리팹마다 다르게 지정해 주세요.
/// </summary>
public class ItemPickup : MonoBehaviour
{
    // ───────────────────────────────────────────────
    // 아이템 종류를 구분하는 열거형
    // Inspector 드롭다운에서 Coin / Power / Boom 중 하나를 선택합니다.
    // ───────────────────────────────────────────────
    public enum ItemType { Coin, Power, Boom }

    [Header("아이템 종류 설정")]
    [Tooltip("이 프리팹의 아이템 종류를 선택하세요.")]
    public ItemType itemType = ItemType.Coin;

    // ───────────────────────────────────────────────
    // Collider2D가 Is Trigger = On 상태일 때,
    // 다른 Collider2D와 겹치는 순간 이 메서드가 호출됩니다.
    // ───────────────────────────────────────────────
    void OnTriggerEnter2D(Collider2D other)
    {
        // "Player" 태그가 붙은 오브젝트와 충돌한 경우에만 처리합니다.
        // 플레이어 오브젝트에 Tag = "Player" 를 반드시 설정해 두세요.
        if (!other.CompareTag("Player"))
            return;

        // PlayerStats 싱글턴이 씬에 없으면 경고 후 종료합니다.
        if (PlayerStats.Instance == null)
        {
            Debug.LogWarning("[ItemPickup] PlayerStats 인스턴스가 씬에 없습니다. GameManager 오브젝트에 PlayerStats를 붙여주세요.");
            return;
        }

        ApplyEffect();

        // 효과를 적용한 뒤 아이템 오브젝트를 제거합니다.
        Destroy(gameObject);
    }

    void ApplyEffect()
    {
        switch (itemType)
        {
            case ItemType.Coin:
                // Coin 획득: AddScore() 메서드를 호출해 점수 +1000
                PlayerStats.Instance.AddScore(1000);
                Debug.Log("[ItemPickup] Coin 획득! +1000점");
                break;

            case ItemType.Power:
                // Power 획득: AddScore()로 점수 +500, AddPower()로 파워 1 증가 (최대 3)
                PlayerStats.Instance.AddScore(500);
                PlayerStats.Instance.AddPower();
                Debug.Log("[ItemPickup] Power 획득! +500점, 파워 증가");
                break;

            case ItemType.Boom:
                // Boom 획득: AddScore()로 점수 +500, AddBoom()으로 붐 카운트 1 증가 (최대 3)
                PlayerStats.Instance.AddScore(500);
                PlayerStats.Instance.AddBoom();
                Debug.Log("[ItemPickup] Boom 획득! +500점, 붐 증가");
                break;
        }
    }
}
