using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;

namespace BM.Api.AliSMS
{
    /// <summary>
    /// 阿里云短信类
    /// </summary>
    public static class SendSms
    {
        #region field
        //产品名称:云通信短信API产品,开发者无需替换
        private const string Product = "Dysmsapi";
        //产品域名,开发者无需替换
        private const string Domain = "dysmsapi.aliyuncs.com";
        // TODO 此处需要替换成开发者自己的AK(在阿里云访问控制台寻找)
        private const string AccessKeyId = "LTAIasx1fLtRNHpt";
        private const string AccessKeySecret = "ruM0ITtIYGtMogZERkGtwcPSOlbvJl";
        #endregion

        /// <summary>
        /// 发送短信验证码到指定的手机
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        public static bool SendSmsToPhone(string phone)
        {
            var sendSms = RequestSendFromApi(phone);
            if (sendSms.Code != null && sendSms.Code == "OK")
            {
                var queryReponse = QuerySendDetails(phone, sendSms.BizId);
                //Console.WriteLine("短信明细查询接口返回数据----------------");
                //Console.WriteLine("Code=" + queryReponse.Code);
                //Console.WriteLine("Message=" + queryReponse.Message);
                foreach (var smsSendDetailDto in queryReponse.SmsSendDetailDTOs)
                {
                    //发送成功的日记记录，这里有循环是因为一次可以对多个手机发送同一个验证码，这里我们只一台手机
                    //发一条一次，所以这里每次只会有一条记录
                    //Console.WriteLine("Content=" + smsSendDetailDto.Content);
                    //Console.WriteLine("ErrCode=" + smsSendDetailDto.ErrCode);
                    //Console.WriteLine("OutId=" + smsSendDetailDto.OutId);
                    //Console.WriteLine("PhoneNum=" + smsSendDetailDto.PhoneNum);
                    //Console.WriteLine("ReceiveDate=" + smsSendDetailDto.ReceiveDate);
                    //Console.WriteLine("SendDate=" + smsSendDetailDto.SendDate);
                    //Console.WriteLine("SendStatus=" + smsSendDetailDto.SendStatus);
                    //Console.WriteLine("Template=" + smsSendDetailDto.TemplateCode);
                }
                return true;
            }
            else
            {
                //发送失败的日记记录
                //Console.Write("短信发送接口返回的结果----------------");
                //Console.WriteLine("Code=" + sendSms.Code);
                //Console.WriteLine("Message=" + sendSms.Message);
                //Console.WriteLine("RequestId=" + sendSms.RequestId);
                //Console.WriteLine("BizId=" + sendSms.BizId);
                return false;
            }
        }

        /// <summary>
        /// 查询发送情况
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="bizId"></param>
        /// <returns></returns>
        private static QuerySendDetailsResponse QuerySendDetails(string phone, string bizId)
        {
            //初始化acsClient,暂不支持region化
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyId, AccessKeySecret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", Product, Domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            //组装请求对象
            QuerySendDetailsRequest request = new QuerySendDetailsRequest();
            //必填-号码
            request.PhoneNumber = phone;
            //可选-流水号
            request.BizId = bizId;
            //必填-发送日期 支持30天内记录查询，格式yyyyMMdd       
            request.SendDate = DateTime.Now.ToString("yyyyMMdd");
            //必填-页大小
            request.PageSize = 10;
            //必填-当前页码从1开始计数
            request.CurrentPage = 1;

            QuerySendDetailsResponse querySendDetailsResponse = null;
            try
            {
                querySendDetailsResponse = acsClient.GetAcsResponse(request);
            }
            catch (Aliyun.Acs.Core.Exceptions.ServerException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            catch (ClientException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            return querySendDetailsResponse;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <returns></returns>
        private static SendSmsResponse RequestSendFromApi(string phone)
        {
            var code = CreateCode();

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyId, AccessKeySecret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", Product, Domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            var request = new SendSmsRequest();
            SendSmsResponse response = null;
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = phone;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = "深圳市源安盾科技有限公司";
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = "SMS_140020270";
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = "{\"code\":\"" + code.Trim() + "\"}";
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = "yourOutId";
                //请求失败这里会抛ClientException异常
                response = acsClient.GetAcsResponse(request);
            }
            catch (Exception ex)
            {
                //TODO LOG EXCEPTION
            }
            return response;
        }

        /// <summary>
        /// 随机创建六位的验证码
        /// </summary>
        /// <returns></returns>
        private static string CreateCode()
        {
            var rd = new Random();
            var num = rd.Next(100000, 1000000);
            return num.ToString();
        }
    }
}