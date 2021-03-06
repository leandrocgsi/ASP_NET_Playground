﻿using RestfulAPIWithAspNet.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.DTO;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private IRepository<Contact> _ContactRepository;

        public ContactController(IRepository<Contact> repository)
        {
            _ContactRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return _ContactRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = _ContactRepository.Find(id);
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
            _ContactRepository.Add(item);
            return new ObjectResult(item);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = _ContactRepository.Find(item.Id);
            if (contactObj == null)
            {
                return NotFound();
            }
            _ContactRepository.Update(item);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _ContactRepository.Remove(id);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Contact> pagedSearchDTO)
        {

            return new ObjectResult(pagedSearchDTO);
        }
    }
}
