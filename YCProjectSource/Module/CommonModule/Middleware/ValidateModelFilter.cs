using CommonModule.DTOs;
using CommonModule.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Module.CommonModule.DTOs;
using Module.CommonModule.Interfaces;

namespace CommonModule.Middleware
{
    public class ValidateModelFilter : IActionFilter
    {
        private readonly ICommonService _commonService;

        public ValidateModelFilter(ICommonService commonService)
        {
            _commonService = commonService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                JsonResponse rt = new JsonResponse();
                rt.Status.Code = ApiStatusCode.ERR_DATA_VALIDATE;
                rt.Status.Desc = "資料驗證錯誤";
                rt.Status.Errors = context.ModelState.AllErrors();

                // 回應結果
                context.Result = new JsonResult(rt);
            }
        }

    }
}