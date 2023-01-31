using Autofac.Extras.Moq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Test
{
    public class BaseTest
    {
        protected AutoMock Mock { get; set; } = AutoMock.GetLoose();

        protected Fixture Fixture { get; set; } = new Fixture();
    }
}
