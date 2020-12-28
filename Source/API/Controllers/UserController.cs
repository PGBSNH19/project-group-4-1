using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<UserDto[]>> GetUsers()
        {
            try
            {
                var results = await _userRepository.GetUsers();
                var mappedEntities = _mapper.Map<UserDto[]>(results);
                if (mappedEntities.Length == 0)
                {
                    return NotFound();
                }
                return Ok(mappedEntities);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var result = await _userRepository.GetUserById(id);
                var mappedEntity = _mapper.Map<UserDto>(result);
                if (mappedEntity == null)
                {
                    return NotFound();
                }
                return Ok(mappedEntity);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }
        [HttpGet("GetUserByName/{name}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            try
            {
                var result = await _userRepository.GetUserByName(name);
                var mappedEntity = _mapper.Map<UserDto>(result);
                if (mappedEntity == null)
                {
                    return NotFound();
                }
                return Ok(mappedEntity);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userRepository.GetUserByEmail(email);
                var mappedEntity = _mapper.Map<UserDto>(result);
                if (mappedEntity == null)
                {
                    return NotFound();
                }
                return Ok(mappedEntity);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserDto user)
        {
            user.Type = UserType.Buyer;
            try
            {
                var mappedEntity = _mapper.Map<User>(user);
                _userRepository.Add(mappedEntity);
                if (await _userRepository.Save())
                {
                    return Created("/api/v1.0/[controller]" + user.UserID, new User { UserID = user.UserID });
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        private void Debugger()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                _userRepository.Delete(user);
                if (await _userRepository.Save())
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
