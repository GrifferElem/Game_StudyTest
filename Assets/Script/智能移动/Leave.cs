using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : AgentBehavior
{
    public float escapeRadius;//逃离半径
    public float dangerRadius;//危险半径
    public float timeToTarget = 0.1f;

    //设立一个逃离半径，在逃离范围内以最大速度离开
    //在外层的危险半径范围内，随 距离拉远 而 速度下降
    //离开危险范围，速度为0
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 direction = transform.position - target.transform.position;
        float distance = direction.magnitude;
        if (distance > dangerRadius)
        {
            return steering;
        }

        float reduce = 0;
        if (distance < escapeRadius)
        {
            reduce = 0;
        }
        else
        {
            reduce = distance / dangerRadius * agent.maxSpeed;
        }
        float targetSpeed = agent.maxSpeed - reduce;

        Vector3 desiredVelocity = direction.normalized;
        desiredVelocity *= targetSpeed;

        steering.linear = (desiredVelocity - agent.velocity) / timeToTarget;
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }
        return steering;
    }
}
