using System.Collections.Generic;

namespace HATEOAS
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ObjectContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}