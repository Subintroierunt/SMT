using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
    public class ResourceField : MonoBehaviour
    {
        private List<ResourcePoint> resourcePoints = new List<ResourcePoint>();

        public void AddResourcePoint(ResourcePoint resourcePoint) =>
            resourcePoints.Add(resourcePoint);

        public void RemoveResourcePoint(ResourcePoint resourcePoint) =>
            resourcePoints.Remove(resourcePoint);

        public int GetCount() =>
            resourcePoints.Count;

        public ResourcePoint GetClosestRes(Vector3 point)
        {
            if (resourcePoints.Count == 0)
            {
                return null;
            }

            float distance = Mathf.Infinity;
            int number = 0;

            for (int i = 0; i < resourcePoints.Count; i++)
            {
                if (Vector3.Distance(point, resourcePoints[i].transform.position) < distance)
                {
                    distance = Vector3.Distance(point, resourcePoints[i].transform.position);
                    number = i;
                }
            }
            return resourcePoints[number];
        }

        
    }
}
