using System;
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
            _videoPlayer.targetTexture = _renTex;
    
            if (_renTex == null)
            {
                throw new Exception("No video texture");

            }


        }


        //todo  _videoPlayer.Prepare(); add preparation on video scene enbaled

        public void PlayVideo(string videoName)
        {
            var video = Resources.Load<VideoClip>(videoName);
            PlayVideo(video);
        }

        public void StopVideo()
        {
            _videoPlayer.Stop();
        }

        public void PlayVideo(VideoClip video)
        {
            _mediaView.SetvideoTex(_renTex);

            if (video == null)
            {
                throw new Exception($"Video {video.name} is not loaded");
            }

            _videoPlayer.clip = video;
            _videoPlayer.Play();
        }
    }
}