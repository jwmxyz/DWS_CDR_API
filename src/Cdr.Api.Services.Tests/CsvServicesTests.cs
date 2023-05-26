using Cdr.Api.Services.CsvMappings;
using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.Api.Services.Tests
{
    [TestClass]
    public class CsvServicesTests
    {
        private CsvServices _csvServices;

        [TestInitialize]
        public void Initialise()
        {
            _csvServices = new CsvServices();
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
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        [DeploymentItem(@"TestDataFiles/InvalidDateCsv.csv")]
        public void When_InValidCsvFile_AnExceptionIsThrown()
        {
            //Arrange
            var stream = File.OpenRead("InvalidDateCsv.csv");
            //Act & Assert
            Assert.ThrowsException<Exception>(() => _csvServices.Read<CallRecord, CallRecordMapping>(stream));
        }
    }
}