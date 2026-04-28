using UnityEngine;

/// <summary>
/// 적기가 죽을 때 아이템을 확률로 드랍하는 스크립트입니다.
/// 적기 프리팹에 붙이고, Inspector에서 아이템 프리팹 3종을 연결해 주세요.
/// </summary>
public class EnemyDrop : MonoBehaviour
{
    [Header("드랍 아이템 프리팹")]
    [Tooltip("ItemCoin 프리팹을 연결하세요.")]
    public GameObject itemCoin;

    [Tooltip("ItemPower 프리팹을 연결하세요.")]
    public GameObject itemPower;

    [Tooltip("ItemBoom 프리팹을 연결하세요.")]
    public GameObject itemBoom;

    // ───────────────────────────────────────────────
    // 드랍 확률 상수 (총합 100)
    //   None  : 0  ~ 29  → 30%
    //   Coin  : 30 ~ 59  → 30%
    //   Power : 60 ~ 79  → 20%
    //   Boom  : 80 ~ 99  → 20%
    // ───────────────────────────────────────────────
    private const int ProbNoneMax  = 30;   //  0 ~ 29
    private const int ProbCoinMax  = 60;   // 30 ~ 59
    private const int ProbPowerMax = 80;   // 60 ~ 79
    // 80 ~ 99 는 Boom

    /// <summary>
    /// 적기 사망 처리 시 이 메서드를 호출하세요.
    /// 예) 적기 HP 스크립트에서 Die() 안에 GetComponent<EnemyDrop>().TryDropItem(); 추가
    /// </summary>
    public void TryDropItem()
    {
        // 0 이상 100 미만의 정수를 랜덤으로 뽑습니다.
        int rand = Random.Range(0, 100);

        if (rand < ProbNoneMax)
        {
            // 30% — 아무것도 드랍하지 않습니다.
            Debug.Log("[EnemyDrop] 드랍 없음");
            return;
        }
        else if (rand < ProbCoinMax)
        {
            // 30% — Coin 드랍
            SpawnItem(itemCoin, "Coin");
        }
        else if (rand < ProbPowerMax)
        {
            // 20% — Power 드랍
            SpawnItem(itemPower, "Power");
        }
        else
        {
            // 20% — Boom 드랍
            SpawnItem(itemBoom, "Boom");
        }
    }

    void SpawnItem(GameObject prefab, string itemName)
    {
        // 프리팹이 Inspector에 연결되지 않았을 때 오류 방지
        if (prefab == null)
        {
            Debug.LogWarning($"[EnemyDrop] {itemName} 프리팹이 연결되지 않았습니다. Inspector에서 연결해 주세요.");
            return;
        }

        // 적기가 죽은 위치에 아이템을 생성합니다.
        Instantiate(prefab, transform.position, Quaternion.identity);
        Debug.Log($"[EnemyDrop] {itemName} 드랍!");
    }
}
