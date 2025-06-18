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
                        agent.SetDestination(target.transform.position);
                        ChangeState(DroneState.move);
                        curTarget = target;
                    }
                    break;
                case DroneState.move:
                    if (agent.remainingDistance <= 0.3f)
                    {
                        if (hasResource)
                        {
                            //Base trigger
                            hasResource = false;
                            ChangeState(DroneState.idle);
                        }
                        else
                        {
                            ChangeState(DroneState.mine);
                            StartCoroutine(Mining());
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
            Debug.Log(droneState);
        }

        private IEnumerator Mining()
        {
            yield return new WaitForSeconds(miningTime);
            curTarget.Mining();
            agent.SetDestination(Vector3.zero); //change to base location
            hasResource = true;
            ChangeState(DroneState.move);
        }
    }
}
