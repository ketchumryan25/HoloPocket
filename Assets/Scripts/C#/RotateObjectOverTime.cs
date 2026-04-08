using UnityEngine;

public class RotateObjectOverTime : MonoBehaviour
{
    public Vector3 axis = Vector3.up; // Axis to rotate around, e.g., Vector3.up for Y axis
    public float targetAngle = 90f;   // Target angle in degrees
    public float duration = 2f;       // Time to reach the target angle in seconds

    public float startAngle = 0f;
    public float elapsedTime = 0f;
    public bool isRotating = false;

    public void StartRotation()
    {
        if (!isRotating)
        {
            startAngle = GetCurrentAxisAngle();
            elapsedTime = 0f;
            isRotating = true;
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newAngle = Mathf.Lerp(startAngle, targetAngle, t);
            SetAxisAngle(newAngle);

            if (t >= 1f)
            {
                isRotating = false; // Rotation complete
            }
        }
    }

    private float GetCurrentAxisAngle()
    {
        // Get current rotation angle around the axis
        float angle;
        Vector3 currentEuler = transform.rotation.eulerAngles;
        if (axis == Vector3.up)
            angle = currentEuler.y;
        else if (axis == Vector3.right)
            angle = currentEuler.x;
        else if (axis == Vector3.forward)
            angle = currentEuler.z;
        else
        {
            // For arbitrary axes, more complex calculation needed
            angle = Vector3.Dot(transform.forward, axis); // approximate
        }
        return angle;
    }

    private void SetAxisAngle(float angle)
    {
        // Set rotation around the specified axis to the given angle
        Quaternion targetRotation = Quaternion.AngleAxis(angle, axis.normalized);
        transform.rotation = targetRotation;
    }
}