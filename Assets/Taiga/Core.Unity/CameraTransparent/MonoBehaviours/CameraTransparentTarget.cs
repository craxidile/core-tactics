using System;
using DG.Tweening;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.Unity.CameraTransparent
{

    public class CameraTransparentTarget : MonoBehaviour
    {

        public GameObject mesh;

        private void OnTriggerEnter(Collider other)
        {
            try
            {
                var cameraTransparent = other
                    .attachedRigidbody
                    .GetComponent<CameraTransparent>();

                if (cameraTransparent != null)
                {
                    mesh.SetActive(false);
                }
            } catch (Exception ex)
            {
                Debug.Log(">>on_trigger_enter_error<< " + ex.Message + ", " + other + ", " + other.attachedRigidbody);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null || other.attachedRigidbody == null) return;
            var cameraTransparent = other
                .attachedRigidbody
                .GetComponent<CameraTransparent>();

            if (cameraTransparent != null)
            {
                mesh.SetActive(true);
            }
        }

    }
}
