using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthAPIController : ControllerBase
  {
    private readonly IAuthService a;
    protected ResponseDto r;

    public AuthAPIController(IAuthService b)
    {
      a = b;
      r = new ResponseDto();
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegistrationRequestDto m)
    {
      var err = a.Register(m).Result;
      if (err != null && err != "")
      {
        r.IsSuccess = false;
        r.Message = err;
        return BadRequest(r);
      }
      r.Message = "Success";
      r.Result = m;
      return Ok(r);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto d)
    {
      var x = a.Login(d).Result;
      if (x == null || x.User == null)
      {
        r.IsSuccess = false;
        r.Message = "Something wrong";
        return BadRequest(r);
      }

      r.Result = x;
      r.Message = "Logged in";
      return Ok(r);
    }

    [HttpPost("AssignRole")]
    public IActionResult AssignRole([FromBody] RegistrationRequestDto p)
    {
      if (p == null) return BadRequest("bad");
      var s = a.AssignRole(p.Email, p.Role.ToUpper()).Result;
      if (!s)
      {
        return StatusCode(500);
      }
      return Ok("done");
    }
  }
}
