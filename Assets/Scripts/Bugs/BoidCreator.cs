using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCreator : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private float nbBoidSpawn = 20;
    List<Boid> boidsInScene;
    [SerializeField] public float turnSpeed = 10f;
    [SerializeField] public float maxDistance = 10f;
    [SerializeField] public float maxAngle = 120f;
    [SerializeField] public float cohesionWeight = 0.3f;
    [SerializeField] public float alignmentWeight = 10f;
    [SerializeField] public float separationWeight = 10f;
    public GameObject moth;

    // Start is called before the first frame update
    void Start()
    {
        boidsInScene = new List<Boid>();
        for (int i = 0; i < nbBoidSpawn; i++)
        {
            Vector2 randDirection = new Vector2(Random.Range(-1,1), Random.Range(-1, 1));

            GameObject prefab = Instantiate(moth, randDirection, transform.rotation);

            float scale = Random.Range(0.7f,0.3f);
            prefab.transform.localScale = new Vector3(
                prefab.transform.localScale.x * scale,
                prefab.transform.localScale.y * scale,
                prefab.transform.localScale.z * scale
            );

            Boid boidToAdd = prefab.GetComponent<Boid>();

            boidsInScene.Add(boidToAdd);

            boidToAdd.speed = Random.Range(2f,3f);
            boidToAdd.Boids = this.boidsInScene;
            boidToAdd.movementVector = randDirection;

            boidToAdd.turnSpeed         = this.turnSpeed;
            boidToAdd.maxDistance       = this.maxDistance;
            boidToAdd.maxAngle          = this.maxAngle;
            boidToAdd.cohesionWeight    = this.cohesionWeight;
            boidToAdd.alignmentWeight   = this.alignmentWeight;
            boidToAdd.separationWeight  = this.separationWeight;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
