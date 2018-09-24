using System.Linq;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.Entity;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extentions;
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


        public async Task<object> AddBoardAsync(AddBoardDto model, long userId)
        {
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name && d.UserId == userId))
                    .FirstOrDefaultAsync();

            if (boardInDb != null)
            {
                throw new BoardServiceException("Board with such name already added.");
            }

            Board boardToAdd = model.toBoard(userId);

            var board = await _boardService.InsertAsync(boardToAdd);
            return new {board};
        }

        public async Task<object> DeleteBoardAsync(DeleteBoardDto model, long userId)
        {
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Id == model.Id && d.UserId == userId))
                    .FirstOrDefaultAsync();

            if (boardInDb == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            var board = (await _boardService.RemoveAsync(boardInDb)).toBoardReturnDto();
            return new {board};
        }

        public async Task<object> GetBoardsAsync(
            long userId
        )
        {
            var personId =
                (await _personService.GetAllAsync(d => d.Id == userId))
                .FirstOrDefaultAsync().Result.Id;

            var boards =
                (await _boardService.GetAllAsync(d => d.UserId == personId)).Select(o => o.toBoardReturnDto()
                ).ToList();

            return boards;
        }
    }
}