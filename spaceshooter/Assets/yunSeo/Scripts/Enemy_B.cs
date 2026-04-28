using UnityEngine;

// Enemy_B: 중간형 에너미 (보통 체력, 빠른 속도)
public class Enemy_B : Enemy
{
    void Awake()
    {
        health = 4;
        moveSpeed = 3.5f;
    }
}
