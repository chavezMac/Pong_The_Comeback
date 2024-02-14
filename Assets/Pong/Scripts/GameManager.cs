using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;

    public GameObject powerUp1;

    public GameObject powerUp2;
    public int randomNum;
    public Transform ball;
    public Transform spawnTransform;
    public float startSpeed = 3f;
    public GoalTrigger leftGoalTrigger;
    public GoalTrigger rightGoalTrigger;

    public PowerUpTrigger powerUp1Trigger;

    public PowerUpTrigger powerUp2Trigger;

    public GameObject leftPaddle;

    public GameObject rightPaddle;
    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;
    private Vector3 ballStartPos;

    private float localx;
    private float localy;
    private float localz;

    private const int scoreToWin = 11;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = ball.position;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
        //set power up as inactive
        powerUp1.SetActive(false);
        powerUp2.SetActive(false);
    }

    // If the ball entered the goal area, increment the score, check for win, and reset the ball
    public void OnGoalTrigger(GoalTrigger trigger)
    {
        if (trigger == leftGoalTrigger || ball.position.x < -10)
        {
            rightPlayerScore++;
            Debug.Log($"Right player scored: {rightPlayerScore}");
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(-1f);
            }
            onScoreCount(rightPlayerScore);
        }
        else if (trigger == rightGoalTrigger || ball.position.x > 10)
        {

            leftPlayerScore++;
            Debug.Log($"Left player scored: {leftPlayerScore}");
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(1f);
            }
            onScoreCount(leftPlayerScore);
        }

        if(rightPlayerScore == leftPlayerScore) {
            scoreText.text = $"<color=\"white\"><b>{leftPlayerScore}</b> - <color=\"white\"><b>{rightPlayerScore}</b>";
            GameObject.Find("Left Paddle").GetComponent<Renderer>().material.color = Color.white;
            GameObject.Find("Right Paddle").GetComponent<Renderer>().material.color = Color.white;
        }else if(rightPlayerScore > leftPlayerScore) {
            scoreText.text = $"<color=\"red\"><b>{leftPlayerScore}</b> - <color=\"green\"><b>{rightPlayerScore}</b>";
            GameObject.Find("Left Paddle").GetComponent<Renderer>().material.color = Color.red;
            GameObject.Find("Right Paddle").GetComponent<Renderer>().material.color = Color.green;
        }else {
            scoreText.text = $"<color=\"green\"><b>{leftPlayerScore}</b> - <color=\"red\"><b>{rightPlayerScore}</b>";
            GameObject.Find("Left Paddle").GetComponent<Renderer>().material.color = Color.green;
            GameObject.Find("Right Paddle").GetComponent<Renderer>().material.color = Color.red;
        }

        if(rightPlayerScore == scoreToWin) {
            scoreText.text = $"<color=\"yellow\"><b>GAME OVER: Right Paddle Wins </b>";
            
        }else if(leftPlayerScore == scoreToWin){
            scoreText.text = $"<color=\"yellow\"><b>GAME OVER: Left Paddle Wins </b>";
      
        }
        changeBackground();
    }

    // If the ball entered the power-up area, apply the power-up and reset the ball
    public void OnPowerUpTrigger(PowerUpTrigger trigger)
    {
        if(trigger == powerUp1Trigger) {
            powerUp1.SetActive(false);
            powerUp2.SetActive(false);
            Debug.Log("Power-up triggered!");
            Rigidbody ballBody = ball.GetComponent<Rigidbody>();
            ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
        }else if(trigger == powerUp2Trigger) {
            powerUp1.SetActive(false);
            powerUp2.SetActive(false);
            Debug.Log("Power-up triggered!");
            Vector3 currentScale = ball.localScale;
            ball.localScale = new Vector3(currentScale.x * 2, currentScale.y * 2, currentScale.z * 2);
        }
        
    }

    public void onScoreCount(int playerScore) {
        if(playerScore == 1) {
            powerUp1.SetActive(true);
            powerUp2.SetActive(true);
        }

        if(playerScore == 7) {
            powerUp1.SetActive(true);
            powerUp2.SetActive(true);
        }
    }

    public void changeBackground() {
        randomNum = Random.Range(1, 4);
        if(randomNum == 1) {
            GameObject.Find("Ground").GetComponent<Renderer>().material.color = Color.gray;
        }else if(randomNum == 2) {
            GameObject.Find("Ground").GetComponent<Renderer>().material.color = Color.black;
        }else if(randomNum == 3) {
            GameObject.Find("Ground").GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    void ResetBall(float directionSign)
    {
        ball.position = ballStartPos;

        // Start the ball within 20 degrees off-center toward direction indicated by directionSign
        directionSign = Mathf.Sign(directionSign);
        Vector3 newDirection = new Vector3(-directionSign, 0f, 0f) * startSpeed;
        newDirection = Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f) * newDirection;

        var rbody = ball.GetComponent<Rigidbody>();
        rbody.velocity = newDirection;
        rbody.angularVelocity = new Vector3();

        // We are warping the ball to a new location, start the trail over
        ball.GetComponent<TrailRenderer>().Clear();
        ball.transform.localScale = new Vector3(.5f, .5f, .5f);
    }
}
