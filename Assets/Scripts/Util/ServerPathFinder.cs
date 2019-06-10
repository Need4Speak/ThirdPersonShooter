using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ServerPathFinder : MonoBehaviour
{
    public Transform target;
    [SerializeField] float speed;
    NavMeshAgent agent;
    public NavMeshPath navMeshPath;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("敌人位置： " + transform.position);
        Debug.Log("玩家位置： " + target.position);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveTo(target.transform.position);
        // transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.position, speed * Time.deltaTime);
        if (gameObject.GetComponent<EnemyHealth>().IsAlive) {
            Move2();
        }
            
    }

    //让角色移动到目标位置
    private void MoveTo(Vector3 target)
    {
            Vector3 offSet = target - transform.position;
            transform.position += offSet.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(target, transform.position) < 0.5f)
            {
                //isOver = true;
                transform.position = target;
            }
        

    }

    /// <summary>
    /// 敌人走向玩家
    /// </summary>
    private void Move2()
    {
        if (Vector3.Distance(transform.position, target.position) > 5f)
        {
            float step = speed * Time.deltaTime;
            transform.forward = target.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, transform.position.y, target.position.z), step);
        }
    }
}
