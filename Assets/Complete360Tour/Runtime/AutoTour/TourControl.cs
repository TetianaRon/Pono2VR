using System;

    namespace Assets.Complete360Tour.Runtime.AutoTour
{
    //todo: Write tests for switched media
    public interface IViewMediaSwitcher
    {
        event Action<string> OnSwitchMedia;
        event Action<float> OnFade;
        event Action<bool> OnEnable;
        void Update(float time);

    }
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

        private void MembersCheck()
        {
            if (_members == null || _members.Length == 0)
                throw new Exception($"_members of tour is NullOrEmpty");
        }

        public string GoNext()
        {
            MembersCheck();

            if (++_index >= _members.Length)
                _index = 0; 

            OnSwitchNode?.Invoke(_members[_index]); 
            return _members[_index];
        }

        public string GoPrev()
        {
            MembersCheck();

            if (--_index < 0)
                _index = _members.Length-1; 

            OnSwitchNode?.Invoke(_members[_index]); 
            return _members[_index];
        }
    }
}