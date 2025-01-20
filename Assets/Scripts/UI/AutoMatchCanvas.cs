using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Void.UI
{
    public class AutoMatchCanvas : MonoBehaviour
    {
         private int _defaultWidth = 1920;
         private int _defaultHeight = 1080;

        private void Awake()
        {
            float currentRatio = (float)Screen.width / Screen.height;
            float defaultRatio = (float)_defaultWidth / _defaultHeight;
            if (currentRatio > defaultRatio)
            {
                GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
            else
            {
                GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
        }
    }
}
