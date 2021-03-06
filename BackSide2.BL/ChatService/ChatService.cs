﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.ChatDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.BL.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<Pin> _pinService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatService(
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<Pin> pinService, IRepository<BoardPin> boardPinService,
            IHttpContextAccessor httpContextAccessor)
        {
            _boardService = boardService;
            _personService = personService;
            _pinService = pinService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
        }


        public Task<ReceiveMessageDto> SendMessageAsync(SendMessageDto Dto)
        {
            throw new NotImplementedException();
        }

        public Task<ReceiveMessageDto> GetLastMessagesInDialogAsync(int interlocutorId)
        {
            throw new NotImplementedException();
        }

        public async Task<long> AddPinAsync(AddPinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();
            var boardInDb =
                await _boardService.GetByIdAsync(model.BoardId);
            if (boardInDb == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            if (boardInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to edit this board.");
            }

            var pin = await _pinService.InsertAsync(model.ToPin(usr));
            var relation = new BoardPin
            {
                CreatedBy = usr.Id,
                Pin = pin,
                Board = boardInDb
            };
            await _boardPinService.InsertAsync(relation);
            return  pin.Id;
        }

        public async Task<PinReturnDto> GetPinAsync(int pinId)
        {
            var pin =
                await (await _pinService.GetAllAsync(d => d.Id == pinId, x => x.BoardPins))
                    .FirstOrDefaultAsync();

            if (pin == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            var boards =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId, x => x.Board))
                    .Select(e => e.Board.ToBoardReturnDto()).ToListAsync();


            return pin.ToPinReturnDto(boards);
        }

        public async Task<PinReturnDto> DeletePinAsync(int pinId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pin =
                await _pinService.GetByIdAsync(pinId);

            if (pin == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            if (pin.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            var allPinConnections =
                await (await _boardPinService.GetAllAsync(d => d.Pin == pin, i => i.Board)).ToListAsync();


            foreach (var pinConnection in allPinConnections)
            {
                await _boardPinService.RemoveAsync(pinConnection);
            }

            var removedPin = await _pinService.RemoveAsync(pin);
            return removedPin.ToPinReturnDto();
        }

        public async Task<PinReturnDto> UpdatePinAsync(UpdatePinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pinOld =
                await _pinService.GetByIdAsync(model.Id);
            if (pinOld == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            if (pinOld.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            var board =
                await _pinService.UpdateAsync(model.ToPin(pinOld, userId));
            return board.ToPinReturnDto();
        }
    }
}