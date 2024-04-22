using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDatabase.Database;
using DB = TDatabase.Database.DbCsclDamicoContext;

namespace TDatabase.Queries
{
    public class ChoiceDbHelper
    {

        public static List<ChoiceModel> Select(DB db)
        {
            return [.. db.Choices.Select(c => new ChoiceModel
            {
                Id = c.Id,
                Value = c.Value,
                Tag = c.Tag,
            })];
        }

        public static async Task<int> Insert(DB db, ChoiceModel choice)
        {
            var choiseId = 0;
            try
            {
                var nextId = (db.Choices.Any() ? db.Choices.Max(x=>x.Id) : 0) + 1;
                Choice newChoice = new()
                {
                    Id = nextId,
                    Value = choice.Value,
                    Tag = choice.Tag,
                };
                db.Choices.Add(newChoice);
                await db.SaveChangesAsync();
                choiseId = nextId;
            }catch (Exception) { }

            return choiseId;
        }

        public static async Task<List<int>> Update(DB db, List<ChoiceModel> choices)
        {
            List<int> modified = [];
            try
            {
                foreach(var elem in choices)
                {
                    var c = db.Choices.Where(x => x.Id == elem.Id).SingleOrDefault();
                    if (c is not null)
                    {
                        c.Value = elem.Value;
                        c.Tag = elem.Tag;
                        if( await db.SaveChangesAsync() > 0)
                        {
                            modified.Add(elem.Id);
                        }
                    }
                }
            }
            catch (Exception) { }

            return modified;
        }

        public static async Task<List<int>> Delete(DB db, List<ChoiceModel> choices)
        {
            List<int> deleted = [];
            try
            {
                foreach (var elem in choices)
                {
                    var c = db.Choices.Where(x => x.Id == elem.Id).SingleOrDefault();
                    if (c is not null)
                    {
                        db.Choices.Remove(c);
                        if (await db.SaveChangesAsync() > 0)
                        {
                            deleted.Add(elem.Id);
                        }
                    }
                }
            }
            catch (Exception) { }

            return deleted;
        }
    }
}
