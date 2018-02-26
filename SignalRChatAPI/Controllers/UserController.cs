using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRChatAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRChatAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        /* 
         * The constructor uses Dependency Injection to inject
         * the database context into the controller, where it
         * is used in each of the CRUD methods in the controller
        */ 
        public  UserController(UserContext context)
        {
            _context = context;
            //  If the database is empty, then add dummy data
            if(_context.Users.Count() == 0)
            {
                _context.Users.Add(new UserModel
                {
                    Email = "test@mail.com",
                    Password = "12345",
                    UserName = "DaveRocks"                  
                });
                _context.SaveChanges();
            }
        }

        // GET api/user
        [HttpGet]
        public IEnumerable<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }

        // GET api/user/#

        /*
         * Name = "something" creates a named route that enable the app to create an 
         * HTTP link using the route name
         */
        [HttpGet("{id}", Name = "GetUser")] 
        public IActionResult GetById(long id)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel user)
        {   // The [FromBody] attribute tells the MVC to get the value 
            // of the object (user) from the HTTP request.
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            /*
             * Returns a 201 responce, and adds a location header to he response.
             * Location header specifies the URI of the newly created object (user)
             * Then uses the "GetUser" named route to create the URL.  
             */ 
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
        // PUT /api/user/#
        // THIS METHOD AS WRITTEN REQUIRES ALL OBJECT VALUES TO BE INPUTTED
        // IN ORDER TO UPDATE THE USER IN THE DB
        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, [FromBody] UserModel user)
        {
            if(user == null || user.Id != id)
            {
                return BadRequest();
            }

            var personToUpdate = _context.Users.FirstOrDefault(t => t.Id == id);

            if(personToUpdate == null)
            {
                return NotFound();
            }

            personToUpdate.IsOnline = user.IsOnline;
            personToUpdate.UserName = user.UserName;
            personToUpdate.Email = user.Email;
            personToUpdate.Password = user.Password;

            _context.Users.Update(personToUpdate);
            _context.SaveChanges();
            return new NoContentResult();
        }
        // DELETE /api/user/#
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return new NoContentResult();
        }


    }


}
