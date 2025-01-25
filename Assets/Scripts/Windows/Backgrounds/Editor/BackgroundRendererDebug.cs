using System;
using UnityEditor;
using UnityEngine;

namespace Windows.Backgrounds.Editor
{
    public class BackgroundRendererDebug : EditorWindow
    {
        [MenuItem("EOS/Background")]
        private static void ShowWindow()
        {
            var window = GetWindow<BackgroundRendererDebug>();
            window.titleContent = new GUIContent("EOS | Background");
            window.Show();
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("not playing.", MessageType.Info);
                return;
            }

            foreach (var mat in BackgroundRenderer.BackgroundMaterials)
            {
                if (GUILayout.Button(mat.name))
                {
                    BackgroundRenderer.ChangeMat(mat);
                }
            }
        }
    }
}