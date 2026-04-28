using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float tileHeight = 10f; // 배경 1장의 높이
    public float resetY = -15f;    // 이 위치보다 아래로 내려가면 위로 올림

    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.position.y < resetY)
            {
                // 가장 위에 있는 자식을 찾아서 그 위에 배치
                float maxY = GetHighestY();
                child.position = new Vector3(child.position.x, maxY + tileHeight, child.position.z);
            }
        }
    }

    float GetHighestY()
    {
        float maxY = float.MinValue;
        foreach (Transform child in transform)
        {
            if (child.position.y > maxY)
                maxY = child.position.y;
        }
        return maxY;
    }
}
