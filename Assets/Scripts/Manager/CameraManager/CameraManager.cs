using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Void.Manager;

namespace Void.CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera[] allVirtualCameras;
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;

        [Header("y Damping Settings for Player Jump/Fall: ")]
        [SerializeField] private float panAmount = 0.1f;
        [SerializeField] private float panTime = 0.2f;
        public float playerFallSpeedTheshold = -10f;
        public bool isLerpingYDamping;
        public bool hasLerpingYDamping;

        public float normalYDamp;

        public static CameraManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            for (int i = 0; i < allVirtualCameras.Length; i++)
            {
                if (allVirtualCameras[i].enabled)
                {
                    this.currentCamera = allVirtualCameras[i];
                    allVirtualCameras[i].Follow = null;
                    GetframingTransposer(currentCamera);
                    return;
                }
            }
            framingTransposer.m_YDamping = normalYDamp;
        }

        private void GetframingTransposer(CinemachineVirtualCamera currentCamera)
        {
            if (!currentCamera) return;
            this.framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        private Transform GetPlayerPos()
        {
            Transform player = Player.Instance.transform;
            return player;
        }
        public void SwapCamera(CinemachineVirtualCamera _newCam)
        {
            currentCamera.enabled = false;
            currentCamera = _newCam;
            currentCamera.Follow = GetPlayerPos();
            currentCamera.enabled = true;
        }

        public IEnumerator LerpYDamping(bool _isPlayerFalling)
        {
            isLerpingYDamping = true;
            // take start y damp amount
            float _startYDamp = framingTransposer.m_YDamping;
            float _endYDamp = normalYDamp;
            //determine end damp amount
            if (_isPlayerFalling)
            {
                _endYDamp = panAmount;
                hasLerpingYDamping = true;
            }
            else
            {
                _endYDamp = normalYDamp;
            }
            //lerp panAmount
            float _timer = 0f;
            while (_timer < panTime)
            {
                _timer += Time.deltaTime;
                float _lerpPanAmount = Mathf.Lerp(_startYDamp, _endYDamp, (_timer / panTime));
                framingTransposer.m_YDamping = _lerpPanAmount;
                yield return null;
            }
            isLerpingYDamping = false;
        }
    }
}
