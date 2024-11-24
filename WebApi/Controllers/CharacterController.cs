using DAS.GoT.Types.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAS.GoT.WebApi.Controllers;

[ApiController, Route("api/[controller]s")]
public class CharacterController : ControllerBase
{
    [HttpGet(Name = "GetCharacters")]
    public IEnumerable<Character> Get()
        => Enumerable.Range(1, 3).Select(index => new Character { }).ToArray();
}
