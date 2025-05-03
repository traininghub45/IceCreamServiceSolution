using AutoMapper;
using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using IceCreamService.Application.Helpers;

namespace IceCreamService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private static readonly byte[] StaticKey = System.Text.Encoding.UTF8.GetBytes("mohammed1961998");

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddAsync(User request)
        {
            var user = new User
            {
                FullName = request.FullName,
                UserName = request.Email,
                Email = request.Email,
                CreationDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
                IsActive = request.IsActive,
                Password = CryptoHelper.HashPassword(request.Password)
            };
            await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateAsync(User user, IFormFile file)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new Exception("User not found");
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingUser.CreationDate = user.CreationDate;
            existingUser.CreatedBy = user.CreatedBy;
            existingUser.phoneNumber = user.phoneNumber;
            if (file != null && file.Length > 0)
            {
                var imagePath = await SaveImageAsync(file);
                existingUser.UserImgProfile = imagePath;
            }
            await _userRepository.UpdateAsync(existingUser);
            return existingUser;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            // Define paths
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsPath = Path.Combine(wwwrootPath, "uploads");

            // Ensure uploads directory exists
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            // Generate file hash to check for duplicates
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0; // Reset position to read again

            // Compute hash of the file content
            using var sha256 = SHA256.Create();
            var fileHash = BitConverter.ToString(sha256.ComputeHash(ms)).Replace("-", "").ToLower();
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            // Search for existing file with same hash
            var existingFiles = Directory.GetFiles(uploadsPath, "*.*", SearchOption.AllDirectories)
                .Where(f => Path.GetExtension(f).ToLower() == fileExtension)
                .ToList();

            foreach (var existingFile in existingFiles)
            {
                using var existingStream = File.OpenRead(existingFile);
                var existingHash = BitConverter.ToString(sha256.ComputeHash(existingStream)).Replace("-", "").ToLower();

                if (existingHash == fileHash)
                {
                    // File with same content exists, return its relative path
                    return existingFile.Replace(wwwrootPath, "").TrimStart(Path.DirectorySeparatorChar);
                }
            }

            // No duplicate found, save new file
            var uniqueName = $"{fileHash}{fileExtension}";
            var fullPath = Path.Combine(uploadsPath, uniqueName);

            ms.Position = 0; // Reset position to save
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await ms.CopyToAsync(stream);
            }

            return $"uploads/{uniqueName}";
        }
        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteByIdAsync(id);
        }

        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            await _userRepository.ResetPasswordAsync(token, newPassword);
        }
    }
}

