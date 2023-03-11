using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCreator : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private float nbBoidSpawn = 20;
    List<Boid> boidsInScene;

    //private Rigidbody2D rigidbody2d;
    [SerializeField] float avoidOtherStrength = 1f; //factor by which boid will try to avoid each other. Higher it is, higher the turn rate to avoid other.
    [SerializeField] float collisionAvoidCheckDistance = 10f; //distance of nearby boids to avoid collision     
	 // Start is called before the first frame update
	[SerializeField] float moveToCenterStrength = 2f; //factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
	[SerializeField] float localBoidsDistance = 5f; //effective distance to calculate the center
	[SerializeField] float alignWithOthersStrength = 10f; //factor determining turn rate to align with other boids
	[SerializeField] float alignmentCheckDistance = 100f; //distance up to which alignment of boids will be checked. Boids with greater distance than this will be ignored

    public GameObject moth;

    // Start is called before the first frame update
    void Start()
    {
        boidsInScene = new List<Boid>();
        for (int i = 0; i < nbBoidSpawn; i++)
        {
            Vector2 randDirection = new Vector2(Random.Range(-1,1), Random.Range(-1, 1));

            GameObject prefab = Instantiate(moth, randDirection, transform.rotation);

            float scale = Random.Range(0.5f,1f);
            prefab.transform.localScale = new Vector3(
                prefab.transform.localScale.x * scale,
                prefab.transform.localScale.y * scale,
                prefab.transform.localScale.z * scale
            );

            Boid boidToAdd = prefab.GetComponent<Boid>();

            boidsInScene.Add(boidToAdd);

            boidToAdd.speed = Random.Range(3f,5f);
            boidToAdd.boidsInScene = this.boidsInScene;
            boidToAdd.direction = randDirection;
            boidToAdd.avoidOtherStrength = this.avoidOtherStrength;
            boidToAdd.collisionAvoidCheckDistance = this.collisionAvoidCheckDistance;
            boidToAdd.moveToCenterStrength = this.moveToCenterStrength;
            boidToAdd.localBoidsDistance = this.localBoidsDistance;
            boidToAdd.alignmentCheckDistance = this.alignmentCheckDistance;
            boidToAdd.alignmentCheckDistance = this.alignmentCheckDistance;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
