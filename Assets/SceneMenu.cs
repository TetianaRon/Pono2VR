using System;
using DigitalSalmon.C360;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
    public class AutoTourOptions
    {
        [Tooltip("If true the AutoTour will begin as soon as you press Play.")]
        [SerializeField]
        public bool autoStart;

        [Tooltip("If true the AutoTour will loop back to the first node when it reaches the end")]
        [SerializeField]
        public bool loop;

        [Tooltip("The length of time to spend in each node")]
        [SerializeField]
        public float nodeDuration = 15f;

        [Tooltip("The length of time to spend in each node")]
        [SerializeField]
        public float startTimeout = 10f;

        [SerializeField] 
        public Text CurrentNode;

        [SerializeField] 
        public Popup PrevButton;

        [SerializeField] 
        public Popup NextButton;
    }

public class SceneMenu : MonoBehaviour
{
    public AutoTourOptions Options; 

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float  _limitAngle =60;
    [SerializeField]
    private float  _lerpRange = 10;
    [SerializeField]
    private float  _speed = 10;



    void Update()
    {
        var anglesX = _camera.rotation.eulerAngles.x;
        var dif = (anglesX - _limitAngle)*-1;
        if (dif>0)
        {
            var a = transform.rotation;
            var b = Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up);
            var f = dif >= _lerpRange ? 1f : dif / _lerpRange;
            transform.rotation = Quaternion.Lerp(a,b,f*Time.deltaTime*_speed);
        }
    }
}
