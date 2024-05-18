using ConfigurationUtilities.Generic;
using DatabaseTransaction.DBContext;
using DataTransferObject.DBModel;
using Microsoft.EntityFrameworkCore;
using Models.InputModel.LoginInputModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UsersDAL
{
    public class UserDAL : IUserDAL
    {
        private readonly IGenericAppServices _igenericAppServices;
        public UserDAL(IGenericAppServices genericAppServices)
        {
            _igenericAppServices = genericAppServices;
        }
        public List<UserResponse> GetUserInfo(GetUserInfo userInfoDto)
        {
            int PageIndex = userInfoDto.PageIndex + 1;
            int StartRow = PageIndex * userInfoDto.PageSize - userInfoDto.PageSize;
            int PageSize = userInfoDto.PageSize;
            int ResultCount = 0;
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                var user = (from users in databaseContext.Users
                            select new UserResponse
                            {
                                Id = users.Id,
                                UserName = users.UserName,
                                Password = users.Password,
                                FirstName = users.FirstName,
                                LastName = users.LastName,
                                CreatedBy = users.CreatedBy,
                                CreatedOn = users.CreatedOn,
                                ModifiedBy = users.ModifiedBy,
                                ModifiedOn = users.ModifiedOn,
                                IsActive = users.IsActive
                            }).Where(p => (!string.IsNullOrEmpty(userInfoDto.UserName) ? p.UserName == userInfoDto.UserName : string.IsNullOrWhiteSpace(userInfoDto.UserName)) &&
                                          (!string.IsNullOrEmpty(userInfoDto.FirstName) ? p.FirstName == userInfoDto.FirstName : string.IsNullOrWhiteSpace(userInfoDto.FirstName)) &&
                                          (!string.IsNullOrEmpty(userInfoDto.LastName) ? p.LastName == userInfoDto.LastName : string.IsNullOrWhiteSpace(userInfoDto.LastName))
                                    ).ToList();


                /* Server Side Pagination */
                ResultCount = user.Count;
                user = user.Skip(StartRow).Take(PageSize).ToList();
                if (user.Count > 0)
                {
                    user[0].TotalRecordCount = ResultCount;
                    user[0].PageIndex = PageIndex;
                    user[0].PageSize = PageSize;
                }
                return user.ToList();
            }
        }

        public void AddUserInfo(AddUserInfo userInfoDto)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        User user = _igenericAppServices.AutoMapper<AddUserInfo, User>(userInfoDto);
                        dbContext.Add(user);
                        dbContext.SaveChanges();

                        userInfoDto.Id = user.Id;

                        transaction.Commit();

                        userInfoDto.Message = "User added successfully";
                    }
                    catch (Exception ex)
                    {
                        userInfoDto.Message = "Error Occured!!";
                        transaction.Rollback();
                    }
                }
            }
        }

        public User GetUser(string userName)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.Where(p => p.UserName == userName).FirstOrDefault();
                if (user == null)
                {
                    return new User();
                }
                else
                {
                    return user;
                }
            }
        }
        public User ValidateUserCredential(LoginInputModel loginInputModel)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.Where(p => p.UserName == loginInputModel.UserName && p.Password == loginInputModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return new User();
                }
                else
                {
                    return user;
                }
            }
        }

        public void DeleteUserInfo(DeleteUserInfo dto)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new User()
                        {
                            Id = (long)dto.Id
                        };
   
                        dbContext.Entry(user).State = EntityState.Deleted;
                        dbContext.SaveChanges();
                        transaction.Commit();
                        dto.Message = "user deleted successfully!";

                    }
                    catch (Exception ex)
                    {
                        dto.Message = "Error Occured!!";
                        transaction.Rollback();
                    }
                }
            }
        }

        public void UpdateUserInfo(UpdateUserInfo dto)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userData = dbContext.Users.Where(p => p.Id == dto.Id);
                        if (userData != null)
                        {
                            var user = _igenericAppServices.AutoMapper<UpdateUserInfo, User>(dto);
                            if (user != null)
                            {
                                dbContext.Update(user);
                                dbContext.SaveChanges();
                                transaction.Commit();

                                dto.Message = "user updated successfully!";
                            }
                        }
                        else
                        {
                            dto.Message = "user not available";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        dto.Message = "Error Occured!!";
                    }
                }
            }
        }
    }
}
