using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Soldier : BaseAgent {

    public float runAwayDistance = 1;
    public FindAlliesAndEnemies faae;
    public UnityEvent seeEvent;
    public UnityEvent cantSeeEvent;
    public Transform[] points;
    public Transform eyes;
    public LayerMask mask;
    private int destPoint = 0;
    BaseAgent target;
    /***STATE MACHINE VARS AND CONSTS***/
    private int state = 0;
    static int STATE_PATROL = 0;
    static int STATE_FIGHT = 1;
    static int STATE_CHASE = 2;
    static int STATE_RUN_AWAY = 3;
    /***********************************/

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        if (seeEvent == null)
            seeEvent = new UnityEvent();
        if (cantSeeEvent == null)
            cantSeeEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateStateMachine();
	}
    void UpdateStateMachine()
    {
        if (state == STATE_PATROL)
        {
            if (IsSuperiority() && EnemyNearby())
            {
                SetStateChase();
                return;
            }
            if(Loses())
            {
                SetStateRunAway();
                return;
            }
            Patrol();
            return;
        }
        if (state == STATE_CHASE)
        {
            if (IsSuperiority())
            {
                if (EnemyNearby())
                {
                    target = faae.NearestEnemy();
                    if (CanSee(target.gameObject))
                    {
                        SetStateFight();
                    }
                    return;
                }
                else
                {
                    Chase();
                    return;
                }
            }
            else
            {
                SetStateRunAway();
            }
        }
        if (state == STATE_RUN_AWAY)
        {
            if (EnemyAway())
            {
                SetStatePatrol();
                return;
            }
            else
            {
                RunAway();
                return;
            }
        }
        if (state == STATE_FIGHT)
        {
            if(!target)
            {
                SetStateChase();
                cantSeeEvent.Invoke();
                return;
            }
            if (Loses())
            {
                cantSeeEvent.Invoke();
                SetStateRunAway();
                return;
            }
            if (EnemyAway())
            {
                cantSeeEvent.Invoke();
                SetStatePatrol();
                return;
            }
            if (!CanSee(target.gameObject))
            {
                cantSeeEvent.Invoke();
                SetStateChase();
                return;
            }
            Fight();
            
        }
    }
    /***********STATES INITIALIZATION**************/
    protected void SetStateFight()
    {
        agent.Stop();
        destination = target.transform;
        seeEvent.Invoke();
        state = STATE_FIGHT;
    }
    protected void SetStatePatrol()
    {
        agent.Resume();
        state = STATE_PATROL;
    }
    protected void SetStateChase()
    {
        agent.Resume();
        state = STATE_CHASE;
    }
    protected void SetStateRunAway()
    {
        agent.Resume();
        state = STATE_RUN_AWAY;
    }
    /*********************************************/
    protected bool IsSuperiority()
    {
        return (faae.NumberOfEnemies() <= faae.NumberOfAllies()+1);
    }
    protected bool Loses()
    {
        return (faae.NumberOfEnemies() > faae.NumberOfAllies()+1);
    }
    protected bool EnemyNearby()
    {
        return faae.NumberOfEnemies() > 0;
    }
    protected bool EnemyAway()
    {
        return faae.NumberOfEnemies() == 0;
    }
    protected void Chase()
    {
        BaseAgent ba = faae.NearestEnemy();
        if (ba)
        {
            agent.destination = ba.transform.position;
        }
    }
    protected void RunAway()
    {
        BaseAgent ba = faae.NearestEnemy();
        if (!ba) return;

        Vector3 enemyPos = ba.transform.position;
        Vector3 offset = enemyPos - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < runAwayDistance * runAwayDistance)
        {
            var heading = -enemyPos + transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.

            agent.destination = transform.position + direction * runAwayDistance;
        }
    }
    protected void Fight()
    {
        if (target)
        {
            destination = target.transform;
            transform.LookAt(target.transform.position);
        }
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Patrol()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
    bool CanSee(GameObject tar)
    {
        RaycastHit hit;
        Vector3 dir = tar.transform.position - eyes.position;
        if (Physics.Raycast(eyes.position, dir.normalized, out hit, Mathf.Infinity, mask.value))
        {
            if (hit.collider.gameObject == tar)
            {
                Debug.DrawLine(eyes.position, hit.point, Color.blue);
                return true;
            }
            return false;
        }
        return false;
    }
}
