using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputSettings;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float zoomSpeed = 1f;
        [SerializeField] private float minSize = 1f;
        [SerializeField] private float maxSize = 10f;
        private InputAction zoom;

        private void Awake()
        {
            zoom = inputSettings.FindActionMap("Player").FindAction("Zoom");
        }

        private void Update()
        {
            var delta = zoom.ReadValue<Single>();
            // var delta = InputSystem.GetDevice<Mouse>().scroll.ReadValue().y;
            SetSize(mainCamera.orthographicSize + delta *zoomSpeed* Time.fixedDeltaTime);
        }

        private void SetSize(float value)
        {
            if(value<minSize||value>maxSize) return;
            mainCamera.orthographicSize = value;
        }
    }
}