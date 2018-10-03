using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Entity.BoardDto;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extentions;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardService(
            IConfiguration configuration,
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<BoardPin> boardPinService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _boardService = boardService;
            _personService = personService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<object> AddBoardAsync(AddBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name && d.Person.Id == userId))
                    .FirstOrDefaultAsync();
            if (boardInDb != null)
            {
                throw new BoardServiceException("Board with such name already added.");
            }

            Board boardToAdd = model.ToBoard(usr);

            var board = (await _boardService.InsertAsync(boardToAdd)).ToBoardReturnDto();

            return new {board};
        }

        public async Task<object> DeleteBoardAsync(DeleteBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Id == model.Id && d.Person.Id == userId))
                    .FirstOrDefaultAsync();

            if (boardInDb == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            var board = (await _boardService.RemoveAsync(boardInDb)).ToBoardReturnDto();
            return new {board};
        }

        public async Task<object> UpdateBoardAsync(UpdateBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boardOld =
                await (await _boardService.GetAllAsync(d => d.Id == model.Id)).FirstOrDefaultAsync();
            if (boardOld == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            var boardWithSameName =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name)).FirstOrDefaultAsync();
            if (boardWithSameName != null && model.Id != boardWithSameName.Id)
            {
                throw new BoardServiceException("Board with same name already exist.");
            }
            var board =
                await _boardService.UpdateAsync(model.ToBoard(boardOld, userId));
            return new { board };
        }

        public async Task<object> GetBoardAsync(
            int boardId
        )
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var pins =
            //    await (await _boardPinService.GetAllAsync(d => d.Board.Id == boardId, x => x.Pin)).Select(e => e.Pin.ToPinReturnDto())
            //        .ToListAsync();

            var board =
                await (await _boardService.GetAllAsync(d => d.Id == boardId, x => x.Person)).FirstOrDefaultAsync();

            bool isOwner = board.Person.Id == userId;
            //var board =
            //    await (await _boardService.GetAllAsync(d => d.Person.Id == personId && d.Id == boardId,
            //            x => x.BoardPins)).Include("BoardPins.Pin")
            //        .FirstOrDefaultAsync();
            //var asd = board.BoardPins.Select(e => e.Pin.toPinReturnDto()).ToList();
            if (board == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            //return board.ToBoardReturnDto(pins, isOwner);
            return board.ToBoardReturnDto(isOwner);
        }

        public async Task<object> GetBoardsAsync()
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boards = (await _personService.GetAllAsync(d => d.Id == userId, x => x.Boards)).FirstOrDefault()?.Boards
                .Select(o => o.ToBoardReturnDto()
                ).ToList();
            return boards;
        }
    }
}