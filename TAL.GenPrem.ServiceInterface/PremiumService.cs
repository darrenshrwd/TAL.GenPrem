namespace TAL.GenPrem.ServiceInterface
{
    using System;
    using System.Net;
    using ServiceModel;
    using ServiceModel.Extensions;
    using ServiceStack;

    public class PremiumService : Service
    {
        public object Post(PremiumInput request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    throw new ArgumentException("We require your name");

                if (string.IsNullOrEmpty(request.Gender))
                    throw new ArgumentException("We require your gender");

                var age = request.DateOfBirth.CalculateAge();

                if (age < 18 || age > 65)
                    throw new ArgumentException("Can only receive a Premium if between the age of 18 and 65");

                var premium = PremiumCalculations.CalculatePremium(age, request.Gender);
                return new PremiumInputResponse { Result = $"Your premium is ${premium}" };
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new PremiumInputResponse { Errors = e.Message };
            }
        }
    }
}