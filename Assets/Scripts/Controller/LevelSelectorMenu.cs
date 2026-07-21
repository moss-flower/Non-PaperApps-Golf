using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    /// <summary>
    /// A class used to handle the logic of Level Selection from the Level Selection Menu.
    /// </summary>
    public class LevelSelectorMenu : Menu
    {
    
        [SerializeField] private GameObject levelButtonPrefab;
        [SerializeField] private GameObject viewport;
        [SerializeField] private GameObject levelListParent;
        [SerializeField] private ScrollRect scrollRect;
    
        [SerializeField] private LevelManager levelManager;
    
        private int selectedLevel = -1;

        /// <summary>
        /// The menu queries the level manager and populates with available data.
        /// </summary>
        private void OnEnable()
        {
            if (levelManager == null)
            {
                print("[LevelSelectorScreen.cs] HEY, ASSIGN THE LEVEL MANAGER!");
                return;
            }

            var levels = levelManager.GetMapData();
            viewport.SetActive(true);
            if (levels == null)
            {
                return;
            }
            GenerateLevelList(levels);
        }

        private void OnDisable()
        {;
            DestroyChildren();
            viewport.SetActive(false);
            selectedLevel = -1;
        }

        
        private void SelectLevel(int level)
        {
            selectedLevel = level;
        }
        
        public void OnConfirmedClicked()
        {
            if (selectedLevel == -1)
            {
                return;
            }
            levelManager.SelectLevel(selectedLevel);
        }

        private void DestroyChildren()
        {
            if (levelListParent == null) return;
            foreach (Transform child in levelListParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Method for generating and instantiating Game objects based on Level/Board information for the game.
        /// </summary>
        /// <param name="boards">A list of boards from the <see cref="levelManager"/>.</param>
        /// <exception cref="NullReferenceException">Thrown when a prefab is missing from the editor window.</exception>
        private void GenerateLevelList(List<BoardInfo> boards)
        {
        
            DestroyChildren();
            if (levelButtonPrefab == null)
            {
                throw new NullReferenceException("Missing: LevelButtonPrefab");
            }
        
            if (boards.Count == 0)
            {
                return;
            }

            for (int i = 0; i < boards.Count; i++)
            {
                GameObject levelButton = Instantiate(levelButtonPrefab, levelListParent.transform);
                LevelSelectButton button = levelButton.GetComponent<LevelSelectButton>();
                button.InitializeButton(boards[i].name, i, this);
            }

            scrollRect.content = levelListParent.GetComponent<RectTransform>();
            if (scrollRect != null)
            {
                StartCoroutine(ResetScrollPosition());
            }
        
        }

        public void OnClick(int level)
        {
            SelectLevel(level);
        }
    
        IEnumerator ResetScrollPosition()
        {
            // Wait one frame to ensure Content Size Fitter adjusts layout properly
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
}

