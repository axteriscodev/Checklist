using System;
using System.Collections.Generic;

namespace TDatabase.Database;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public int? IdOrganization { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual Organization? IdOrganizationNavigation { get; set; }

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}
