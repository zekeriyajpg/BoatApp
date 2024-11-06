using BoatApp.Business.DataProtection;
using BoatApp.Business.Operations.User.Dtos;
using BoatApp.Business.Types;
using BoatApp.Data.Repositories;
using BoatApp.Data.UnitOfWork;
using ProjectLayers.Data.Entities;
using ProjectLayers.Data.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());
            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Email adresi zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FİrstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Cripted(user.Password), // Parolayı kriptola ve kaydet
                BirthDate = user.BirthDate,
                UserType = UserType.Customer
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kullanıcı kaydında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());
            if (userEntity == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }

            // Kullanıcının girdiği şifreyi veritabanındaki kriptolu şifre ile karşılaştırmak için decrypt işlemi yap
            var decryptedStoredPassword = _protector.UnCripted(userEntity.Password);
            if (decryptedStoredPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        Firstname = userEntity.FİrstName,
                        Lastname = userEntity.LastName,
                        UserType = userEntity.UserType
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
        }
    }
}


