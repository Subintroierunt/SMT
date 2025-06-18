using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ResourcePoint : MonoBehaviour
    {
        public event Action<ResourcePoint, Vector2Int> Depleted;

        private bool isDepleted;
        private bool isLocked;

        private Vector2Int coords;

        public bool IsLocked =>
            isLocked;

        public void Lock() =>
            isLocked = true;

        public void Mining()
        {
            Depleted.Invoke(this, coords);
            isDepleted = true;
            gameObject.SetActive(false);
        }

        public void Restore(Vector2Int newCoords)
        {
            coords = newCoords;
            transform.localPosition = (Vector3)(Vector2)coords;
            isDepleted = false;
            isLocked = false;
            gameObject.SetActive(true);
        }
    }
}
