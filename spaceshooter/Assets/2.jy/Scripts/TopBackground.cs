using UnityEngine;

public class TopBackground : MonoBehaviour
{
    public float speed = 1.2f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}

