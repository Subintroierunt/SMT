using Entities;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace GameSystems
{
    public class ResourceEmmiter : MonoBehaviour
    {
        [SerializeField] private ResourcePoint prefab;

        [SerializeField] private ResourceField resourceField;
        
        [SerializeField] private Vector2 emmiterSize = new Vector2(8,8);
        [SerializeField] private float emmiterInterval = 5f;
        [SerializeField] private int emitterMaxEntities = 5;

        private List<Vector2Int> emitterPoints;
        private List<ResourcePoint> resourcePoints = new List<ResourcePoint>();

        private void Start()
        {
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
                ResourcePoint resPoint = Instantiate(prefab, transform);
                resPoint.gameObject.SetActive(false);
                resourcePoints.Add(resPoint);
            }
        }

        private void Update()
        {
            if (Time.time % emmiterInterval < Time.deltaTime)
            {
                Emmit();
            }
        }

        private void Emmit()
        {
            if (resourceField.GetCount() == emitterMaxEntities) 
            {
                return;
            }
            int rand = Mathf.RoundToInt(Random.Range(0, emitterPoints.Count));
            
            ResourcePoint resPoint = resourcePoints[0];
            resPoint.Restore(emitterPoints[rand]);
            resPoint.Depleted += ResourcePointDepleted;
            emitterPoints.RemoveAt(rand);
            resourcePoints.RemoveAt(0);
            resourceField.AddResourcePoint(resPoint);
        }

        private void ResourcePointDepleted(ResourcePoint resPoint, Vector2Int coords)
        {
            resourcePoints.Add(resPoint);
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
