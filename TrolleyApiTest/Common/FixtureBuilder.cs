using AutoFixture;
using AutoFixture.AutoNSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrolleyApiTest.Common
{
    public static class FixtureBuilder
    {
        public static IFixture Build()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}
