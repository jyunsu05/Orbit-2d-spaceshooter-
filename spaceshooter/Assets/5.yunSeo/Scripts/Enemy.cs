using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;

    public int health;
    public Sprite[] sprites;
    [Header("Movement")]
    public float moveSpeed = 3f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        CheckOutOfScreen();
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(HitFlash());
    }

    private void CheckOutOfScreen()
    {
        // 에너미가 화면 밖으로 나갔을 때 에너미를 제거하는 기능
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator HitFlash()
    {
        sr.sprite = sprites[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = sprites[0];
    }
}
