using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulAPIWithAspNet.Test.Data.POCO;
using System;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Test
{
    [TestClass]
    public class QueryBuilderTest
    {
        PagedSearchDTO<Person> dto = new PagedSearchDTO<Person>();
        QueryBuilder<Person> queryBuilder = new QueryBuilder<Person>();

        PagedSearchDTO<Collaborator> dtoCollaborator = new PagedSearchDTO<Collaborator>();
        QueryBuilder<Collaborator> queryBuilderCollaborator = new QueryBuilder<Collaborator>();

        [TestInitialize]
        public void setup()
        {
            dto = MockDTO();
        }

        [TestMethod]
        [Description("Testa a geração do select inicial")]
        public void GetBaseSelectTest()
        {
            String baseSelect = "select * from Person p ";
            Assert.AreEqual(baseSelect, queryBuilder.WithDTO(dto).GetBaseSelect("p", "Person"));
        }

        [TestMethod]
        [Description("Testa a definição da página inicial do select")]
        public void GetStartTest()
        {
            Assert.AreEqual(0, queryBuilder.WithDTO(dto).GetStart());
        }

        [TestMethod]
        [Description("Testa o select count(*) inicial para a query")]
        public void GetBaseSelectCount()
        {
            String baseSelect = "select count(*) from Person p ";
            Assert.AreEqual(baseSelect, queryBuilder.WithDTO(dto).GetBaseSelectCount("p", "Person"));
        }

        [TestMethod]
        [Description("Testa a geração do where e das cláusulas")]
        public void GetWhereAndParametersTest()
        {
            String whereClause = " where p.name = @name and p.email = @email and p.phone = @phone and 1 = 1 ";
            Assert.AreEqual(whereClause, queryBuilder.WithDTO(dto).GetWhereAndParameters("p"));
        }

        [TestMethod]
        [Description("Testa a geração do where e das cláusulas com uma key vazia")]
        public void GetWhereAndParametersWithBlankStringKeyTest()
        {
            Dictionary<String, Object> filters = MockFilters();
            filters.Add("", "LEANDRO");
            dto.Filters = filters;
            String whereClause = " where p.name = @name and p.email = @email and p.phone = @phone and 1 = 1 ";
            Assert.AreEqual(whereClause, queryBuilder.WithDTO(dto).GetWhereAndParameters("p"));
        }

        [TestMethod]
        [Description("Testa a geração do where e das cláusulas com uma value vazia")]
        public void GetWhereAndParametersWithBlankStringValueTest()
        {
            Dictionary<String, Object> filters = new Dictionary<String, Object>();
            filters.Add("name", "");
            dto.Filters = filters;
            String whereClause = " where 1 = 1 ";
            Assert.AreEqual(whereClause, queryBuilder.WithDTO(dto).GetWhereAndParameters("p"));
        }

        [TestMethod]
        [Description("Testa a geração da query final")]
        public void GetQueryFromDTOTest()
        {
            String selectWithParameters = "select * from Person p  where p.name = @name and p.email = @email and p.phone = @phone and 1 = 1  order by p.name asc";
            Assert.AreEqual(selectWithParameters, queryBuilder.WithDTO(dto).GetQueryFromDTO("p", "Person"));
        }

        [TestMethod]
        [Description("Testa a geração do order by")]
        public void GetOrderByTest()
        {
            Assert.AreEqual(" order by p.name asc", queryBuilder.WithDTO(dto).GetOrderBy("p"));
        }

        [TestMethod]
        [Description("Testa a recuperação do realname de uma coluna no banco")]
        public void GetRealColumnNameTest()
        {
            Assert.AreEqual("id", queryBuilderCollaborator.WithDTO(dtoCollaborator).GetRealColumnName("Id"));
            Assert.AreEqual("login_ldap", queryBuilderCollaborator.WithDTO(dtoCollaborator).GetRealColumnName("UserId"));
            Assert.AreEqual("collaborator_name", queryBuilderCollaborator.WithDTO(dtoCollaborator).GetRealColumnName("InstallationId"));
            Assert.AreEqual("dt_record", queryBuilderCollaborator.WithDTO(dtoCollaborator).GetRealColumnName("LastAccess"));
            Assert.AreEqual("status", queryBuilderCollaborator.WithDTO(dtoCollaborator).GetRealColumnName("Status"));
        }

        public PagedSearchDTO<Person> MockDTO()
        {
            dto.CurrentPage = 1;
            dto.PageSize = 10;
            dto.SortFields = "name";
            dto.SortDirections = "asc";
            dto.Filters = MockFilters();
            return dto;
        }

        private Dictionary<String, Object> MockFilters()
        {
            Dictionary<String, Object> filters = new Dictionary<String, Object>();
            filters.Add("name", "LEANDRO");
            filters.Add("email", "a@b.c");
            filters.Add("phone", "12345678998");
            filters.Add("cpf", null);
            filters.Add("religion", null);
            return filters;
        }

    }
}
