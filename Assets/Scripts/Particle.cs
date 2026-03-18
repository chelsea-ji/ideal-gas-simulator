using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] public Vector2 velocity;
    public float mass;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "wall")
        {
            Wall wall = other.GetComponent<Wall>();

            Vector2 normal = wall.normal.normalized;
            velocity = Vector2.Reflect(velocity, normal);
        }
        else if (other.tag == "particle")
        {
            if (GetInstanceID() < other.GetInstanceID())
            {
                Particle particle = other.GetComponent<Particle>();
                
                Vector2 normal = (transform.position - other.transform.position).normalized;
                Vector2 vN = Vector2.Dot(velocity, normal) * normal;
                Vector2 otherVN = Vector2.Dot(particle.velocity, normal) * normal;
                
                Vector2 vT = velocity - vN;
                Vector2 otherVT = particle.velocity - otherVN;
                
                velocity = otherVN + vT;
                particle.velocity = vN + otherVT;
            }
        }
    }
}
