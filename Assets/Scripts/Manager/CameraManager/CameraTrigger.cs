using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Void.CameraSystem
{
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera newCamera;
        private void OnTriggerStay2D(Collider2D _other)
        {
            if (_other.CompareTag("Player"))
            {
                CameraManager.Instance.SwapCamera(newCamera);
            }
        }
    }
}