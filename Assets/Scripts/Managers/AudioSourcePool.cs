using Models;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class AudioSourcePool : MonoBehaviour
    {
        [SerializeField] private SoundEmitter prefab;
        [SerializeField] private int capacity = 8;
        
        private ObjectPool<SoundEmitter> pool;

        private void Awake()
        {
            pool = new ObjectPool<SoundEmitter>(Spawn, OnGet, OnReturn, OnDestroyEmitter, true, capacity, 100);
        }

        public SoundEmitter Get()
        {
            return pool.Get();
        }

        public void Release(SoundEmitter emitter)
        {
            pool.Release(emitter);
        }

        private SoundEmitter Spawn()
        {
            var emitter = Instantiate(prefab, transform);
            return emitter;
        }

        private void OnGet(SoundEmitter emitter)
        {
            emitter.gameObject.SetActive(true);
        }
        
        private void OnReturn(SoundEmitter emitter)
        {
            emitter.gameObject.SetActive(false);
        }

        private void OnDestroyEmitter(SoundEmitter emitter)
        {
            Destroy(emitter.gameObject);
        }

    }
}