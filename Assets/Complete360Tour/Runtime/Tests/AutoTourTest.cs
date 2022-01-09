using System;
using Assets.Complete360Tour.Runtime.AutoTour;
using NUnit.Framework;

namespace Assets.Complete360Tour.Runtime.Tests
{
    //todo: Test IMediaView integration
    public class AutoTourTest
    {
        ITourControl _tour;

        [SetUp]
        public void Setup()
        {
            _tour = new TourControl(new[] { "0", "1", "2" });
        }

        [Test]
        public void Test1Fist()
        {
            var result = _tour.Current;
            Assert.AreEqual("0", result);
        }

        [Test]
        public void Test2Next()
        {
            var result = _tour.GoNext();
            Assert.AreEqual("1", result);
            result = _tour.Current;
            Assert.AreEqual("1", result);

            Action<string> isTwo = (str) => { Assert.AreEqual("2", str); };
            Action<string> isZero = (str) =>
            {
                if (str == "0")
                {
                    Assert.Pass();
                }
            };

            _tour.OnSwitchNode += isTwo;
            result = _tour.GoNext();

            _tour.OnSwitchNode -= isTwo;
            _tour.OnSwitchNode += isZero;
            result = _tour.GoNext();
            Assert.Fail();
        }

        [Test]
        public void Test2Prev()
        {
            var result = _tour.GoPrev();
            Assert.AreEqual("2", result);
            result = _tour.Current;
            Assert.AreEqual("2", result);

            Action<string> isTwo = (str) => { Assert.AreEqual("1", str); };
            Action<string> isZero = (str) =>
            {
                if (str == "0")
                {
                    Assert.Pass();
                }
            };

            _tour.OnSwitchNode += isTwo;
            result = _tour.GoPrev();
            Assert.AreEqual("1", result);
            _tour.OnSwitchNode -= isTwo;

            _tour.OnSwitchNode += isZero;
            result = _tour.GoPrev();
            Assert.AreEqual("0", result);

            Assert.Fail();
        }
    }
}