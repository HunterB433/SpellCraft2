using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 100f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its Z-axis
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
}
