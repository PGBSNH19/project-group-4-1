﻿using API.Models;
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

        /// <summary>
        /// Gets all users
        /// </summary>
        /// 
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

        /// <summary>
        /// Gets a User by their id.
        /// </summary>
        /// 
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var result = await _userRepository.GetUserById(id);
                var mappedEntity = _mapper.Map<UserDto>(result);
                mappedEntity.UserID = result.UserID;
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

        /// <summary>
        /// Gets a User by their name.
        /// </summary>
        /// 
        [HttpGet("GetUserByName/{name}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            try
            {
                var result = await _userRepository.GetUserByName(name);
                var mappedEntity = _mapper.Map<UserDto>(result);
                mappedEntity.UserID = result.UserID;
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

        /// <summary>
        /// Gets a User by their email.
        /// </summary>
        /// 
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userRepository.GetUserByEmail(email);
                var mappedEntity = _mapper.Map<UserDto>(result);
                mappedEntity.UserID = result.UserID;
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

        /// <summary>
        /// Post a new User.
        /// </summary>
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
        /// <summary>
        /// Puts a User.
        /// </summary>
        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> PutUser(int userId,[FromBody] UserDto userDto)
        {
            try
            {
                var oldUser = await _userRepository.GetUserById(userId);
                if (oldUser == null)
                    return NotFound($"Can't find any user with id: {userId}");
                var newUser = _mapper.Map(userDto, oldUser);
                _userRepository.Update(newUser);
                if (await _userRepository.Save())
                    return Ok(newUser);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
            return BadRequest();
        }
        private void Debugger()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a specific User.
        /// </summary>
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
