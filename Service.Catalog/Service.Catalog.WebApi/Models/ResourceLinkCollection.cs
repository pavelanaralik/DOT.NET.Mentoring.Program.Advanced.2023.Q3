﻿using System.Collections;
using System.Runtime.Serialization;

namespace Service.Catalog.WebApi.Models;

[CollectionDataContract(Name = "Links", Namespace = "")]
public sealed class ResourceLinkCollection : IEnumerable<ResourceLink>
{
    private readonly List<ResourceLink> _links = new List<ResourceLink>();

    public ResourceLinkCollection()
    {
    }

    public ResourceLinkCollection(IEnumerable<ResourceLink> collection)
    {
        _links = new List<ResourceLink>(collection);
    }

    public void Add(ResourceLink item)
    {
        _links.Add(item);
    }

    public IEnumerator<ResourceLink> GetEnumerator() => _links.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _links.GetEnumerator();
}