using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.User;
using Tracker.Application.Interfaces;

namespace Tracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _unitOfWork.UsersRepository.SaveUserAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(userId, "User");
            }
            return _mapper.Map<UserDto>(user);
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UsersRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(userId, "User");
            }
            _mapper.Map(updateUserDto, user);
            await _unitOfWork.UsersRepository.UpdateUserAsync(user);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(userId, "User");
            }
            await _unitOfWork.UsersRepository.DeleteUserAsync(user);
            return true;
        }
    }
}