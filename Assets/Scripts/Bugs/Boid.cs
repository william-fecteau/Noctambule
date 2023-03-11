using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    public List<Boid> boidsInScene;
    [SerializeField] public Vector2 direction = new Vector2(1,1);
    //private Rigidbody2D rigidbody2d;
    [SerializeField] public float avoidOtherStrength = 1f; //factor by which boid will try to avoid each other. Higher it is, higher the turn rate to avoid other.
    [SerializeField] public float collisionAvoidCheckDistance = 10f; //distance of nearby boids to avoid collision     
	 // Start is called before the first frame update
	[SerializeField] public float moveToCenterStrength = 2f; //factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
	[SerializeField] public float localBoidsDistance = 5f; //effective distance to calculate the center
	[SerializeField] public float alignWithOthersStrength = 10f; //factor determining turn rate to align with other boids
	[SerializeField] public float alignmentCheckDistance = 100f; //distance up to which alignment of boids will be checked. Boids with greater distance than this will be ignored

    void Start()
    {
        //rigidbody2d = new Rigidbody2D();
		boidsInScene = new List<Boid>();
		boidsInScene.AddRange((Boid[])GameObject.FindObjectsOfType(typeof(Boid)));
    }

    // Update is called once per frame
    void Update()
    {
		AlignWithOthers();
		MoveToCenter();
		AvoidOtherBoids();
		transform.Translate(direction * (speed * Time.deltaTime));
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void MoveToCenter(){

    	Vector2 positionSum = transform.position;//calculate sum of position of nearby boids and get count of boid
    	int count = 0;

    	foreach (Boid boid in boidsInScene)
    	{
    		float distance = Vector2.Distance(boid.transform.position, transform.position);
    		if (distance <= localBoidsDistance){
    			positionSum += (Vector2)boid.transform.position;
    			count++;
    		}
    	}

    	if (count == 0){
    		return;
    	}

    	//get average position of boids
    	Vector2 positionAverage = positionSum / count;
    	positionAverage = positionAverage.normalized;
    	Vector2 faceDirection = (positionAverage - (Vector2) transform.position).normalized;

    	//move boid toward center
    	float deltaTimeStrength = moveToCenterStrength * Time.deltaTime;
    	direction=direction+deltaTimeStrength*faceDirection/(deltaTimeStrength+1);
    	direction = direction.normalized;
    }
	
	void AvoidOtherBoids(){
		Vector2 faceAwayDirection = Vector2.zero;//this is a vector that will hold direction away from near boid so we can steer to it to avoid the collision.

		//we need to iterate through all boid
		foreach (Boid boid in boidsInScene){
			float distance = Vector2.Distance(boid.transform.position, transform.position);

			//if the distance is within range calculate away vector from it and subtract from away direction.
			if (distance <= collisionAvoidCheckDistance){
				faceAwayDirection =faceAwayDirection+ (Vector2)(transform.position - boid.transform.position);
			}
		}

		faceAwayDirection = faceAwayDirection.normalized;//we need to normalize it so we are only getting direction

		direction=direction+avoidOtherStrength*faceAwayDirection/(avoidOtherStrength +1);
		direction = direction.normalized;
	}

	void AlignWithOthers(){
		//we will need to find average direction of all nearby boids
		Vector2 directionSum = Vector3.zero;
		int count = 0;

		foreach (var boid in boidsInScene){
			float distance = Vector2.Distance(boid.transform.position, transform.position);
			if (distance <= alignmentCheckDistance){
				directionSum += boid.direction;
				count++;
			}
		}

		Vector2 directionAverage = directionSum / count;
		directionAverage = directionAverage.normalized;

		//now add this direction to direction vector to steer towards it
		float deltaTimeStrength = alignWithOthersStrength * Time.deltaTime;
		direction=direction+deltaTimeStrength*directionAverage/(deltaTimeStrength+1);
		direction = direction.normalized;

	}
}