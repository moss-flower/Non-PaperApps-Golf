using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Builders
{
    /// <summary>
    /// A class used for building arrow objects in the game view.
    /// </summary>
    public class ArrowBuilder : MonoBehaviour
    {
        //------------------------------ Parameters ------------------------------
        // Game objects
        [SerializeField] private GameObject arrowPrefab;
        private GameObject parentObject;
        
        // Booleans
        private bool isInitialized;
        
        // List
        private List<GameObject> arrows;

        
        // ------------------------------------ Methods ----------------------------
        private void Awake()
        {
            arrows = new List<GameObject>();
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        /// <summary>
        /// Method used to create a new parent game objects when the builder is loaded.
        /// </summary>
        public void Initialize()
        {
            parentObject = new GameObject();
            parentObject.transform.SetParent(transform);
            isInitialized = true;
        }
        
        /// <summary>
        /// Method used for constructing a new Arrow as a mesh in the game world.
        /// </summary>
        /// <param name="pointA">The position in the game world where the arrow should start.</param>
        /// <param name="pointB">The Position in the game world where the arrow should end.</param>
        public void AddArrow(Vector3 pointA, Vector3 pointB)
        {
            GameObject arrowGameObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity, parentObject.transform);
            arrows.Add(arrowGameObject);
            ArrowGenerator arrowGenerator = arrowGameObject.GetComponent<ArrowGenerator>();
            arrowGenerator.OnCreate();
            arrowGenerator.GenerateArrow(pointA, pointB);
        }

        /// <summary>
        /// Method used to remove all arrows from the board.
        /// </summary>
        public void RemoveArrows()
        {
            Destroy(parentObject);
            isInitialized = false;
        }
    }
}