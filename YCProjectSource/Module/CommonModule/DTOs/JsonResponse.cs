
namespace Module.CommonModule.DTOs
{
    /// <summary>
    /// 泛型 Rest 回覆物件
    /// </summary>
    /// <typeparam name="T">泛型類別</typeparam>
    public class JsonResponse<T>
    {
        /// <summary>
        /// c'tor
        /// </summary>
        public JsonResponse()
        {
            Status = new Status
            {
                Code = "0",
                Desc = "交易成功",
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="code"></param>
        public JsonResponse(string code)
        {
            Status = new Status
            {
                Code = code,
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        public JsonResponse(string code, string desc)
        {
            Status = new Status
            {
                Code = code,
                Desc = desc,
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        public Status Status { get; set; }

        public T Body { get; set; }
    }

    /// <summary>
    /// 無泛型 Rest 回覆物件
    /// </summary>
    public class JsonResponse
    {
        //public List<string> Errors { get; set; }

        /// <summary>
        /// c'tor
        /// </summary>
        public JsonResponse()
        {
            Status = new Status
            {
                Code = "0",
                Desc = "交易成功",
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="code"></param>
        public JsonResponse(string code)
        {
            Status = new Status
            {
                Code = code,
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        public JsonResponse(string code, string desc)
        {
            Status = new Status
            {
                Code = code,
                Desc = desc,
                TrxTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
        }

        public Status Status { get; set; }
    }
}