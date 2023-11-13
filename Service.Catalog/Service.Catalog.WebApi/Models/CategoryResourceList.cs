using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Service.Catalog.WebApi.Models;

[DataContract(Name = "CategoryList", Namespace = "")]
[KnownType(typeof(ResourceBase))]
public sealed class CategoryResourceList : ResourceBase
{
    public CategoryResourceList(IUrlHelper urlHelper, IReadOnlyCollection<CategoryResource> categories) : base(urlHelper)
    {
        Categories = categories ?? throw new ArgumentNullException(nameof(categories));
    }

    [DataMember(Order = 1)]
    public IEnumerable<CategoryResource> Categories { get; }
}