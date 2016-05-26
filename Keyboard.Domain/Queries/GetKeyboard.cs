using Keyboard.Common.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace Keyboard.Domain.Queries
{
    public class GetKeyboard : IDataQuery<DvrKeyboard>
    {

        private readonly CableProviderConnection _context;

        private readonly string _type;

        public GetKeyboard(CableProviderConnection context, string type)
        {
            _context = context;
            _type = type;
        }

        public DvrKeyboard Execute()
        {
            return _context.DvrKeyboards.Include(k => k.DvrKeys).Single(k => k.Type == _type);
        }

    }
}
