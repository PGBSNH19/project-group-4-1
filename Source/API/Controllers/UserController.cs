﻿using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


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

        /// <summary>
        /// Log in User.
        /// </summary>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _userRepository.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// Post a new User.
        /// </summary>
        /// <remarks>
        /// Sample Request: 
        ///
        ///    Post /User
        ///    
        ///    {
        ///    
        ///         "UserID": 1,
        ///         
        ///         "Username": "Example",
        ///         
        ///         "Email": "Example@Example.com",
        ///         
        ///         "Password": "*********",
        ///         
        ///         "Salt": "jdakjgo21ok4k==",
        ///         
        ///         "UserType": 2,
        ///         
        ///         "MarketplaceSellers": [],
        ///         
        ///         "UserProducts": []
        ///         
        ///    }
        ///
        ///</remarks>
        /// <param name="user"></param>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserDto user)
        {
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
                if (e is DbUpdateException)
                {
                    return this.StatusCode(StatusCodes.Status403Forbidden, $"Value not unique {e.InnerException.Message}");
                }
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
