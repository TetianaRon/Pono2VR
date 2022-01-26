using System;
using System.Text;
using DigitalSalmon.C360;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace Assets.Scripts
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoReceiver : MonoBehaviour
    {
        private VideoPlayer _videoPlayer;

        [SerializeField]
        private bool _playOnAwake = true;

        [FormerlySerializedAs("_videoToPlay")]
        [SerializeField]
        private string _videoName = "Kolo4ava/mistok"; 

        private MediaView _mediaView;
        private RenderTexture _renTex;

        protected void Awake()
        {

            _videoPlayer = GetComponent<VideoPlayer>();
            _mediaView = GetComponent<MediaView>();

            // todo: Made without load
            _renTex = Resources.Load<RenderTexture>("VideoTex");
    
            if (_renTex == null)
            {
                throw new Exception("No video texture");

            }


        }

        private void Start()
        {
            if (_playOnAwake)
            {
                PlayVideo(_videoName);
            }

        }

            //todo  _videoPlayer.Prepare(); add preparation on video scene enbaled

        public void PlayVideo(string videoName)
        {
            _mediaView.SetvideoTex(_renTex);
            var video = Resources.Load<VideoClip>(videoName);
            if (video == null)
            {
                throw new Exception($"Video {videoName} is not loaded");
            }

            _videoPlayer.clip = video; 
            _videoPlayer.Play();

        }
    }
}