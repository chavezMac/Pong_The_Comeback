using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.OnGoalTrigger(this);
    }
}
