using CallCenter.Data;
using CallCenter.Data.Model;

namespace DataTests;
public class PersonDataTests
{
    private IConfiguration Configuration { get; set; }

    public PersonDataTests()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonDataTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public async void GetPersons_ShouldReturn()
    {
        // Arrange
        string? _connectionString = Configuration["ConnectionStrings:Default"];
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            // fail fast because we have no connection string
            _ = true.Should().BeFalse();
            return;
        }
        Mock<ILogger<PersonDataAccess>> mock = new();
        ILogger<PersonDataAccess> logger = mock.Object;
        PersonDataAccess personData = new(_connectionString, logger);
        CancellationTokenSource cancellationTokenSource = new();

        //Act
        IEnumerable<MyPerson> result = await personData.GetPersons(cancellationTokenSource.Token);
        List<MyPerson> expected = result.ToList<MyPerson>();

        // Assert
        _ = expected.Should().NotBeNull();
        _ = expected.Count.Should().Be(4); // we know this because we wrote the script in create database
        _ = expected.Where(x => x.Id == 4).Count().Should().Be(1);
        _ = expected.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
        _ = expected.Where(x => x.Id == 4).First().Title.Should().Be("Mr");
        _ = expected.Where(x => x.Id == 4).First().LegalName.Should().Be("Shawn Corey Carter");
        _ = expected.Where(x => x.Id == 4).First().PreferredName.Should().Be("Jay Z");
        _ = expected.Where(x => x.Id == 4).First().Alias.Should().Be("v-shawncarter");
        _ = expected.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
        _ = expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
        _ = expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
        _ = expected.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
        _ = expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
        _ = expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public async void UpdatePersons_ShouldReturn()
    {
        // Arrange
        string? _connectionString = Configuration["ConnectionStrings:Default"];
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            // fail fast because we have no connection string
            _ = true.Should().BeFalse();
            return;
        }
        Mock<ILogger<PersonDataAccess>> mock = new();
        ILogger<PersonDataAccess> logger = mock.Object;
        PersonDataAccess personData = new(_connectionString, logger);
        CancellationTokenSource cancellationTokenSource = new();
        IEnumerable<MyPerson> result = await personData.GetPersons(cancellationTokenSource.Token);
        List<MyPerson> actual = result.ToList<MyPerson>();
        if (!actual.Any())
        {
            _ = true.Should().BeFalse();
            return;
        }
        MyPerson person = actual[0];
        person.ModifiedBy = 1;
        person.ModifiedDate = DateTime.UtcNow;

        // Act 
        MyInteger response = await personData.UpdateMyPerson(person, cancellationTokenSource.Token);

        // Assert
        _ = response.Value.Should().Be(1);
    }
}