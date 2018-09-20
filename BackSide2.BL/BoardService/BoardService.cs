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
    public class BoardService : IBoardService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;

        public BoardService(
            IConfiguration configuration,
            IRepository<Board> boardService,
            IRepository<Person> personService
        )
        {
            _configuration = configuration;
            _boardService = boardService;
            _personService = personService;
        }


        public async Task<object> AddBoardAsync(
            AddBoardDto model, string userEmail
        )
        {
            var personId =
                (await _personService.GetAllAsync(d => d.Email == userEmail))
                .FirstOrDefaultAsync().Result.Id;

            Board board =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name && d.UserId == personId))
                    .FirstOrDefaultAsync();

            if (board != null)
            {
                throw new BoardServiceException("Board with such name already added.");
            }

            Board boardToAdd = new Board
            {
                UserId = personId,
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                IsPrivate = model.IsPrivate,
                CreatedBy = personId
            };

            await _boardService.InsertAsync(boardToAdd);
            return new
            {
                boardToAdd
            };
        }

        public async Task<object> DeleteBoardAsync(
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
                throw new BoardServiceException("Board not found.");
            }

            await _boardService.DeleteAsync(board);
            return board;
        }

        public async Task<object> GetBoardsAsync(
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