using UnityEngine;

// Enemy_A: 기본형 에너미 (낮은 체력, 보통 속도)
public class Enemy_A : Enemy
{
    void Awake()
    {
        health = 2;
        moveSpeed = 2f;
    }
}
