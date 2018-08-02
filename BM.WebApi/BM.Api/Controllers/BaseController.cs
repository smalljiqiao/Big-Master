using System.Web.Http;
using BM.Services.Data.Androids;
using BM.Services.Data.ShortMessages;
using BM.Services.Data.Users;
using BM.Services.Infrastructure;

namespace BM.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ApiController
    {
        #region Field

        /// <summary>
        /// 
        /// </summary>
        protected UserService UserService;

        /// <summary>
        /// 
        /// </summary>
        protected SmsService SmsService;

        /// <summary>
        /// 
        /// </summary>
        protected AndroidService AndroidService;

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        protected BaseController()
        {
            if (Singleton<UserService>.Instance == null)
                Singleton<UserService>.Instance = new UserService();

            if (Singleton<SmsService>.Instance == null)
                Singleton<SmsService>.Instance = new SmsService();

            if (Singleton<AndroidService>.Instance == null)
                Singleton<AndroidService>.Instance = new AndroidService();

            UserService = Singleton<UserService>.Instance;
            SmsService = Singleton<SmsService>.Instance;
            AndroidService = Singleton<AndroidService>.Instance;
        }
    }
}