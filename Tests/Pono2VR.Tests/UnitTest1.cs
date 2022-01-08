using Assets.Complete360Tour.Runtime.AutoTour;
using Microsoft.VisualBasic.CompilerServices;
using NUnit.Framework;

namespace Pono2VR.Tests
{
    public class Tests
    {
        ITourControl _tour;
        [SetUp]
        public void Setup()
        {
            _tour = new TourControl(new []{"0", "1", "2"});
        }

        [Test]
        public void Test1()
        {
            var result = _tour.Current;
            Assert.AreEqual("0",result);
        }
    }
}