using System;
using System.Linq;
using DigitalSalmon.C360;
using UnityEditor;
using UnityEngine;

namespace Assets.Complete360Tour.Runtime.Tests
{
    [CustomEditor(typeof(DigitalSalmon.C360.AutoTour))]
    public class AutoTourEditor : Editor
    {
        SerializedProperty _noteNames;
        SerializedProperty _noteNamesUkr;


 void OnEnable()
        {
            _noteNames = serializedObject.FindProperty("nodeNames");
            _noteNamesUkr = serializedObject.FindProperty("nodeTextUkr");
        }

        public override void OnInspectorGUI()
        {

            EditorGUILayout.LabelField("some");
            serializedObject.Update();
            base.OnInspectorGUI();
            if (EditorGUILayout.LinkButton("LoadAsset"))
            {

                DigitalSalmon.C360.Complete360Tour completeTour = (target as DigitalSalmon.C360.AutoTour)
                    .GetComponent<DigitalSalmon.C360.Complete360Tour>();

                if (_noteNames.arraySize > 0)
                    Debug.Log(
                        $"NodeNames contains {_noteNames.arraySize} please reduce it's size in order to auto load from aSset");
                else
                {
 
                    var text = completeTour.tourData.text;
                    var tour = TourConverter.ParseTour(text);

                    for (int i = 0; i < tour.NodeDataList.Count; i++)
                    {
                        _noteNames.InsertArrayElementAtIndex(i);
                        var element = _noteNames.GetArrayElementAtIndex(i);
                        element.stringValue = tour.NodeDataList[i].NiceName;

                    }
                }
                if (_noteNamesUkr.arraySize > 0)
                    Debug.Log( $"NodeNames contains {_noteNames.arraySize} please reduce it's size in order to auto load from aSset");
                else
                {
 
                    var text = completeTour.tourDataNames.text;
                    var lines = text.Split( new[] { Environment.NewLine }, StringSplitOptions.None
                    ); 
                    Debug.Log($"We got {lines.Length} from tourDataNames" );

                    for (int i = 0; i < _noteNames.arraySize; i++)
                    {
                        _noteNamesUkr.InsertArrayElementAtIndex(i);
                        var element = _noteNamesUkr.GetArrayElementAtIndex(i);
                        element.stringValue = lines[i].ToString();

                    }
                }

            
        } 
            //if (_noteNames.vector3Value.y < (target as DigitalSalmon.C360.AutoTour).transform.position.y)


            serializedObject.ApplyModifiedProperties();
        }
    }
}