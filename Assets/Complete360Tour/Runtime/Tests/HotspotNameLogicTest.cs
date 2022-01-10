using System;
using Assets.Complete360Tour.Runtime.AutoTour;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Complete360Tour.Runtime.Tests
{
    public class HotspotNameLogicTest
    {
        private HotspotNameLogic _logic;
        private IFadableName _fader;
        private string _title;

        [SetUp]
        public void SetUp()
        {
            _fader = Substitute.For<IFadableName>();
            _title = "Hello";
            _logic = new HotspotNameLogic(_fader, 0.5f, _title);
        }

        [Test]
        public void Test00TimeUpdate()
        {
            _fader.OnUpdateTime += Raise.Event<Action<float>>(0.5f);
            Assert.AreEqual(0.5f,_logic.Time);
        }

        [Test]
        public void Test01FadeIn()
        {

            _logic.PressDown();
            _fader.Received().FadeIn(_title);
        }

        [Test]
        public void Test01FadeOut()
        {
             Assert.AreEqual(0.5f,_logic.Time);
        }
    }
}