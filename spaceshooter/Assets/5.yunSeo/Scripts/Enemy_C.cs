using UnityEngine;

// Enemy_C: 강화형 에너미 (높은 체력, 느린 속도)
public class Enemy_C : Enemy
{
    void Awake()
    {
        health = 8;
        moveSpeed = 1.5f;
    }
}
