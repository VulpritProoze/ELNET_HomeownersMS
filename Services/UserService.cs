using HomeownersMS.Data;

namespace HomeownersMS.Services
{
    public class UserService
    {
        private readonly HomeownersContext _context;

        public UserService(HomeownersContext context)
        {
            _context = context;
        }

    }
}
