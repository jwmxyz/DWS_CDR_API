using Cdr.Api.Services.CsvMappings;
using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using Crd.DataAccess.Migrations.DbModels;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cdr.Api.Services.Tests
{
    [TestClass]
    public class CsvServicesTests
    {
        private CsvServices _csvServices;
        private Mock<ICdrErrorManager> _errorManagerMock;
        private Mock<ILogger<ICdrErrorManager>> _loggerMock;

        [TestInitialize]
        public void Initialise()
        {
            _loggerMock = new Mock<ILogger<ICdrErrorManager>>();
            _errorManagerMock = new Mock<ICdrErrorManager>();
            _errorManagerMock
                .Setup(x => x.LogErrorAndReturnException<Exception>(It.IsAny<string>(), It.IsAny<Exception>()))
                .Returns(new InvalidCsvException("test"));

            _csvServices = new CsvServices(_errorManagerMock.Object);
        }

        [TestMethod]
        [DeploymentItem(@"TestDataFiles/ValidCsv.csv")]
        public void When_ValidCsvFile_AllResultsAreAddedToList()
        {
            //Arrange
            var stream = File.OpenRead("ValidCsv.csv");
            //Act 
            var result = _csvServices.Read<CallRecord, CallRecordMapping>(stream);
            //Assert
            Assert.IsTrue(result.ValidRecords.Count() == 2);
        }

        [TestMethod]
        [DeploymentItem(@"TestDataFiles/InvalidDateCsv.csv")]
        public void When_InValidCsvFile_AnyInvalidDataIsIgnored()
        {
            //Arrange
            var stream = File.OpenRead("InvalidDateCsv.csv");
            //Act & Assert
            var result = _csvServices.Read<CallRecord, CallRecordMapping>(stream);
            //Assert
            Assert.IsTrue(result.ValidRecords.Count() == 1);
            Assert.IsTrue(result.ParsingErrors.Count() == 1);
        }
    }
}