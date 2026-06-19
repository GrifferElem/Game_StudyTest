using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : AgentBehavior
{
    public float targetRadius; //目标半径
    public float slowRadius;// 减速半径
    public float timeToTarget = 0.1f;

    //假设目标在一个 半径targetRadius 的范围内，进入这个范围时速度为0
    //在目标范围外还有一层 半径slowRadius 的减速范围，随着距离目标越近，速度下降
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        //1 先获得与目标的距离
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;
        //2 根据距离获得目标的速度(速率）
        float targetSpeed;//此时是标量
        
        if (distance < targetRadius)
        {
            //直接返回新new 的steering，在agent 里，linear为0时设定了velocity = 0 来停止运动
            return steering;
        }else if (distance > slowRadius)
        {
            //离远，全速
            targetSpeed = agent.maxSpeed;
        }
        else
        {
            //在进入 减速范围 随 距离拉近 时减速
            targetSpeed = agent.maxSpeed * distance / slowRadius;
        }
        //3 然后获得steering的加速度
        Vector3 desiredVelocity = direction.normalized;//预期速度获得方向
        desiredVelocity *= targetSpeed;//获得大小

        steering.linear = (desiredVelocity - agent.velocity) / timeToTarget;//公式 a = (v2-v1) / t 
        //4 限制加速度不超过agent.maxAccel
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }

        return steering;
    }
}