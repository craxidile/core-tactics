using System;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;

namespace Taiga.Core.Unity.MapInput
{

    public delegate void MapPointerUpdateDelegate(Vector2? position);

    public class MapGroundInput : MonoBehaviour
    {
        // EDIT: Pond
        public static bool enabled = true;
        
        new UnityEngine.Camera camera;
        int raycastMask;
        int groundLayer;

        public event MapPointerUpdateDelegate OnPointerUpdate;
        public event Action OnPointerTrigger;

        private void Start()
        {
            camera = UnityEngine.Camera.main;
            raycastMask = LayerMask.GetMask("Ground", "UI");
            groundLayer = LayerMask.NameToLayer("Ground");
        }

        Vector2 lastPointerPosition = Vector2.negativeInfinity;

        public Vector2 PointerPosition => lastPointerPosition;

        void Update()
        {
            UpdateMouseMove();
            UpdateMouseDown();
        }

        void UpdateMouseMove()
        {
            if (!enabled) return;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, hitInfo: out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask: raycastMask))
            {

                var gamePosition = hit.point.UnityToGamePosition();
                if (lastPointerPosition != gamePosition)
                {
                    OnPointerUpdate?.Invoke(gamePosition);
                    lastPointerPosition = gamePosition;
                }
            }
            else
            {
                OnPointerUpdate?.Invoke(null);
            }
        }

        void UpdateMouseDown()
        {
            if (!enabled) return;
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, hitInfo: out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask: raycastMask))
                {
                    if (hit.collider.gameObject.layer == groundLayer)
                    {
                        OnPointerTrigger?.Invoke();
                    }
                }
            }
        }
    }
}
