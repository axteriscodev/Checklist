using System;
using System.Collections.Generic;

namespace TDatabase.Database;

public partial class CompanySite
{
    public int IdCompany { get; set; }

    public int IdSite { get; set; }

    public string? JobsDescription { get; set; }

    public string? Note { get; set; }

    public int? SubcontractedBy { get; set; }

    public virtual Company IdCompanyNavigation { get; set; } = null!;

    public virtual Site IdSiteNavigation { get; set; } = null!;

    public virtual Company? SubcontractedByNavigation { get; set; }
}
