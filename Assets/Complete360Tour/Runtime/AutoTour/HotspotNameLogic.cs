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

        /// <summary>
        /// false - means we can start FadeIn becasue it's isnt started
        /// true - means we can FadeOut becase it's Active
        /// </summary>
        bool IsActive { get;}
    }

    public abstract class HotspotNameLogicBase<T> where T:IFadableName
    {

        public bool IsShowing { get; protected set; }

        protected IFadableName fader;
        public float Time { get; protected set; }
        protected float lastPress;
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
            HideTimeoutCheck();

        }

        protected abstract void HideTimeoutCheck();


        public void PressDown()
        {
            if (!fader.IsActive)
            {
                fader.FadeIn(Title);
            }else if (Time - lastPress > unpressedTimeout)
            {
                 fader.FadeOut(Title);
            }

            lastPress = Time; 
        }

    }

    public class HotspotNameLogic: HotspotNameLogicBase<IFadableName>
    {
        protected override void HideTimeoutCheck()
        {

            if (IsShowing && Time > lastPress + unpressedTimeout)
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