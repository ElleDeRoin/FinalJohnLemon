using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform pivotPoint; 
    public Vector3 rotationAxis = Vector3.up; 
    public float rotationSpeed = 30f;    // <-- Add this line

    void Update()
    {
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Pivot point not assigned for circular movement.");
        }
    }
}
