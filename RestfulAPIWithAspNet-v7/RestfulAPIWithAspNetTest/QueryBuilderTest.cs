using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulAPIWithAspNet.Test.Data.POCO;
using System;
using System.Collections.Generic;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Model;
using RestfulAPIWithAspNet.Tests.Utils.Data.POCO;
using RestfulAPIWithAspNet.Utils.Data;

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
            String whereClause = " where p.name like '%LEANDRO%' and p.email like '%a@b.c%' and p.phone like '%12345678998%' and 1 = 1 ";
            Assert.AreEqual(whereClause, queryBuilder.WithDTO(dto).GetWhereAndParameters("p"));
        }

        [TestMethod]
        [Description("Testa a geração do where e das cláusulas com uma key vazia")]
        public void GetWhereAndParametersWithBlankStringKeyTest()
        {
            Dictionary<String, Object> filters = MockFilters();
            filters.Add("", "LEANDRO");
            dto.Filters = filters;
            String whereClause = " where p.name like '%LEANDRO%' and p.email like '%a@b.c%' and p.phone like '%12345678998%' and 1 = 1 ";
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
            String selectWithParameters = "select * from Person p  where p.name like '%LEANDRO%' and p.email like '%a@b.c%' and p.phone like '%12345678998%' and 1 = 1  order by p.name asc offset 0 rows fetch next 10 rows only";
            Assert.AreEqual(selectWithParameters, queryBuilder.WithDTO(dto).GetQueryFromDTO("p", "Person"));
        }

        [TestMethod]
        [Description("Testa a geração do order by")]
        public void GetOrderByTest()
        {
            Assert.AreEqual(" order by p.name asc", queryBuilder.WithDTO(dto).GetOrderBy("p"));
        }

        [TestMethod]
        [Description("Testa a geração do offset de páginação")]
        public void GetOffSetTest()
        {
            Assert.AreEqual(" offset 0 rows fetch next 10 rows only", queryBuilder.WithDTO(dto).GetOffSet());
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

        [TestMethod]
        [Description("Testa a recuperação do realname de uma coluna no banco")]
        public void IsDateTimeTypeTest()
        {
            Assert.IsTrue(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("04/10/2017 19:52:07"));
            Assert.IsTrue(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("2017-10-04T19:52:07.663"));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("OLIVEIRA"));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType(""));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType(null));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("2017"));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("10"));
            Assert.IsFalse(queryBuilderCollaborator.WithDTO(dtoCollaborator).IsDateTimeType("2017-ASDFRT19:52:07.663"));
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
