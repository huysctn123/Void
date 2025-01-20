using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void
{
    public class particleController : MonoBehaviour
    {
       private void FinishAnim()
        {
            Destroy(gameObject);
        }
    }
}
