using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Messages;
using DAS.GoT.Types.Models;
using DAS.GoT.Types.Utils;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Message = DAS.GoT.Types.Messages.AddCharacterRequest;
using Result = DAS.GoT.Types.Messages.AddCharacterResult;

namespace DAS.GoT.WebApi.Controllers;

[ApiController]
public class CharacterController(
    IMediator mediator,
    ICoreStore store) : ControllerBase
{
    [HttpGet, Route("api/[controller]s")]
    // ToDo: change result type to Task<IActionResult>
    // ToDo: add proper exception handling, possibly consider adding global exception filter
    public IEnumerable<CharacterCore> Get() => store.GetAll();

    [HttpGet, Route("api/[controller]s/{alias}/{itemsPerPage?}")]
    // ToDo: change result type to Task<IActionResult>
    // ToDo: add proper exception handling, possibly consider adding global exception filter
    public Paged<CharacterCore> Search(string alias, int itemsPerPage = 10)
        => Paged<CharacterCore>.Create(store.Search(alias), itemsPerPage);

    [HttpPost, Route("api/[controller]s/{guid}")]
    // ToDo: change result type to Task<IActionResult>
    // ToDo: consider adding global exception filter
    public async Task<AddCharacterResult> Add(Character value, string guid)
    {
        try
        {
            return (await mediator.CreateRequestClient<AddCharacterRequest>()
                .GetResponse<AddCharacterResult>(Message.Create(value, guid))).Message;
        }
        // ToDo: differentiate on exception types
        catch(Exception ex)
        {
            return Result.Create(guid).WithFailure(ex.Message);
        }
    }
}
