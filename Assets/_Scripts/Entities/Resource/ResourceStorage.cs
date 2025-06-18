using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] private ParticleSystem resourceParticles;

        [SerializeField] private int resourceCount;

        public void DropResources()
        {
            resourceParticles.Play();
            resourceCount++;
        }
    }
}
