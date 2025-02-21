using Microsoft.AspNetCore.Mvc;
using UserApplication.Dtos;
using UserApplication.Services;

namespace UserApplication.Controllers;

public interface IUserController
{
    IActionResult Get();
    IActionResult GetUser(int id);
    IActionResult GetUsers();
    IActionResult AddUser([FromBody] UserDto.User userInfo);
    IActionResult Delete(int id);
}

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase, IUserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public string Test1 { get; set; }

    //TODO: test postponed
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new[] { "UserApplication web api" });
    }


    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUser(id);

        if (user == null)
            return BadRequest("User not found!");

        return Ok(user);
    }

    [HttpGet("all")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        if (users == null || users.Count == 0)
            return BadRequest("Users not found!");
        return Ok(users);
    }


    [HttpPost("adduser")]
    public IActionResult AddUser([FromBody] UserDto.User userInfo)
    {
        try
        {
            var n = new Test();
            userInfo.CreateDate = DateTime.Now;
            var user = _userService.AddUser(userInfo);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest("Failed to add user! " + ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _userService.DeleteUser(id);

        if (user == null)
            return BadRequest("Failed to delete user!");

        return Ok(user);
    }
}