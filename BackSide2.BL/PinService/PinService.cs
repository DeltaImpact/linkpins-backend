using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.PinService
{
    public class PinService : IPinService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<Pin> _pinService;

        public PinService(
            IConfiguration configuration,
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<Pin> pinService
        )
        {
            _configuration = configuration;
            _boardService = boardService;
            _personService = personService;
            _pinService = pinService;
        }


   
    }
}