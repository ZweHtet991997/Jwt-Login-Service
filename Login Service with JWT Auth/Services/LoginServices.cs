using Login_Service_with_JWT_Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_Service_with_JWT_Auth.Services
{
    public class LoginServices
    {
        private EFDBContext _dbContext;

        public LoginServices(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Register(UserModel model)
        {
            try
            {
                UserEntities entities = new UserEntities();
                entities.UserName = model.UserName;
                entities.Password = model.Password;
                entities.CreatedDate = DateTime.Now.Date;

                await _dbContext.User.AddAsync(entities);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Login(UserModel model)
        {
            try
            {
                UserEntities entities = new UserEntities();
                entities = await _dbContext.User.Where(x => x.UserName == model.UserName && x.Password == model.Password)
                    .FirstOrDefaultAsync();
                return entities != null ? entities.Id : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> GetUserProfile(int userId)
        {
            try
            {
                UserEntities entities = new UserEntities();
                entities = await _dbContext.User.Where(x => x.Id == userId).FirstOrDefaultAsync();
                if (entities != null)
                {
                    UserModel model = new UserModel();
                    model.Id = entities.Id;
                    model.UserName = entities.UserName;
                    model.CreatedDate = entities.CreatedDate.ToString("dd-MM-yyyy");
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
