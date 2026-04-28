using UnityEngine;

public class MiddleBackground : MonoBehaviour
{
    public float speed = 0.8f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
