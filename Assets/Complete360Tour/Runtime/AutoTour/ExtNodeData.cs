using System;

namespace Assets.Complete360Tour.Runtime.AutoTour
{
    public class ExtNodeData
    {
 
        public Guid Id { get; private set; } 
        public string FileName { get; private set; } 
        public int IndexInSequence { get; private set; } 
        public string ThumbnailPath { get; private set; }

        public string GetTitle(int lang = 0)
        {
            throw new NotImplementedException(); 
        }

    }
}