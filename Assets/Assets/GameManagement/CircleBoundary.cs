using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class CircleBoundary : MonoBehaviour
{
    public int segments = 64; 
    public float radius = 10f; 

    void Start()
    {
        GenerateCircle();
    }
    // using the edgecolloider and making it a circle
    void GenerateCircle()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[segments + 1];
        float angleStep = 360f / segments;
        for (int i = 0; i < segments + 1; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
        edge.points = points;
    }
    // show the visible circle boundary
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position;
        Vector3 previousPoint = center + new Vector3(radius, 0, 0);
        float angleStep = 360f / segments;
        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = newPoint;
        }
    }
}
