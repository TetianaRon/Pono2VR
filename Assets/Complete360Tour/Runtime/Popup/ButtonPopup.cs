using System;

namespace DigitalSalmon.C360
{
    public class ButtonPopup:Popup
    {
        public event Action OnTriggerEnter;
        protected override void Trigger()
        {
            base.Trigger();

        }
    }
}