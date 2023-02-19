namespace DataTests;
public class PersonDataTests
{
    IConfiguration Configuration { get; set; }

    public PersonDataTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonDataTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public async void GetPersons_ShouldReturn()
    {
        // Arrange
        string _connectionString = Configuration["ConnectionStrings:Default"];
        Mock<ILogger<PersonData>> mock = new();
        ILogger<PersonData> logger = mock.Object;
        PersonData personData = new(_connectionString, logger);

        //Act
        IEnumerable<MyPerson> result = await personData.GetPersons();
        List<MyPerson> expected = result.ToList<MyPerson>();

        // Assert
        expected.Should().NotBeNull();
        expected.Count.Should().Be(4); // we know this because we wrote the script in create database
        expected.Where(x => x.Id == 4).Count().Should().Be(1);
        expected.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
        expected.Where(x => x.Id == 4).First().Title.Should().Be("Mr");
        expected.Where(x => x.Id == 4).First().LegalName.Should().Be("Shawn Corey Carter");
        expected.Where(x => x.Id == 4).First().PreferredName.Should().Be("Jay Z");
        expected.Where(x => x.Id == 4).First().Alias.Should().Be("v-shawncarter");
        expected.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
        expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
        expected.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
        expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
    }
}