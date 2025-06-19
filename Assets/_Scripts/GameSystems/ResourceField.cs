using Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystems
{
    public class ResourceField
    {
        private List<ResourceDeposit> resourceDeposits = new List<ResourceDeposit>();

        public void AddResourcePoint(ResourceDeposit resourcePoint) =>
            resourceDeposits.Add(resourcePoint);

        public void RemoveResourcePoint(ResourceDeposit resourcePoint) =>
            resourceDeposits.Remove(resourcePoint);

        public int GetCount() =>
            resourceDeposits.Count;

        public ResourceDeposit GetClosestRes(Vector3 point)
        {
            if (resourceDeposits.Count == 0 )
            {
                return null;
            }

            SortedDictionary<float, ResourceDeposit> distances = new SortedDictionary<float, ResourceDeposit>();

            for (int i = 0; i < resourceDeposits.Count; i++)
            {
                distances.Add(Vector3.Distance(point, resourceDeposits[i].transform.position), resourceDeposits[i]);
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
