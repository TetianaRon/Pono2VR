using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Complete360Tour.Runtime.AutoTour;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalSalmon.C360 {

    public class AutoTour : MonoBehaviour {
		//-----------------------------------------------------------------------------------------
		// Events:
		//-----------------------------------------------------------------------------------------

		public event EventHandler Complete;

		//-----------------------------------------------------------------------------------------
		// Inspector Variables:
		//-----------------------------------------------------------------------------------------

		[Tooltip("The names, in order, of the nodes this AutoTour should traverse.")]
		[SerializeField]
		protected string[] nodeNames;

		[SerializeField]
        public string[] nodeTextUkr;

        private Dictionary<string, string> _nameDic;

        //-----------------------------------------------------------------------------------------
		// public Fields:
		//-----------------------------------------------------------------------------------------


        private float _lastSwitch;

		private Complete360Tour complete360Tour;
        private TourControl _tourControl;

        [SerializeField]
        private  SceneMenu _sceneMenu;

        [SerializeField]
        private  AutoTourOptions _autoTourOptions; 
        //-----------------------------------------------------------------------------------------
		// Unity Lifecycle:
		//-----------------------------------------------------------------------------------------
        private static AutoTour _instance;

        protected void Awake()
        {
            _instance = this;
            
            if (nodeNames.Length > nodeTextUkr.Length)
            {
                throw new Exception("We have more names than we have translations");
            }

            _nameDic = new Dictionary<string, string>(nodeNames.Length);
            for (int i = 0; i < nodeNames.Length; i++)
                _nameDic.Add(nodeNames[i],nodeTextUkr[i]);

            _autoTourOptions = _sceneMenu.Options;
            _lastSwitch = _autoTourOptions.startTimeout;
            complete360Tour = GetComponent<Complete360Tour>();
            _tourControl = new TourControl(nodeNames);
        }

        private void OnEnable()
        {
            Complete360Tour.MediaSwitch += SwitchText;
            _tourControl.OnSwitchNode += complete360Tour.GoToMedia;

            _autoTourOptions.PrevButton.OnTrigger += Prev;
            _autoTourOptions.NextButton.OnTrigger += Next;

        }


        private void OnDisable()
        {
            _tourControl.OnSwitchNode -= complete360Tour.GoToMedia;
            Complete360Tour.MediaSwitch += SwitchText;
            _autoTourOptions.PrevButton.OnTrigger -= Prev;
            _autoTourOptions.NextButton.OnTrigger -= Next;
        }

        private void Next(Hotspot value)
        {
            _tourControl.GoNext();
        }

        private void Prev(Hotspot value)
        {
            _tourControl.GoPrev();
        }

        public static string GetTitleFromName(string niceName)
        {
            if (_instance == null)
                return niceName;
            return _instance.GetTextFromInstance(niceName);
        }

        public string GetTextFromInstance(string niceName)
        {
            if (_nameDic.ContainsKey(niceName))
                return _nameDic[niceName];

            return niceName;
        }

        private void SwitchText(MediaSwitchStates state, NodeData node)
        {
            _autoTourOptions.CurrentNode.text = GetTextFromInstance(node.NiceName);
            _lastSwitch = Time.time;

        }



		protected IEnumerator Start() {
			// Delay a frame to let C360 initialise.
			yield return null;
			if (_autoTourOptions.autoStart) BeginAutoTour();
		}

		//-----------------------------------------------------------------------------------------
		// Public Methods:
		//-----------------------------------------------------------------------------------------

		public void BeginAutoTour() { StartCoroutine(AutoTourCoroutine()); }

		public void StopAutoTour() { StopAllCoroutines(); }

		//-----------------------------------------------------------------------------------------
		// Private Methods:
		//-----------------------------------------------------------------------------------------

		private IEnumerator AutoTourCoroutine() {


            yield return null;
			while (true) {

                yield return null;
                if (_lastSwitch + _autoTourOptions.nodeDuration < Time.time)
                {
                    _tourControl.GoNext(); 
                    _lastSwitch = Time.time;

                }
                yield return null;
            }
		}

	}
}