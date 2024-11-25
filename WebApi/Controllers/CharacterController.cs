using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAS.GoT.WebApi.Controllers;

[ApiController]
public class CharacterController(ICoreStore store) : ControllerBase
{
    [HttpGet, Route("api/[controller]s")]
    public IEnumerable<CharacterCore> Get() => store.GetAll();

    [HttpGet, Route("api/[controller]s/{alias}/{itemsPerPage?}")]
    public Paged<CharacterCore> Search(string alias, int itemsPerPage = 10)
        => Paged<CharacterCore>.Create(store.Search(alias), itemsPerPage);
}
