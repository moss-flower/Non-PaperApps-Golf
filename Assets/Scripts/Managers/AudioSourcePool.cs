using Models;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class AudioSourcePool : MonoBehaviour
    {
        [SerializeField] private SoundPlayer prefab;
        [SerializeField] private int capacity = 8;
        
        private ObjectPool<SoundPlayer> pool;

        private void Awake()
        {
            pool = new ObjectPool<SoundPlayer>(Spawn, OnGet, OnReturn, OnDestroyEmitter, true, capacity, 100);
        }

        public SoundPlayer Get()
        {
            return pool.Get();
        }

        public void Release(SoundPlayer player)
        {
            pool.Release(player);
        }

        private SoundPlayer Spawn()
        {
            var emitter = Instantiate(prefab, transform);
            return emitter;
        }

        private void OnGet(SoundPlayer player)
        {
            player.gameObject.SetActive(true);
        }
        
        private void OnReturn(SoundPlayer player)
        {
            player.gameObject.SetActive(false);
        }

        private void OnDestroyEmitter(SoundPlayer player)
        {
            Destroy(player.gameObject);
        }

    }
}