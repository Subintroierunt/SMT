using Entities;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace GameSystems
{
    public class ResourceEmmiter : MonoBehaviour
    {
        [SerializeField] private ResourceDeposit prefab;

        private ResourceField resourceField;
        
        [SerializeField] private Vector2 emmiterSize = new Vector2(8,8);
        [SerializeField] private float emmiterInterval = 5f;
        [SerializeField] private int emitterMaxEntities = 5;

        private List<Vector2Int> emitterPoints;
        private List<ResourceDeposit> resourceDeposits = new List<ResourceDeposit>();

        public ResourceField ResourceField =>
            resourceField;

        private void Start()
        {
            resourceField = new ResourceField();
            emitterPoints = new List<Vector2Int>();

            for (int i = 0; i < emmiterSize.y; i++)
            {
                for (int j = 0; j < emmiterSize.x; j++)
                {
                    emitterPoints.Add(new Vector2Int(j, i));
                }
            }

            for (int i = 0; i < emitterMaxEntities; i++)
            {
                ResourceDeposit resPoint = Instantiate(prefab, transform);
                resPoint.gameObject.SetActive(false);
                resourceDeposits.Add(resPoint);
            }
        }

        private void Update()
        {
            if (Time.time % emmiterInterval < Time.deltaTime)
            {
                Emmit();
            }
        }

        public void SetInterval(float interval) =>
            emmiterInterval = interval;

        private void Emmit()
        {
            if (resourceField.GetCount() == emitterMaxEntities) 
            {
                return;
            }
            int rand = Mathf.RoundToInt(Random.Range(0, emitterPoints.Count));
            
            ResourceDeposit resPoint = resourceDeposits[0];
            resPoint.Restore(emitterPoints[rand]);
            resPoint.Depleted += ResourcePointDepleted;
            emitterPoints.RemoveAt(rand);
            resourceDeposits.RemoveAt(0);
            resourceField.AddResourcePoint(resPoint);
        }

        private void ResourcePointDepleted(ResourceDeposit resPoint, Vector2Int coords)
        {
            resourceDeposits.Add(resPoint);
            emitterPoints.Add(coords);
            resourceField.RemoveResourcePoint(resPoint);
            resPoint.Depleted -= ResourcePointDepleted;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1,0,0,0.25f);
            Gizmos.DrawCube(transform.position + (new Vector3(emmiterSize.x / 2, 0, emmiterSize.y / 2)), new Vector3(emmiterSize.x, 0, emmiterSize.y));
        }
    }
}
