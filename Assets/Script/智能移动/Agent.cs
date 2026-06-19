using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Rigidbody rb;
    protected Steering steering;

    public float maxSpeed;
    public float maxAccel;
    public float rotation;//角速度
    public float orientation;//朝向
    public Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        velocity = Vector3.zero;
        steering = new Steering();
    }
    public virtual void Update()
    {
        //if(rb ==null) return;
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        //限制朝向
        if (orientation < 0)
        {
            orientation += 360f;
        }
        else if (orientation > 360f)
        {
            orientation -= 360f;
        }
        transform.Translate(displacement, Space.World);
        transform.rotation = Quaternion.identity;

        transform.Rotate(Vector3.up,orientation);
    }
    public virtual void LateUpdate()
    {
        //更新代理的速度和旋转    
        velocity += steering.linear * Time.fixedDeltaTime;
        rotation += steering.angular * Time.fixedDeltaTime;
        //对速度限制
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (steering.angular == 0)
        {
            rotation = 0f;
        }
        if (steering.linear.sqrMagnitude == 0)
        {
            velocity = Vector3.zero;
        }
        //使 steering初始化，防止受上一帧影响
        steering = new Steering();
    }
    private void FixedUpdate()
    {
        if(rb ==null) return;

        Vector3 displacement = velocity * Time.fixedDeltaTime;
        orientation += rotation * Time.fixedDeltaTime;
        if (orientation < 0)
        {
            orientation += 360f;
        }else if(orientation > 360f)
        {
            orientation -= 360f;
        }
        rb.AddForce(displacement, ForceMode.VelocityChange);
        Vector3 orientationVec = OriToVec(orientation);
        rb.rotation = Quaternion.LookRotation(orientationVec, Vector3.up);
    }

    //朝向角度转换成方向向量
    private Vector3 OriToVec(float ori)
    {
        Vector3 vec = Vector3.zero;
        vec.x = Mathf.Sin(ori * Mathf.Deg2Rad);
        vec.z = Mathf.Cos(ori * Mathf.Deg2Rad);
        return vec.normalized;
    }
    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }
}
