using DigitalSalmon.C360;
using UnityEditor;
using UnityEngine;

namespace Assets.Complete360Tour.Runtime.Tests
{
    [CustomEditor(typeof(DigitalSalmon.C360.AutoTour))]
    public class AutoTourEditor : Editor
    {
        SerializedProperty _noteNames;

    private string _textAsset = "Assets/0Tours/Kosiv/Kosiv.txt";


    void OnEnable()
        {
            _noteNames = serializedObject.FindProperty("nodeNames");
        }

        public override void OnInspectorGUI()
        {
                if (EditorGUILayout.LinkButton( "LoadAsset"))
                {

                     DigitalSalmon.C360.Complete360Tour completeTour = (target as DigitalSalmon.C360.AutoTour).GetComponent<DigitalSalmon.C360.Complete360Tour>();
                    if(_noteNames.arraySize>0)
                        _noteNames.ClearArray();

                
                    var text = completeTour.tourData.text;
                    var tour = TourConverter.ParseTour(text);
                    for (int i = 0; i < tour.NodeDataList.Count; i++)
                    {
                        _noteNames.InsertArrayElementAtIndex(i);
                        var element = _noteNames.GetArrayElementAtIndex(i);
                        element.stringValue = tour.NodeDataList[i].NiceName;

                    }
                } 
            base.OnInspectorGUI();
            //if (_noteNames.vector3Value.y < (target as DigitalSalmon.C360.AutoTour).transform.position.y)


            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}