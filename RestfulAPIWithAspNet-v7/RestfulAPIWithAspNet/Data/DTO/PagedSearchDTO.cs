using System;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Data.DTO
{
    public class PagedSearchDTO<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections;
        public Dictionary<string, Object> filters { get; set; }
        public List<T> list { get; set; }

        public PagedSearchDTO() { }

        public PagedSearchDTO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.SortFields = sortFields;
            this.SortDirections = sortDirections;
        }

        public PagedSearchDTO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, Object> filters)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.SortFields = sortFields;
            this.SortDirections = sortDirections;
            this.filters = filters;
        }

        public PagedSearchDTO(int currentPage, string sortFields, string sortDirections) : this (currentPage, 10, sortFields, sortDirections)
        {
        }

        public int GetCurrentPage()
        {
            return CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize;
        }
    }
}