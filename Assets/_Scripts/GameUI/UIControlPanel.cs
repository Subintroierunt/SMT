using Entities;
using GameSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class UIControlPanel : MonoBehaviour
    {
        [SerializeField] private DroneFactory redDrones;
        [SerializeField] private DroneFactory blueDrones;
        [SerializeField] private ResourceEmmiter resourceEmmiter;

        [SerializeField] private Slider droneCount;
        [SerializeField] private TextMeshProUGUI textCount;
        [SerializeField] private Slider droneSpeed;
        [SerializeField] private TextMeshProUGUI textSpeed;
        [SerializeField] private TMP_InputField emmitInterval;
        [SerializeField] private Toggle drawPaths;

        private void Start()
        {
            droneCount.onValueChanged.AddListener(redDrones.SetActiveDroneCount);
            droneCount.onValueChanged.AddListener(blueDrones.SetActiveDroneCount);
            droneCount.onValueChanged.AddListener(CountChanged);
            

            droneSpeed.onValueChanged.AddListener(redDrones.SetDroneSpeed);
            droneSpeed.onValueChanged.AddListener(blueDrones.SetDroneSpeed);
            droneSpeed.onValueChanged.AddListener(SpeedChanged);

            drawPaths.onValueChanged.AddListener(redDrones.SetDroneDrawPath);
            drawPaths.onValueChanged.AddListener(blueDrones.SetDroneDrawPath);

            emmitInterval.onValueChanged.AddListener(IntervalChanged);
        }

        private void CountChanged(float count)
        {
            textCount.text = "Drone count " + Mathf.RoundToInt(count);
        }

        private void SpeedChanged(float speed)
        {
            textSpeed.text = "Drone speed " + Mathf.RoundToInt(speed);
        }

        private void IntervalChanged(string value)
        {
            resourceEmmiter.SetInterval(float.Parse(value));
        }

        private void OnDestroy()
        {
            droneCount.onValueChanged.RemoveAllListeners();

            droneSpeed.onValueChanged.RemoveAllListeners();

            drawPaths.onValueChanged.RemoveAllListeners();
        }
    }
}
