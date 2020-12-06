//#define TAKE_ACCOUNT_POINT_ENTITIES

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace SuperPivot
{
    internal class TargetWrapper
    {
        public enum Component { X=0, Y=1, Z=2 }
        public Transform transform { get; private set; }
        public string name { get { return transform.name; } }

        Vector3 m_CachedPosition;
        Quaternion m_CachedRotation;
        Vector3 m_CachedScale;


        public TargetWrapper(Transform t)
        {
            Debug.Assert(t != null);
            transform = t;
            UpdateTargetCachedData();
        }

        public void UpdateTargetCachedData()
        {
            Debug.Assert(transform);
            m_CachedPosition = transform.position;
            m_CachedRotation = transform.rotation;
            m_CachedScale = transform.localScale;
        }

        public bool TargetTransformHasChanged()
        {
            Debug.Assert(transform);
            return transform.position != m_CachedPosition
                || transform.rotation != m_CachedRotation
                || transform.localScale != m_CachedScale;
        }

        public void SetPivot(Vector3 pivotPos, API.Space space)
        {
            API.SetPivot(transform, pivotPos, space);
        }

        public void SetPivot(Component comp, float value, API.Space space)
        {
            Debug.Assert(transform, "Invalid target entity");
            var pivotPos = transform.GetPivotPosition(space);
            pivotPos[(int)comp] = value;
            API.SetPivot(transform, pivotPos, space);
        }

        static float InverseLerpUnclamped(float from, float to, float value)
        {
            if (from == to) return 0.5f;
            return (value - from) / (to - from);
        }

        static bool GUIButtonZero()
        {
            var buttonStyle = new GUIStyle(EditorStyles.miniButton);
            buttonStyle.fixedWidth = 40f;
            return GUILayout.Button("Zero", buttonStyle);
        }

        public void GUIWorldPosition()
        {
            EditorGUILayout.LabelField("World Position", EditorStyles.boldLabel);

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                var newPos = EditorGUILayout.Vector3Field("", transform.position);
                if (EditorGUI.EndChangeCheck())
                    SetPivot(newPos, API.Space.Global);

                if (GUIButtonZero())
                    SetPivot(Vector3.zero, API.Space.Global);
            }
        }

        public void GUILocalPosition()
        {
            if (transform.parent)
            {
                EditorGUILayout.LabelField(string.Format("Local Position (relative to '{0}')", transform.parent.name), EditorStyles.boldLabel);
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUI.BeginChangeCheck();
                    var newPos = EditorGUILayout.Vector3Field("", transform.localPosition);
                    if (EditorGUI.EndChangeCheck())
                        SetPivot(newPos, API.Space.Local);

                    if (GUIButtonZero())
                        SetPivot(Vector3.zero, API.Space.Local);
                }
            }
        }
    }
}
