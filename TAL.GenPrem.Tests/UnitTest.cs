﻿using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using TAL.GenPrem.ServiceInterface;
using TAL.GenPrem.ServiceModel;

namespace TAL.GenPrem.Tests
{
    using System;
    using ServiceModel.Extensions;

    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<MyServices>();
            appHost.Container.AddTransient<PremiumService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        [Test]
        public void CalculateAgeWorksFor50YrsAgoToday()
        {
            //Arrange
            const int targetAgeToday = 50;
            var sameDateXYearsAgo = TestingShared.SameDateXYearsAgo(targetAgeToday);

            //Act
            var age = sameDateXYearsAgo.CalculateAge();

            //Assert
            Assert.AreEqual(targetAgeToday, age);
        }

        [Test]
        public void CalculatePremiumReturnsExpectedResultFor40YrOldFemale()
        {
            //Arrange
            const int targetAgeToday = 40;
            const string gender = "female";

            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", gender);
            var age = input.DateOfBirth.CalculateAge();

            //Act
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            //Assert
            Assert.AreEqual(4400, premium);
        }

        [Test]
        public void CalculatePremiumReturnsExpectedResultFor30YrOldMale()
        {
            const int targetAgeToday = 30;
            const string gender = "male";

            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", gender);
            var age = input.DateOfBirth.CalculateAge();
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            Assert.AreEqual(3600, premium);
        }

        [Test]
        public void CalculatePremiumThrowsIfUnder18()
        {
            void CheckFunction()
            {
                //Arrange
                const int targetAgeToday = 17;
                var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
                var age = input.DateOfBirth.CalculateAge();

                //Act
                PremiumCalculations.CalculatePremium(age, input.Gender);
            }

            //Assert
            Assert.Throws(typeof(ArgumentException), CheckFunction);
        }

        [Test]
        public void CalculatePremiumThrowsIfOver65()
        {
            void CheckFunction()
            {
                //Arrange
                const int targetAgeToday = 66;
                var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
                var age = input.DateOfBirth.CalculateAge();

                //Act
                PremiumCalculations.CalculatePremium(age, input.Gender);
            }

            //Assert
            Assert.Throws(typeof(ArgumentException), CheckFunction);
        }

        [Test]
        public void Can_call_GeneratePremiumService()
        {
            //Arrange
            var service = appHost.Container.Resolve<PremiumService>();
            const int targetAgeToday = 18;
            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
            var age = input.DateOfBirth.CalculateAge();
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            //Act
            var response = (PremiumInputResponse)service.Post(input);

            //Assert
            Assert.That(response.Result, Is.EqualTo($"Your premium is ${premium}"));
        }
    }
}
