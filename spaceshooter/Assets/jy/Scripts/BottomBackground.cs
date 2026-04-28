using UnityEngine;

public class BottomBackground : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
