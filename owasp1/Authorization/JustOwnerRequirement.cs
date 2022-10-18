using Microsoft.AspNetCore.Authorization;

namespace owasp1.Authorization
{
    public class JustOwnerRequirement
        :IAuthorizationRequirement
    {

    }

    public class JustOwnerRequirementHandler
        : AuthorizationHandler<JustOwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JustOwnerRequirement requirement)
        {
            //ad hoc code to verify ower auth rules

            if ( context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
                
            }
            else
            {
                context.Fail();
            }
           

            return Task.CompletedTask;
        }
    }
}
