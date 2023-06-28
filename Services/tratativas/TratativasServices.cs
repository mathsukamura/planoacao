using apiplanoacao.Data;
using System.Threading.Tasks;

namespace apiplanoacao.Services.tratativas
{
    public class TratativasServices
    {
        private readonly ContextDb _context;

        public TratativasServices(ContextDb context)
        {
            _context = context;
        }

    }
}
