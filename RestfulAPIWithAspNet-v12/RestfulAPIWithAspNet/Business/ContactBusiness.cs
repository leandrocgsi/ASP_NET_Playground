using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Data.VO;

namespace RestfulAPIWithAspNet.Business
{

    public class ContactBusiness
    {

        private readonly ILogger _logger;
        private readonly ContactConverter _converter;

        private IRepository<Contact> _contactRepository;
        private QueryBuilder<Contact> _queryBuilder;

        public ContactBusiness(IRepository<Contact> repository, ILogger<ContactBusiness> logger)
        {
            _contactRepository = repository;
            _converter = new ContactConverter();
            _logger = logger;
            _queryBuilder = new QueryBuilder<Contact>();
        }

        public IEnumerable<ContactVO> FindAll()
        {
            var contacts = _contactRepository.GetAll();
            return _converter.ParseList(contacts);
        }

        public ContactVO GetByIdAsync(string id)
        {
            var contact = _contactRepository.Find(id);
            return _converter.Parse(contact);
        }

        public PagedSearchDTO<Contact> PagedSearch(PagedSearchDTO<Contact> pagedSearchDTO)
        {
            string query = _queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTO("b", "contacts");
          
            pagedSearchDTO.List = _contactRepository.FindWithPagedSearch(query);
            pagedSearchDTO.TotalResults = _contactRepository.GetCount(_queryBuilder.WithDTO(pagedSearchDTO).GetSelectCount("b", "contacts"));

            return pagedSearchDTO;
        }

        public ContactVO Create(Contact contact)
        {
            if (contact == null) return new ContactVO();
            var returnContact = _contactRepository.Add(contact);
            return _converter.Parse(returnContact);
        }

        public ContactVO Update(Contact contact)
        {
            var returnContact = new Contact();
            var result = _contactRepository.Exists(contact.Id);
            if (!result) return new ContactVO();
            var updated = _contactRepository.Update(contact);
            return _converter.Parse(updated);
        }

        public bool Delete(string id)
        {
            if (id == null || "".Equals(id)) return false;
            var result = _contactRepository.Exists(id);
            if (!result) return false;
            _contactRepository.Remove(id);
            return true;
        }
    }
}