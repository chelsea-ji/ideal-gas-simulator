using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class ParticleSpeedDisplay : MonoBehaviour
{
    public TMP_Text speedText;
    public Particle[] particles;
    public Wall[] walls;
    [SerializeField] public Main main;

    void Start()
    {
        particles = main.particles;
        walls = main.walls;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        particles = main.particles;
        walls = main.walls;

        string output = "";
        float maxSpeed = -1e-100f;
        float minSpeed = 100f;
        float rmsSpeed = 0f;
        float meanSpeed = 0f;
        float effectiveTemperature = 0f;
        float N = particles.Length;
        float pressure = 0f;
        float mass = particles[0].mass;

        for (int i = 0; i < particles.Length; i++)
        {
            Vector2 velocity = particles[i].velocity;
            float speed = velocity.magnitude;
            if (speed > maxSpeed)
            {
                maxSpeed = speed;
            }
            if (speed < minSpeed)
            {
                minSpeed = speed;
            }
            meanSpeed += speed; 
            rmsSpeed += speed * speed;
            output += $"{i+": "+speed.ToString(): F2}  ";
        }

        for (int i = 0; i < walls.Length; i++)
        {
            pressure += walls[i].pressure / walls[i].length;
        }

        rmsSpeed = Mathf.Sqrt(rmsSpeed / N);
        float kineticEnergy = (rmsSpeed * rmsSpeed * mass * N) * 0.5f;
        meanSpeed = meanSpeed / N;
        effectiveTemperature = (mass * (rmsSpeed*rmsSpeed)) / (2f * 1.38e-23f);
        pressure /= Time.time;

        output += $"Max Speed: {maxSpeed.ToString(): F2}\n";
        output += $"Min Speed: {minSpeed.ToString(): F2}\n";
        output += $"RMS Speed: {rmsSpeed.ToString(): F2}\n";
        output += $"Mean Speed: {meanSpeed.ToString(): F2}\n";
        output += $"Kinetic Energy: {kineticEnergy.ToString(): F2}\n";
        output += $"Pressure: {pressure.ToString(): F2}\n";
        output += $"Temperature: {effectiveTemperature.ToString(): F2}";

        speedText.text = output;
    }
}
