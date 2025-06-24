using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TaterGame.SnowIsland.Shared
{
    public class ModelFlicker: MonoBehaviour
    {
        public Color FlickColor = new Color(1, 0.3f, 0.3f, 1);

        private Dictionary<MeshRenderer, Material> m_OriginMaterials = new();
        private Dictionary<MeshRenderer, Material> m_Materials;
        [SerializeField]
        private float m_Duration = 0.125f;
        [ShowInInspector,ReadOnly]
        private bool m_CanFlick = true;

        private void Start()
        {
            RefreshModel();
        }
        
        public void RefreshModel()
        {
            if (m_Materials != null)
            {
                foreach (var keyValuePair in m_Materials)
                {
                    Object.Destroy(keyValuePair.Value);
                }
                m_Materials.Clear();
                m_Materials = null;
                foreach (var keyValuePair in m_OriginMaterials)
                {
                    if (keyValuePair.Key == null)
                    {
                        continue;
                    }
                    keyValuePair.Key.material = keyValuePair.Value;
                }
                m_OriginMaterials.Clear();
            }

            foreach (var componentsInChild in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m_OriginMaterials[componentsInChild] = componentsInChild.material;
            }

            m_Materials = new Dictionary<MeshRenderer, Material>();
            foreach (var keyValuePair in m_OriginMaterials)
            {
                var keyMaterial = Instantiate(keyValuePair.Value);
                keyValuePair.Key.material = keyMaterial;
                m_Materials[keyValuePair.Key] = keyMaterial;
            }
        }
        
        public async void Flicker()
        {
            if (!m_CanFlick||m_Materials==null)
            {
                return;
            }

            m_CanFlick = false;
            var rootSeq = Sequence.Create();
            var turnRedSeq = Sequence.Create();
            foreach (var materialsValue in m_Materials.Values)
            {
                var tween = Tween.MaterialColor(materialsValue, FlickColor, m_Duration);
                turnRedSeq.Group(tween);
            }
            var turnWhiteSeq = Sequence.Create();
            foreach (var materialsValue in m_Materials.Values)
            {
                var tween = Tween.MaterialColor(materialsValue, Color.white, m_Duration);
                turnWhiteSeq.Group(tween);
            }
            rootSeq.Chain(turnRedSeq).Chain(turnWhiteSeq);
            await rootSeq;
            m_CanFlick = true;
        }

#if UNITY_EDITOR
        [Button]
        public void FlickerEditor()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            Flicker();
        }
#endif
    }
}