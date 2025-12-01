namespace TDatabase.Queries;

using System.Diagnostics;
using Shared.Documents;
using TDatabase.Database;
using DB = TDatabase.Database.ChecklistContext;


public class ConstructorSiteDbHelper
{

    #region Query principali cantiere

    public static List<SiteModel> Select(DB db, int organizationId, int idConstructorSite = 0)
    {
        var constructorSites = db.Sites.AsQueryable();

        if (idConstructorSite > 0)
        {
            constructorSites = constructorSites.Where(x =>x.IdOrganization == organizationId && x.Id == idConstructorSite);
        }

        return constructorSites.Where(x => x.IdOrganization == organizationId).Select(x => new SiteModel()
        {
            Id = x.Id,
            Name = x.Name,
            JobDescription = x.JobDescription ?? "",
            Address = x.Address ?? "",
            StartDate = x.StartDate ?? DateTime.Now,
            EndDate = x.EndDate,
            Client = db.Clients.Where(c => c.Id == x.IdClient).Select(nc => new ClientModel()
            {
                Id = nc.Id,
                Name = nc.Name,
            }).FirstOrDefault() ?? new(),
            Companies = (from cc in db.CompanySites
                         join c in db.Companies on cc.IdCompany equals c.Id
                         where cc.IdSite == x.Id
                         select new CompanyModel()
                         {
                             Id = c.Id,
                             CompanyName = c.CompanyName ?? "",
                             JobsDescriptions = cc.JobsDescription ?? "",
                             SubcontractedBy = cc.SubcontractedBy }).ToList(),
        }).ToList();
    }

    public static async Task<int> Insert(DB db, SiteModel constructorSite, int organizationId)
    {
        var siteId = 0;
        try
        {
            var nextId = (db.Sites.Any() ? db.Sites.Max(x => x.Id) : 0) + 1;
            Site newConstructorSite = new()
            {
                Id = nextId,
                Name = constructorSite.Name,
                JobDescription = constructorSite.JobDescription,
                Address = constructorSite.Address,
                StartDate = constructorSite.StartDate,
                EndDate = constructorSite.EndDate,
                IdClient = constructorSite.Client.Id > 0 ? constructorSite.Client.Id : null,
                IdOrganization = organizationId
            };
            //associo le aziende al cantiere
            HandleCompaniesToConstructionSite(db, constructorSite.Companies, constructorSite.Id);

            db.Sites.Add(newConstructorSite);
            await db.SaveChangesAsync();
            siteId = nextId;
        }
        catch (Exception) { }

        return siteId;
    }

    public static async Task<List<int>> Update(DB db, List<SiteModel> constructorSites)
    {
        List<int> modified = [];
        try
        {
            foreach (var elem in constructorSites)
            {
                var m = db.Sites.Where(x => x.Id == elem.Id).SingleOrDefault();
                if (m is not null)
                {
                    m.Name = elem.Name;
                    m.Address = elem.Address;
                    m.StartDate = elem.StartDate;
                    m.EndDate = elem.EndDate;
                    m.IdSico = elem.IdSico;
                    m.IdSicoInProgress = elem.IdSicoInProgress;
                    m.PreliminaryNotificationStart = elem.PreliminaryNotificationStartDate;
                    m.PreliminaryNotificationInProgress = elem.PreliminaryNotificationInProgress;
                    if (elem.Client is not null && elem.Client.Id > 0)
                    {
                        m.IdClient = elem.Client.Id;
                    }
                    m.JobDescription = elem.JobDescription;

                    HandleCompaniesToConstructionSite(db, elem.Companies, elem.Id);

                    if (await db.SaveChangesAsync() > 0)
                    {
                        modified.Add(elem.Id);
                    }
                }

            }
        }
        catch (Exception) { }

        return modified;
    }

    public static async Task<List<int>> Hide(DB db, List<SiteModel> constructorSites)
    {
        List<int> hiddenItems = [];
            try
            {
                foreach (var elem in constructorSites)
                {
                    var mc = db.Sites.Where(x => x.Id == elem.Id).SingleOrDefault();
                    if (mc is not null)
                    {
                        mc.Active = false;
                        if (await db.SaveChangesAsync() > 0)
                        {
                            hiddenItems.Add(elem.Id);
                        }
                    }
                }
            }
            catch (Exception) { }

            return hiddenItems;

    }

    #endregion

    #region Associazione Cantiere ed Aziende 


    /// <summary>
    /// Query parziale (non viene eseguito il salvataggio) per associare le anziede al cantiere
    /// </summary>
    /// <param name="db"></param>
    /// <param name="companies"></param>
    /// <param name="idSite"></param>
    public static void HandleCompaniesToConstructionSite(DB db, List<CompanyModel> companies, int idSite)
    {
        var newCompaniesIds = companies.Select(x => x.Id).ToList();

        var currentAssociatedCompanies = db.CompanySites.Where(x => x.IdSite == idSite).ToList();

        var currentAssociatedCompaniesIds = currentAssociatedCompanies.Select(x => x.IdCompany).ToList();

        var companiesToRemove = currentAssociatedCompanies.Where(x => !newCompaniesIds.Contains(x.IdCompany)).ToList();

        var companiesToAdd = companies.Where(x => !currentAssociatedCompaniesIds.Contains(x.Id)).ToList();

        foreach (var comp in companiesToRemove)
        {
            db.CompanySites.Remove(comp);
        }

        foreach (var comp in companiesToAdd)
        {
                var CompConstruct = new CompanySite()
                {
                    IdCompany = comp.Id,
                    IdSite = idSite,
                    JobsDescription = comp.JobsDescriptions,
                    SubcontractedBy = comp.SubcontractedBy,
                };
                db.CompanySites.Add(CompConstruct);
            // quando inseriremo i valori di JobsDescription e SubcontractedBy aggiungere anche else
            // di aggiornamento, visto che potrebbe aver aggiornato anche quei campi
        }
    }

    #endregion
}
