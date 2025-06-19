using GameSystems;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class DroneFactory : MonoBehaviour
    {
        [SerializeField] private int startDrones = 3;
        [SerializeField] private DroneAI droneAI;
        [SerializeField] private ResourceEmmiter resourceEmmiter;
        [SerializeField] private ResourceStorage resourceStorage;

        private List<DroneAI> drones = new List<DroneAI>();
        private int activeDrones;
        private int maxActiveDrones = 5;

        private void Start()
        {
            for (int i = 0; i < maxActiveDrones; i++) 
            {
                DroneAI drone = Instantiate(droneAI, transform);
                drone.Init(resourceEmmiter, resourceStorage);
                drones.Add(drone);
                drone.gameObject.SetActive(false);
            }

            for (int i = 0; i < startDrones; i++)
            {
                Assemble();
            }
        }

        public void SetDroneSpeed(float speed) =>
            drones.ForEach(drone => { drone.SetSpeed(speed); });

        public void SetDroneDrawPath(bool value) =>
            drones.ForEach(drone => { drone.SwitchDrawPath(value); });

        public void SetActiveDroneCount(float count)
        {
            count = Mathf.RoundToInt(count);

            if (count < 0)
            {
                return;
            }

            if (count > maxActiveDrones)
            {
                count = maxActiveDrones;
            }

            while (activeDrones != count)
            {
                if (activeDrones < count)
                {
                    Assemble();
                }
                else
                {
                    Disassemble();
                }
            }
        }

        public void Assemble()
        {
            if (activeDrones < maxActiveDrones)
            {
                drones[activeDrones].transform.position = transform.position;
                drones[activeDrones].gameObject.SetActive(true);
                activeDrones++;
            }
        }

        public void Disassemble()
        {
            if (activeDrones > 0)
            {
                drones[activeDrones].gameObject.SetActive(false);
                activeDrones--;
            }
        }
    }
}
