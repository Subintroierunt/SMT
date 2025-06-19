using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class DroneAI : MonoBehaviour
    {
        [SerializeField] private ResourceEmmiter resourceEmmiter;
        [SerializeField] private ResourceStorage resourceStorage;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private DroneDrawPath drawPath;

        private ResourceField resourceField;
        private DroneState droneState;
        private float miningTime = 2f;
        private ResourceDeposit resourcePoint;
        private bool hasResource;

        private ResourceDeposit curTarget;

        public void Init(ResourceEmmiter emmiter, ResourceStorage storage)
        {
            resourceEmmiter = emmiter;
            resourceStorage = storage;
        }

        public void SwitchDrawPath(bool value) =>
            drawPath.SetDraw(value);

        private void Update()
        {
            if (resourceField == null)
            {
                resourceField = resourceEmmiter.ResourceField;
            }

            switch (droneState)
            {
                case DroneState.idle:
                    ResourceDeposit target = resourceField.GetClosestRes(transform.position);
                    if (target != null)
                    {
                        agent.SetDestination(target.transform.position + ((transform.position - target.transform.position).normalized * 0.1f));
                        ChangeState(DroneState.move);
                        curTarget = target;
                        agent.isStopped = false;
                    }
                    else
                    {
                        agent.SetDestination(resourceStorage.transform.position);
                    }
                    break;
                case DroneState.move:
                    if (agent.remainingDistance <= 1f)
                    {
                        if (hasResource)
                        {
                            resourceStorage.DropResources();
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

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }

        private void ChangeState(DroneState newState)
        {
            droneState = newState;
        }

        private IEnumerator Mining()
        {
            yield return new WaitForSeconds(miningTime);
            curTarget.Mined();
            agent.isStopped = false;
            agent.SetDestination(resourceStorage.transform.position);
            hasResource = true;
            ChangeState(DroneState.move);
        }
    }
}
