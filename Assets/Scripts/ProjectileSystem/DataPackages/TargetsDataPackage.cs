using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void.ProjectileSystem.DataPackages
{
    public class TargetsDataPackage : ProjectileDataPackage
    {
       // The list of transforms that the Targeter weapon component detected
        public List<Transform> targets;
    }
}