//using CallCenter.Data.Model;
//using CallCenter.Server.Validation;
//using MediatR;

//namespace CallCenter.Server.MyPersons;

//public record CreateMyPersonCommand(string Title, string LegalName, string PreferredName, string Alias) 
//    : IRequest<Result<MyPerson, ValidationFailed>>;

//public class CreateMyPersonHandler : IRequestHandler<CreateMyPersonCommand, Result<MyPerson>>
//{
//    private readonly IMyPersonRepository _repository;
//    public async Task<Result<MyPerson, ValidationFailed>> Handle(CreateMyPersonCommand request, CancellationToken cancellationToken)
//    {
//        MyPerson person = new()
//        {
//            Title = request.Title,
//            LegalName = request.LegalName,
//            PreferredName = request.PreferredName,
//            Alias = request.Alias
//        };

//        await _repository.CreateAsync(person);
//        return person;
//    }
//}