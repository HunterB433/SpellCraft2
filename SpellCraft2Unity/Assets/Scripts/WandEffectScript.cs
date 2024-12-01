using UnityEngine;

public class WandEffectScript : MonoBehaviour
{
    public Transform startPoint; // Assign the start of the line in the Inspector
    public Transform endPoint;   // Assign the end of the line in the Inspector
    public float speed = 1f;     // Speed of movement along the line

    void Update()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start Point and End Point must be assigned!");
            return;
        }

        // Ping-Pong value for back-and-forth motion
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // Interpolate position along the line
        Vector3 newPosition = Vector3.Lerp(startPoint.position, endPoint.position, t);
        transform.position = newPosition;

        // No rotation logic to keep prefab orientation fixed
    }
}
