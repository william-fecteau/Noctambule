using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float turnSpeed = 10f;
    [SerializeField] public float maxDistance = 10f;
    [SerializeField] public float maxAngle = 120f;
    [SerializeField] public float cohesionWeight = 0.3f;
    [SerializeField] public float alignmentWeight = 10f;
    [SerializeField] public float separationWeight = 10f;

    public Vector2 cohesionVector;
    public Vector2 alignmentVector;
    public Vector2 separationVector;
    public List<Boid> Boids = new List<Boid>();
	public Vector2 movementVector;

    public void Start()
    {
        // Find all the boid moths in the scene
        Boids.AddRange(FindObjectsOfType<Boid>());
        Boids.Remove(this);
    }

    public void Update()
    {
        // Calculate the cohesion, alignment, and separation vectors for the boid moth
        cohesionVector = CalculateCohesionVector();
        alignmentVector = CalculateAlignmentVector();
        separationVector = CalculateSeparationVector();

        // Combine the vectors to get the final movement vector for the boid moth
        movementVector = (cohesionVector * cohesionWeight) + (alignmentVector * alignmentWeight) + (separationVector * separationWeight);

        // Rotate the boid moth towards the direction it's moving in
        if (movementVector.magnitude > 0)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), turnSpeed * Time.deltaTime);
        }
    }

    public void FixedUpdate()
    {
        // Move the boid moth in the direction of the movement vector
        GetComponent<Rigidbody2D>().velocity = movementVector.normalized * speed;
    }

    public Vector2 CalculateCohesionVector()
    {
        // Calculate the center of mass of all the boid moths
        Vector2 centerOfMass = Vector2.zero;
        foreach (Boid Boid in Boids)
        {
            centerOfMass += (Vector2) Boid.transform.position;
        }
        centerOfMass /= Boids.Count;

        // Calculate the vector towards the center of mass
        Vector2 cohesionVector = centerOfMass - (Vector2) transform.position;

        // Normalize the vector and scale it by the distance to the center of mass
        cohesionVector.Normalize();
        cohesionVector *= maxDistance;

        // Only apply the vector if it's within the maximum angle
        if (Vector2.Angle(transform.right, cohesionVector) > maxAngle)
        {
            cohesionVector = Vector2.zero;
        }

        return cohesionVector;
    }

    public Vector2 CalculateAlignmentVector()
    {
        // Calculate the average velocity of all the boid moths
        Vector2 averageVelocity = Vector2.zero;
        foreach (Boid Boid in Boids)
        {
            averageVelocity += Boid.GetComponent<Rigidbody2D>().velocity;
        }
        averageVelocity /= Boids.Count;

        // Only apply the vector if it's within the maximum angle
        if (Vector2.Angle(transform.right, averageVelocity) > maxAngle)
        {
            averageVelocity = Vector2.zero;
        }

        return averageVelocity;
    }
	private Vector2 CalculateSeparationVector()
{
    Vector2 separationVector = Vector2.zero;

    foreach (Boid boidMoth in Boids)
    {
        if (boidMoth != this)
        {
            // Calculate the vector away from the other boid moth
            Vector2 awayVector = (Vector2)transform.position - (Vector2)boidMoth.transform.position;

            // Only apply the vector if it's within the maximum distance
            if (awayVector.magnitude < maxDistance)
            {
                bool shouldAvoid = false;

                // Check if there are any tilemap colliders between this boid moth and the other boid moth
                RaycastHit2D[] hits = Physics2D.RaycastAll(boidMoth.transform.position, awayVector.normalized, awayVector.magnitude, 2);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.isTrigger || hit.collider.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
                    {
                        // Ignore trigger colliders and objects with Ignore Raycast layer
                        continue;
                    }

                    shouldAvoid = true;
					
                    break;
                }

                if (shouldAvoid)
                {
                    // Add the vector away from the other boid moth to the separation vector
                    separationVector += awayVector;
                }
            }
        }
    }

    // Only apply the vector if it's not zero
    if (separationVector.magnitude > 0)
    {
        separationVector /= Boids.Count - 1;

        // Normalize the vector and scale it by the maximum distance
        separationVector.Normalize();
        separationVector *= maxDistance;

        // Only apply the vector if it's within the maximum angle
        if (Vector2.Angle(transform.right, separationVector) > maxAngle)
        {
            separationVector = Vector2.zero;
        }
    }

    return separationVector;
}
}