using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purse : Seek
{
    private GameObject targetReal;
    private Agent targetAgent;

    public float maxPrediction;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetReal = target;
        target = new GameObject();
    }
    private void OnDestroy()
    {
        Destroy(targetReal);
    }

    public override Steering GetSteering()
    {
        Vector3 direaction = targetReal.transform.position - transform.position;
        float distance = direaction.magnitude;
        float speed = agent.velocity.magnitude;
        float prediction;

        //当速度非常慢时，可以预测目标物体未来更多时间后运动后的位置
        if (speed < distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        //如果速度非常快就让预测时间小一点
        else
        {
            prediction = distance / speed;
        }
        //预测目标未来的位置
        target.transform.position = targetReal.transform.position;
        target.transform.position += targetAgent.velocity * prediction;

        return base.GetSteering();
    }
}