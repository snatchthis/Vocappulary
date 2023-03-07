using System;
using Vocappulary.Persistence.Data;

namespace Vocappulary.Persistence.Services
{
    public class LearnItemRepository : Repository<LearnItem>
    {
        public LearnItemRepository(string databasePath) : base(databasePath)
        {
        }
    }
}

