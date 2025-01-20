using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Void.ObjectPoolSystem;

namespace Void.Interfaces
{
    public interface IObjectPoolItem 
    {

        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;
        void Release();
    }
}