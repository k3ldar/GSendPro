﻿using System;
using System.Diagnostics.CodeAnalysis;

using GSendApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendApi
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ApiSettingsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidUri_Null_Throws_ArgumentNullException()
        {
            new ApiSettings(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Construct_InvalidUri_NotAbsolute_Throws_ArgumentOutOfRangeException()
        {
            new ApiSettings(new Uri("/partial/address", UriKind.RelativeOrAbsolute));
        }

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            ApiSettings sut = new(new Uri("http://localhost/", UriKind.RelativeOrAbsolute));
            Assert.IsNotNull(sut);
        }
    }
}
