using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Services.Concrete;

namespace Movie_API.Filter
{

    public class ValidationFilter : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var _validationService = context.HttpContext.RequestServices.GetService<IValidationService>();

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestResult();    
            }
            else
            {
                var user = context.ActionArguments["user"];
                var checkUserName = await _validationService.IsUserNameValid((UserRegisterDTO)user);
                var checkEmail = await _validationService.IsEmailValid((UserRegisterDTO)user);
                var checkPassword = _validationService.IsPasswordValid((UserRegisterDTO)user);
                var checkConditions = _validationService.IsConditionsValid(checkEmail, checkUserName, checkPassword);
                if(checkConditions != true)
                {
                    context.Result = new BadRequestResult();
                }
                
            }
               
        }
        
    }
}
