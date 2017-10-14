﻿using RestfulAPIWithAspNet.Models;
using RestfulAPIWithAspNet.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        public IContactsRepository ContactsRepo { get; set; }

        public ContactsController(IContactsRepository _repo)
        {
            ContactsRepo = _repo;
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return ContactsRepo.GetAll();
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public IActionResult GetById(string id)
        {
            var item = ContactsRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ContactsRepo.Add(item);
            return CreatedAtRoute("GetContacts", new { Controller = "Contact", id = item.MobilePhone }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = ContactsRepo.Find(item.Id);
            if (contactObj == null)
            {
                return NotFound();
            }
            ContactsRepo.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ContactsRepo.Remove(id);
        }
    }
}
