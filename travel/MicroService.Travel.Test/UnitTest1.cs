using System;

using MicroService.Travel.Service.Declaration;
using MicroService.Travel.Service.Repository;
using MicroService.Travel.Service.Service;

using NUnit.Framework;

namespace MicroService.Travel.Test
{
    public class Tests
    {
        private ITravelService _travelService;

        [SetUp]
        public void Setup()
        {
            _travelService = new TravelService(new TravelRepository());
        }

        [Test]
        public void InsertTravel()
        {
            try
            {
                var inserted = _travelService.Create(new Service.Model.Travel
                                          {
                                              Name = "Test"
                                          });

                var travel = _travelService.Get(inserted.Id);

                Assert.Equals(inserted.Id, travel.Id);
                Assert.Equals(inserted.Name, travel.Name);

                _travelService.Delete(inserted.Id);

                Assert.Pass("InsertTravel");
            }
            catch (Exception e)
            {
                Assert.Fail("InsertTravel");
            }
        }

        [Test]
        public void UpdateTravel()
        {
            try
            {
                var inserted = _travelService.Create(new Service.Model.Travel
                                          {
                                              Name = "Test"
                                          });

                inserted.Name = "Test 2";

                var travel = _travelService.Update(inserted);

                Assert.Equals(inserted.Id, travel.Id);
                Assert.Equals(inserted.Name, travel.Name);

                _travelService.Delete(inserted.Id);

                Assert.Pass("InsertTravel");
            }
            catch (Exception e)
            {
                Assert.Fail("InsertTravel");
            }
        }
    }
}