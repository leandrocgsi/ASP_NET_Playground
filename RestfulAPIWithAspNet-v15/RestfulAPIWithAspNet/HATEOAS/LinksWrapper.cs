using System.Collections.Generic;

namespace RestfulAPIWithAspNet.HATEOAS
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public List<Link> Links { get; set; }
    }
}