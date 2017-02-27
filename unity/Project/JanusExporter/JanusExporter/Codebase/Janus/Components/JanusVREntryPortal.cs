﻿#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JanusVR
{
    public class JanusVREntryPortal : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private bool circular = false;

        public bool Circular
        {
            get { return circular; }
            set
            {
                if (circular != value)
                {
                    MeshRenderer renderer = this.GetComponent<MeshRenderer>();
                    MeshFilter filter = this.GetComponent<MeshFilter>();
                    filter.sharedMesh = JanusResources.PlaneMesh;

                    if (value)
                    {
                        filter.sharedMesh = JanusResources.CylinderBaseMesh;
                    }
                    else
                    {
                        filter.sharedMesh = JanusResources.PlaneMesh;
                    }
                }
                circular = value;
            }
        }

        public Vector3 GetJanusPosition()
        {
            // offset the position so it fits on the main app
            Vector3 position = transform.position;
            Vector3 scale = transform.localScale;
            Vector3 halfScale = scale / 2.0f;

            Vector3 bot = position - (transform.up * halfScale.y);
            return bot;
        }

        private void OnDrawGizmos()
        {
            Vector3 pos = transform.position;
            Vector3 scale = transform.localScale;
            Vector3 halfScale = scale / 2.0f;

            float size = scale.magnitude / 5.0f;

            Gizmos.color = Color.red;

            Vector3 target = pos + (transform.forward * size);
            Vector3 targetArrow = pos + (transform.forward * size * 0.85f);
            Vector3 up = transform.up * 0.1f;
            Vector3 right = transform.right * 0.1f;

            if (circular)
            {

            }
            else
            {
                Vector3 topLeft = pos + (transform.up * halfScale.y) + (transform.right * halfScale.x);
                Vector3 botLeft = pos - (transform.up * halfScale.y) + (transform.right * halfScale.x);
                Vector3 topRight = pos + (transform.up * halfScale.y) - (transform.right * halfScale.x);
                Vector3 botRight = pos - (transform.up * halfScale.y) - (transform.right * halfScale.x);

                Gizmos.DrawLine(topLeft, topRight);
                Gizmos.DrawLine(topRight, botRight);
                Gizmos.DrawLine(botRight, botLeft);
                Gizmos.DrawLine(botLeft, topLeft);
            }

            Gizmos.DrawLine(pos, target);
            Gizmos.DrawLine(target, targetArrow + up);
            Gizmos.DrawLine(target, targetArrow - up);
            Gizmos.DrawLine(target, targetArrow + right);
            Gizmos.DrawLine(target, targetArrow - right);
        }


        [MenuItem("GameObject/JanusVR/Room Entry Portal")]
        private static void CreateEntryPortal()
        {
            GameObject go = new GameObject("Room Entry Portal");
            go.transform.localScale = new Vector3(1.8f, 2.5f, 1);

            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            MeshFilter filter = go.AddComponent<MeshFilter>();

            renderer.receiveShadows = false;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            filter.sharedMesh = JanusResources.PlaneMesh;

            Material mat = Material.Instantiate(AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat"));
            renderer.sharedMaterial = mat;

            JanusVREntryPortal portal = go.AddComponent<JanusVREntryPortal>();
        }
    }
}
#endif