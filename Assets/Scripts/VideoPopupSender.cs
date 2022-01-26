using DigitalSalmon.C360;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class VideoPopupSender : Popup
    {
        [Header("Video")]
        [SerializeField]
        protected VideoClip videoClip;

        protected override void Awake()
        {
            base.Awake();

            if (!ValidateClip()) 
                return; 
            //todo: Valideate textures and e.t.c
        }


        private bool ValidateClip()
        {
            if (videoClip == null)
            {
                Debug.LogWarning("No VideoClip found in Popup");
                if (Application.isPlaying) Destroy(gameObject);
                return false;
            }

            return true;
        }

        protected override void HoveredChanged(bool hovered)
        {
            base.HoveredChanged(hovered);
            if (hovered)
            {
               MediaView.VideoReceiver.PlayVideo(videoClip);
            }
            else
            {

                Debug.Log("Should stop video but just hide hopper");
            //todo: do Something
              // MediaView.VideoReceiver.StopVideo();
            }
        }

 
 }

} 
