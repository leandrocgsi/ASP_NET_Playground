﻿using System.Collections.Generic;

namespace HATEOAS
{
    /// <summary>
    /// Interface for models that support Hypermedia
    /// </summary>
    public interface ISupportsHyperMedia
    {
        /// <summary>
        /// A collection of hepermedia links
        /// </summary>
        List<HyperMediaLink> Links { get; set; }
    }
}