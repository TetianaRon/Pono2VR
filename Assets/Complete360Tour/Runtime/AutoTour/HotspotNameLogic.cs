using System;
using UnityEngine;

namespace Assets.Complete360Tour.Runtime.AutoTour
{
    //todo: Node extra Info container (Image,Name,Translation, e.t.c)
    //todo: AutoTour refs that Container by hash code or something;

    public interface IFadableName
    {
        void FadeIn(string text);
        void FadeOut(string text);
        
        event Action OnFadedIn; 
        event Action OnFadedOut; 

        event Action<float> OnUpdateTime;
        bool IsActive { get;}
    }

    public abstract class HotspotNameLogicBase<T> where T:IFadableName
    {

        public bool IsShowing { get; protected set; }

        protected IFadableName fader;
        public float Time { get; protected set; }
        protected float lastPressedTime;
        protected readonly float unpressedTimeout;

        protected string Title { get; }

        protected HotspotNameLogicBase(IFadableName fader, float unpressedTimeout, string title)
        {
            IsShowing = false;
            this.fader = fader;
            this.unpressedTimeout = unpressedTimeout;
            Title = title;
            Time = 0;
            fader.OnUpdateTime += UpdateTime;
        }

        private void UpdateTime(float time)
        {
            this.Time = time;

            HideTimeoutUpdate();

        }

        protected abstract void HideTimeoutUpdate();


        public void Pressed()
        {
            lastPressedTime = Time;

        }

    }

    public class HotspotNameLogic: HotspotNameLogicBase<IFadableName>
    {
        protected override void HideTimeoutUpdate()
        {

            if (IsShowing && Time > lastPressedTime + unpressedTimeout)
            {
                IsShowing = false;
                fader.FadeOut(Title);
            }
        }

        public HotspotNameLogic(IFadableName fader, float unpressedTimeout, string title) : base(fader, unpressedTimeout, title)
        {
        }
    }

}