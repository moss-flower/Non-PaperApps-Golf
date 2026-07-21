using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Builders
{
    public class ArrowBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        private GameObject parentObject;
        
        private bool isInitialized;
        
        private List<GameObject> arrows;

        private void Awake()
        {
            arrows = new List<GameObject>();
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        public void Initialize()
        {
            parentObject = new GameObject();
            parentObject.transform.SetParent(transform);
            isInitialized = true;
        }
        
        public void AddArrow(Vector3 pointA, Vector3 pointB)
        {
            GameObject arrowGameObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity, parentObject.transform);
            arrows.Add(arrowGameObject);
            ArrowGenerator arrowGenerator = arrowGameObject.GetComponent<ArrowGenerator>();
            arrowGenerator.OnCreate();
            arrowGenerator.GenerateArrow(pointA, pointB);
        }

        public void RemoveArrows()
        {
            /*while (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }*/
            
            Destroy(parentObject);
            isInitialized = false;
        }
    }
}