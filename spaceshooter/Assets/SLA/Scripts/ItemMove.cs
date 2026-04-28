using UnityEngine;

public class ItemMove : MonoBehaviour
{
    [Header("Move Settings")]
    [Tooltip("아이템이 아래로 떨어지는 속도입니다.")]
    public float moveSpeed = 6.0f;

    [Header("Destroy Settings")]
    [Tooltip("이 값보다 아래로 내려가면 아이템을 자동으로 삭제합니다.")]
    public float destroyY = -7.0f;

    void Update()
    {
        MoveDown();
        DestroyIfOutOfScreen();
    }

    void MoveDown()
    {
        // 모든 아이템이 공통으로 아래 방향으로 천천히 떨어지도록 이동시킵니다.
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void DestroyIfOutOfScreen()
    {
        // 화면 아래 경계 밖으로 완전히 벗어난 아이템은 메모리 낭비를 막기 위해 제거합니다.
        if (transform.position.y < destroyY)
        {
            Debug.Log("아이템이 제거되었습니다.");
            Destroy(gameObject);
        }
    }
}