using AutoFixture;
using IP2C_IPInfoProvider.Services;
using IP2C_IPInfoProvider.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPInfoAPI_Codes.DTO;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using IP2C_IPInfoProvider.Models;

namespace IPInfoAPI_Codes.UnitTests
{
    public class IP2CImplTests
    {
        private Mock<IIP2C> _iP2CImplMock;
        private Fixture _fixture;
        private IIP2C _ip2CImpl;

        public IP2CImplTests()
        {
            _iP2CImplMock = new Mock<IIP2C>();
            _fixture = new Fixture();
        }

        [Test]
        public void GetIPCountryDetails_Returns_IPInfo_By_Giving_A_Valid_IP()
        {
            var validIP = "12.23.34.45";

            _iP2CImplMock.Setup(ip2c => ip2c.getIPCountryDetails(It.IsAny<string>())).Returns(It.IsAny<IPInfo>());

            _ip2CImpl = new IP2CImpl();

            var result = _ip2CImpl.getIPCountryDetails(validIP);

            Assert.IsInstanceOf<IPInfo>(result);
        }

        [Test]
        public void GetIPCountryDetails_Throws_BadIPRequestException_By_Giving_An_Invalid_IP()
        {
            var invalidIP = _fixture.Create<string>();

            _iP2CImplMock.Setup(ip2c => ip2c.getIPCountryDetails(It.IsAny<string>())).Throws(new BadIPRequestException(It.IsAny<string>()));

            _ip2CImpl = new IP2CImpl();

            Assert.Throws<BadIPRequestException>(()=> _ip2CImpl.getIPCountryDetails(invalidIP));
        }
    }
}
