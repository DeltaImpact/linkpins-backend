using System.Linq;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.Entity;
using BackSide2.BL.Exceptions;
using BackSide2.BL.PinService;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.BoardService
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


        public async Task<object> AddPinAsync(
            AddPinDto model, string userEmail
        )
        {
            var personId =
                (await _personService.GetAllAsync(d => d.Email == userEmail))
                .FirstOrDefaultAsync().Result.Id;
            if (personId != null)
            {
                throw new PinServiceException("Person not found.");
            }
            var boardId =
                 (await _boardService.GetAllAsync(d => d.Name == model.BoardName && d.UserId == personId))
                    .FirstOrDefaultAsync().Result.Id;

            if (boardId != null)
            {
                throw new PinServiceException("Board not found.");
            }

            Pin pinToAdd = new Pin
            {
                BoardId = boardId,
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                CreatedBy = personId
            };

            await _pinService.InsertAsync(pinToAdd);
            return new
            {
                pinToAdd = pinToAdd
            };
        }

        public async Task<object> DeletePinAsync(
            DeleteBoardDto model, string userEmail
        )
        {
            var personId =
                (await _personService.GetAllAsync(d => d.Email == userEmail))
                .FirstOrDefaultAsync().Result.Id;

            Board board =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name && d.UserId == personId))
                    .FirstOrDefaultAsync();

            if (board == null)
            {
                throw new PinServiceException("Board not found.");
            }

            await _boardService.DeleteAsync(board);
            return board;
        }

        public async Task<object> GetBoards(
            string userEmail
        )
        {
            //Person person =
            //    await (await _personService.GetAllAsync(d => d.Email == userEmail))
            //        .FirstOrDefaultAsync();
            var personId =
                (await _personService.GetAllAsync(d => d.Email == userEmail))
                .FirstOrDefaultAsync().Result.Id;

            //var personId2 =
            //    await (await _personService.GetAllAsync(d => d.Email == userEmail))
            //        .FirstOrDefaultAsync().Id;
            var boards =
                (await _boardService.GetAllAsync(d => d.UserId == personId)).Select(o => new
                {
                    Name = o.Name,
                    Description = o.Description,
                    Img = o.Img,
                    IsPrivate = o.IsPrivate,
                    Created = o.Created,
                }).ToList();


            return boards;
        }
    }
}