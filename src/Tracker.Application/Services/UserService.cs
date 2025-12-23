using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.User;
using Tracker.Application.Interfaces;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace Tracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            _logger.LogDebug("Creating user with email {UserEmail}", createUserDto.Email);
            try
            {
                var existUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(createUserDto.Email);
                if (existUser != null)
                {
                    _logger.LogWarning("Duplicate user creation attempt with email {UserEmail}", createUserDto.Email);
                    throw new DuplicateEntityException("User", "Email", createUserDto.Email);
                }
                _logger.LogDebug("Hashing password for user {UserEmail}", createUserDto.Email);

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.PasswordHash);
                createUserDto.PasswordHash = passwordHash;
                var user = _mapper.Map<User>(createUserDto);

                await _unitOfWork.UsersRepository.SaveUserAsync(user);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation(
                        "User {UserId} created successfully with email {UserEmail}",
                        user.UserId,
                        createUserDto.Email);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user with email {UserEmail}", createUserDto.Email);
                throw;
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            _logger.LogDebug("Getting user with ID {UserId}", userId);
            try
            {
                var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);

                    throw new NotFoundException(userId, "User");
                }
                _logger.LogDebug("Successfully retrieved user with ID {UserId}", userId);

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user with ID {UserId}", userId);
                throw;
            }
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            _logger.LogDebug("Getting all users");
            try
            {
                var users = await _unitOfWork.UsersRepository.GetAllUsersAsync();
                _logger.LogInformation("Retrieved {UserCount} users", users.Count());

                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw;
            }
        }
        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            _logger.LogDebug("Updating user with ID {UserId}", userId);
            try
            {
                var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found for update", userId);
                    throw new NotFoundException(userId, "User");
                }

                _logger.LogDebug(
                    "Checking email uniqueness for user {UserId}. New email: {UserEmail}",
                    userId,
                    updateUserDto.Email);

                var existUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(updateUserDto.Email);
                if (existUser != null && existUser.UserId != userId)
                {
                    _logger.LogWarning(
                        "Duplicate email update attempt for user {UserId}. Email {UserEmail} already exists",
                        userId,
                        updateUserDto.Email);
                    throw new DuplicateEntityException("User", "Email", updateUserDto.Email);
                }

                if (!string.IsNullOrWhiteSpace(updateUserDto.PasswordHash))
                {
                    _logger.LogDebug("Updating password for user {UserId}", userId);
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.PasswordHash);
                    updateUserDto.PasswordHash = passwordHash;
                }
                else
                {
                    updateUserDto.PasswordHash = user.PasswordHash;
                }

                _mapper.Map(updateUserDto, user);
                await _unitOfWork.UsersRepository.UpdateUserAsync(user);

                _logger.LogInformation(
                    "User {UserId} updated successfully. Email: {UserEmail}",
                    userId,
                    updateUserDto.Email);

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with ID {UserId}", userId);
                throw;
            }
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            _logger.LogDebug("Attempting to delete user with ID {UserId}", userId);
            try
            {
                var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found for deletion", userId);

                    throw new NotFoundException(userId, "User");
                }
                await _unitOfWork.UsersRepository.DeleteUserAsync(user);
                _logger.LogInformation("User {UserId} deleted successfully", userId);

                return true;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user with ID {UserId}", userId);

                throw;
            }
        }
    }
}