﻿using HATEOAS;
using I18N;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "REQUIRED_FIELD_TITLE_UNDEFINED")]
        public string Title { get; set; }

        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;

        public List<HyperMediaLink> Links { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}