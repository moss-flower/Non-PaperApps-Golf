using TMPro;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Tool built to animate TMP objects.
    /// </summary>
    /// <remarks>Unfinished.</remarks>
    public class TMPUtility : MonoBehaviour
    {
        private TextMeshPro m_textMeshPro;
        private TextContainer m_textContainer;
    
        private TMP_TextInfo m_textInfo;

        private string textLabel = "Getting started with TEXT MESH PRO";

        private struct VertexAnim
        {
            public float angleRange;
            public float angle;
            public float speed;
        }

        private void Awake()
        {
            m_textMeshPro = gameObject.AddComponent<TextMeshPro>();
            m_textMeshPro.text = textLabel;
            m_textMeshPro.color = new Color32(255, 255, 255, 255);
            m_textMeshPro.alignment = TextAlignmentOptions.Center;
        
            m_textContainer = GetComponent<TextContainer>();
            m_textContainer.width = 40f;
        
            m_textMeshPro.ForceMeshUpdate();
        }

        public void AnimateText()
        {
            Mesh mesh = m_textMeshPro.mesh;
            Vector3[] vertices = mesh.vertices;
        
            m_textInfo = m_textMeshPro.textInfo;
            int characterCount = m_textInfo.characterCount;

            for (int i = 0; i < characterCount; i++)
            {
                TMP_CharacterInfo characterInfo = m_textInfo.characterInfo[i];
            
            }

        }
    
    
    }
}
