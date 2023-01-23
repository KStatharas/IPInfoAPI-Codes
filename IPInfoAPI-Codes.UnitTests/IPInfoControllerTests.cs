using AutoFixture;
using AutoFixture.AutoMoq;
using IPInfoAPI_Codes.Controllers;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.RegularExpressions;

namespace IPInfoAPI_Codes.UnitTests
{
    public class IPInfoControllerTests
    {
        private Mock<IIPInfoService> _serviceMock;
        private Fixture _fixture;
        private IPInfoController _controller;

        public IPInfoControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = new Mock<IIPInfoService>();

        }

        [Test]
        public async Task GetIP_Returns_CountryDTO_By_Giving_An_IP()
        {
            string ip = "12.23.34.45";
            var countryDTO = _fixture.Create<CountryDTO>();
            _serviceMock.Setup(repo => repo.GetCountry(It.IsAny<string>())).Returns(Task.FromResult(countryDTO));

            _controller = new IPInfoController(_serviceMock.Object);

            var result = await _controller.GetIP(ip);
            var obj = result as OkObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [Test]
        public async Task GetReport_Returns_CountryReportDTO()
        {
            List<String> twoLetterCodes = new List<String>() { "US", "KR", "GR" };
            var countryReportDTOList = _fixture.CreateMany<CountryReportDTO>(3);
            _serviceMock.Setup(repo => repo.GetIpReport(It.IsAny<List<String>>())).Returns(Task.FromResult(countryReportDTOList));

            _controller = new IPInfoController(_serviceMock.Object);

            var result = await _controller.GetReport(twoLetterCodes);
            var obj = result as OkObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}