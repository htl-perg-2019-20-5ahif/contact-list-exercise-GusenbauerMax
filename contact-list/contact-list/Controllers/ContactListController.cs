using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace contact_list.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactListController : ControllerBase
    {
        private static List<Contact> contacts = new List<Contact> {};

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact newContact)
        {
            contacts.Add(newContact);
            return CreatedAtRoute("GetSpecificContact", new { index = contacts.IndexOf(newContact) }, newContact);
        }

        [HttpGet]
        [Route("findByName/{name}", Name = "GetSpecificContact")]
        public IActionResult GetContact(string name)
        {
            return Ok(contacts.Find(contact => contact.FirstName.ToLower().Contains(name.ToLower()) || contact.LastName.ToLower().Contains(name.ToLower())));
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteContact(int index)
        {
            if (index >= 0 && index < contacts.Count)
            {
                contacts.RemoveAt(index);
                return NoContent();
            }

            return BadRequest("Invalid index");
        }
    }
}
