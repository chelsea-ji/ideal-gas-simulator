using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] public Vector2 normal = new Vector2(0, -1);
    [SerializeField] public float length = 0f;
    public float pressure = 0f;
    private int count = 0;

    private void Start()
    {
        pressure = 0f;
        count = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count++;
        Debug.Log(count);
        GameObject other = collision.gameObject;
        if (other.tag == "particle")
        {
            Particle particle = other.GetComponent<Particle>();

            Vector2 vN = Vector2.Dot(particle.velocity, normal) * normal;

            pressure += particle.mass * vN.magnitude * 2;
        }
    }
}


