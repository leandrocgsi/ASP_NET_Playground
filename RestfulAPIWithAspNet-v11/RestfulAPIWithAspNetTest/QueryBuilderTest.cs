using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;
using System;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Test
{
    [TestClass]
    public class QueryBuilderTest
    {
        PagedSearchDTO<Person> dto = new PagedSearchDTO<Person>();
        QueryBuilder<Person> queryBuilder = new QueryBuilder<Person>();

        PagedSearchDTO<Book> dtoBook = new PagedSearchDTO<Book>();
        QueryBuilder<Book> queryBuilderBook = new QueryBuilder<Book>();

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
            Assert.AreEqual(baseSelect, queryBuilder.WithDTO(dto).GetSelectCount("p", "Person"));
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
            String selectWithParameters = "select * from Person p  where p.name like '%LEANDRO%' and p.email like '%a@b.c%' and p.phone like '%12345678998%' and 1 = 1  order by p.name asc limit 10 offset 1 ";
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
            Assert.AreEqual(" limit 10 offset 1 ", queryBuilder.WithDTO(dto).GetOffSet());
        }

        [TestMethod]
        [Description("Testa a recuperação do realname de uma coluna no banco")]
        public void GetRealColumnNameTest()
        {
            Assert.AreEqual("id", queryBuilderBook.WithDTO(dtoBook).GetRealColumnName("Id"));
        }

        [TestMethod]
        [Description("Testa a recuperação do realname de uma coluna no banco")]
        public void IsDateTimeTypeTest()
        {
            Assert.IsTrue(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("04/10/2017 19:52:07"));
            Assert.IsTrue(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("2017-10-04T19:52:07.663"));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("OLIVEIRA"));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType(""));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType(null));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("2017"));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("10"));
            Assert.IsFalse(queryBuilderBook.WithDTO(dtoBook).IsDateTimeType("2017-ASDFRT19:52:07.663"));
        }

        [TestMethod]
        [Description("Testa a geração dos alias das colunas")]
        public void TestGetColumnsAlias()
        {
            Assert.AreEqual("*", queryBuilder.WithDTO(dto).GetColumnsAlias());
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
