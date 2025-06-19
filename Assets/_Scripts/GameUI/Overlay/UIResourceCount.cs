using Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class UIResourceCount : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private TextMeshProUGUI textBox;
        [SerializeField] private ResourceStorage resourceStorage;

        private void Start()
        {
            resourceStorage.resourceLoaded += ResourceChanged;
        }

        private void Update()
        {
            textBox.transform.position = Camera.main.WorldToScreenPoint(resourceStorage.transform.position + offset);
        }

        private void ResourceChanged(int value)
        {
            textBox.text = value.ToString();
        }

        private void OnDestroy()
        {
            resourceStorage.resourceLoaded -= ResourceChanged;
        }
    }
}
