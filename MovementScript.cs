using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    //Camera size variables
    float horizontalExtent, verticalExtent;

    //Paddles and associated Variables
    [SerializeField] GameObject RightPaddle;
    [SerializeField] GameObject LeftPaddle;
    float paddleSpped = 5f;
    float paddleScale;
    //
	
    //Ball and associated variables	
    [SerializeField] GameObject Ball;
    float ballSpeed = 7f;
    float radius;
    Vector3 ballDir;
    //
	
void Start () {

        verticalExtent = Camera.main.orthographicSize;
        horizontalExtent = verticalExtent * Camera.main.aspect;

        radius = Ball.transform.localScale.x / 2;
        paddleScale = RightPaddle.transform.localScale.y / 2;

        ballDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
	}
	
	
	void Update () {
		
	}
    private void FixedUpdate()
    {
        BallMovement();
        CheckBallCollisionWithWalls();
        CheckBallCollisionWithRightPaddle();
        CheckBallCollisionWithLeftPaddle();
        GetInputs();

    }

    void BallMovement()
    {
        Ball.transform.Translate(ballDir * ballSpeed * Time.deltaTime);
    }
    void GetInputs()
    {
        Vector3 pos;
        float dir = Input.GetAxisRaw("Vertical_L");
        LeftPaddle.transform.Translate(new Vector3(0, dir) * Time.deltaTime * paddleSpped);
	//Clamping paddle position
        pos = LeftPaddle.transform.position;
        pos.y = Mathf.Clamp(pos.y, -verticalExtent + paddleScale, verticalExtent - paddleScale);

        LeftPaddle.transform.position = pos;

        dir = Input.GetAxisRaw("Vertical_R");
        RightPaddle.transform.Translate(new Vector3(0, dir) * Time.deltaTime * paddleSpped);

	//Clamping paddle position
        pos = RightPaddle.transform.position;
        pos.y = Mathf.Clamp(pos.y, -verticalExtent + paddleScale, verticalExtent - paddleScale);

        RightPaddle.transform.position = pos;

    }

    void CheckBallCollisionWithWalls()
    {
        if(Ball.transform.position.y + radius >= verticalExtent || Ball.transform.position.y - radius <= -verticalExtent)//Up Wall || Down Wall
        {
            ballDir.y *= -1;//Bounce off Wall
        }
    }

    void CheckBallCollisionWithRightPaddle()
    {
        
        if (Ball.transform.position.x + radius >= RightPaddle.transform.position.x - RightPaddle.transform.localScale.x/2)//Ball.x is to the right of left edge of Ridght Paddle
        {
            if(Ball.transform.position.y < RightPaddle.transform.position.y + paddleScale && Ball.transform.position.y > RightPaddle.transform.position.y - paddleScale)//Ball.y is in Between Top and Bottom edges of Paddle
            {
                ballDir.x *= -1;//Bounce off Paddle
            }
        }
    }

    void CheckBallCollisionWithLeftPaddle()
    {

        if (Ball.transform.position.x - radius <= LeftPaddle.transform.position.x + LeftPaddle.transform.localScale.x / 2)//Ball.x is to the left of right edge of Left Paddle
        {
            if (Ball.transform.position.y < LeftPaddle.transform.position.y + paddleScale && Ball.transform.position.y > LeftPaddle.transform.position.y - paddleScale)//Ball.y is in Between Top and Bottom edges of Paddle
            {
                ballDir.x *= -1;//Bounce off Paddle
            }
        }
    }
}
