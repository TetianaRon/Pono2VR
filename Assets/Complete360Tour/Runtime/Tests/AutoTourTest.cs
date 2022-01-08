using System.Collections;
using Assets.Complete360Tour.Runtime.AutoTour;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AutoTourTest
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
