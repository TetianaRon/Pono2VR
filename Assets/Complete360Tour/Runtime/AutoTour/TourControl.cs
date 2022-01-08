using System;

namespace Assets.Complete360Tour.Runtime.AutoTour
{
    public interface ITourControl
    {
        event Action<string> OnSwitchNode;
        public string Current { get; }
        public string GoNext();
        public string GoPrev();
    }

    public class TourControl : ITourControl
    {
        private string[] _members;
        private int _index;

        public TourControl(string[] members)
        {
            _members = members;
            _index = 0;
        }


        public event Action<string> OnSwitchNode;
        public string Current => _members[_index];
        public string GoNext()
        {
            throw new NotImplementedException();
        }

        public string GoPrev()
        {
            throw new NotImplementedException();
        }
    }
}