using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class DroneAI : MonoBehaviour
    {
        [SerializeField] private ResourceField resourceField;
        [SerializeField] private NavMeshAgent agent;

        private DroneState droneState;
        private float miningTime = 2f;
        private ResourcePoint resourcePoint;
        private bool hasResource;

        private ResourcePoint curTarget;

        public void Init(ResourceField resourceField)
        {
            this.resourceField = resourceField;
        }

        private void Update()
        {
            switch (droneState)
            {
                case DroneState.idle:
                    ResourcePoint target = resourceField.GetClosestRes(transform.position);
                    if (target != null)
                    {
                        agent.SetDestination(target.transform.position + ((transform.position - target.transform.position).normalized * 0.1f));
                        ChangeState(DroneState.move);
                        curTarget = target;
                        Debug.Log(target.transform.position + " ");
                        agent.isStopped = false;
                    }
                    break;
                case DroneState.move:
                    if (agent.remainingDistance <= 1f)
                    {
                        if (hasResource)
                        {
                            //Base trigger
                            hasResource = false;
                            ChangeState(DroneState.idle);
                        }
                        else
                        {
                            if (!curTarget.IsLocked)
                            {
                                curTarget.Lock();
                                agent.isStopped = true;
                                ChangeState(DroneState.mine);
                                StartCoroutine(Mining());
                            }
                            else
                            {
                                ChangeState(DroneState.idle);
                            }
                        }
                    }
                    break;
                case DroneState.mine:
                    break;
            }
        }

        private void ChangeState(DroneState newState)
        {
            droneState = newState;
        }

        private IEnumerator Mining()
        {
            yield return new WaitForSeconds(miningTime);
            curTarget.Mining();
            agent.isStopped = false;
            agent.SetDestination(Vector3.zero); //change to base location
            hasResource = true;
            ChangeState(DroneState.move);
        }
    }
}
