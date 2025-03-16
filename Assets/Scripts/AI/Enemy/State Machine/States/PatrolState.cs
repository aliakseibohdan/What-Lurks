using UnityEngine;

public class PatrolState : BaseState
{
    public int currentWaypointIndex;
    public float waitAtWaypointTime = 2f;
    public float waitAtWaypointTimer;

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitAtWaypointTimer += Time.deltaTime;
            if (waitAtWaypointTimer > waitAtWaypointTime)
            {
                if (currentWaypointIndex > enemy.path.waypoints.Count - 1)
                    currentWaypointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.waypoints[currentWaypointIndex++].position);
                waitAtWaypointTimer = 0;
            }
        }
    }
}