using UnityEditor.Rendering;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] float mass = 0f;
    [SerializeField] float temperature = 0f;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] Wall topWall;
    [SerializeField] Wall bottomWall;
    [SerializeField] Wall rightWall;
    [SerializeField] Wall leftWall;

    public Particle[] particles;
    public Wall[] walls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(-5.9f, 1.9f), 
                Random.Range(-3.9f, 3.9f)
            );

            float speed = 0f;

            if (i == 0)
            {
                float kB = 1.380649e-23f;
                float N = particles.Length;
                speed = Mathf.Sqrt(2f * N * kB * temperature / mass);
            }

            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector2 velocity = dir * speed;

            GameObject instance = Instantiate(particlePrefab, pos, Quaternion.identity);
            Particle particle = instance.GetComponent<Particle>();
            particle.velocity = velocity;
            particle.mass = mass;
            particles[i] = particle;
        }

        walls[0] = topWall;
        walls[1] = bottomWall;
        walls[2] = rightWall;
        walls[3] = leftWall;
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
