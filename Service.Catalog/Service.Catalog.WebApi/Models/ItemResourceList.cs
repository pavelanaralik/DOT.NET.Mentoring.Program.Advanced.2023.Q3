using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Service.Catalog.WebApi.Models;

[DataContract(Name = "ItemList", Namespace = "")]
[KnownType(typeof(ResourceBase))]
public sealed class ItemResourceList : ResourceBase
{
    public ItemResourceList(IUrlHelper urlHelper, IReadOnlyCollection<ItemResource> items) : base(urlHelper)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    [DataMember(Order = 1)]
    public IEnumerable<ItemResource> Items { get; }
}