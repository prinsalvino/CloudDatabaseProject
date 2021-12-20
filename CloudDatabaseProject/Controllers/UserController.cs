using AutoMapper;
using CloudDatabaseProject.DTO;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudDatabaseProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        ILogger _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;   
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserReturnDTO> Get()
        {
            try
            {
                var newUsers = new List<UserReturnDTO>();
                var users = _userService.GetUsers(); ;
                foreach (var user in users)
                {
                    var newUser = _mapper.Map<UserReturnDTO>(user);
                    newUsers.Add(newUser);
                }
                return newUsers;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = _userService.GetUser(id);
                var newUser = _mapper.Map<OrderItemReturnDTO>(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }

        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] UserDTO body)
        {
            try
            {
                var user = _mapper.Map<User>(body);

                _userService.AddUser(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
              
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDTO value)
        {
            try
            {
                User user = _userService.GetUser(id);
                if (value.Name != String.Empty)
                    user.Name = value.Name;
                if (value.Email != String.Empty)
                    user.Email = value.Email;
                if (user.Password != String.Empty)
                    user.Password = value.Password;

                _userService.UpdateUser(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _userService.DeleteUser(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
