using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] private ParticleSystem resourceParticles;

        [SerializeField] private int resourceCount;

        public event Action<int> resourceLoaded;

        public void DropResources()
        {
            resourceParticles.Play();
            resourceCount++;
            resourceLoaded?.Invoke(resourceCount);
        }
    }
}
