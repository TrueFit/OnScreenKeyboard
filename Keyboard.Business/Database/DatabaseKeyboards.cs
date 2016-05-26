using Keyboard.Common.Interfaces;
using Model = Keyboard.Common.Models;
using Keyboard.Common.Enums;
using Keyboard.Domain.Queries;
using AutoMapper;
using Keyboard.Domain;
using Keyboard.Business.Logic;

namespace Keyboard.Business.Database
{
    public class DatabaseKeyboards : IKeyboards<Model.Keyboard>
    {
        private readonly IDataExecutor _dataExecutor;

        public DatabaseKeyboards()
        {
            _dataExecutor = new DataExecutor();
        }

        public DatabaseKeyboards(IDataExecutor dataExecutor)
        {
            _dataExecutor = dataExecutor;
        }

        public Model.Keyboard GetKeyboard(KeyboardType type)
        {
            using (var ctx = new CableProviderConnection())
            {
                string keyboardType = type.ToString();
                DvrKeyboard keyboard = _dataExecutor.ExecuteQuery(new GetKeyboard(ctx, keyboardType));

                IMapper mapHandler = MappingHandler.BuildMapper();
                return mapHandler.Map<Model.Keyboard>(keyboard);
            }
        }

    }
}
