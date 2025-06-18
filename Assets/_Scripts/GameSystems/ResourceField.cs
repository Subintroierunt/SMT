using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (resourcePoints.Count == 0 )
            {
                return null;
            }

            SortedDictionary<float, ResourcePoint> distances = new SortedDictionary<float, ResourcePoint>();

            for (int i = 0; i < resourcePoints.Count; i++)
            {
                distances.Add(Vector3.Distance(point, resourcePoints[i].transform.position), resourcePoints[i]);
            }

            for (int i = 0; i < distances.Count; i++)
            {
                if (!distances.ElementAt(i).Value.IsLocked)
                {
                    return distances.ElementAt(i).Value;
                }
            }

            return null;
        }
    }
}
