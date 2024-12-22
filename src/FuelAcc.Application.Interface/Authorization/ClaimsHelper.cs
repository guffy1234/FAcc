using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Enums;
using System.Security.Claims;

namespace FuelAcc.Application.Interface
{
    public static class ClaimsHelper
    {
        public static Claim MakeClaim(Type entityType, ApplicationAction appAction)
        {
            var name = MakeClaimName(entityType, appAction);
            return new Claim(name, true.ToString());
        }

        private static string MakeClaimName(Type entityType, ApplicationAction appAction)
        {
            return $"O|{entityType.Name}|{appAction}";
        }

        public static Claim MakeClaim(ApplicationArea eventArea, ApplicationAction appAction)
        {
            var name = MakeClaimName(eventArea, appAction);
            return new Claim(name, true.ToString());
        }

        private static string MakeClaimName(ApplicationArea appArea, ApplicationAction appAction)
        {
            return $"A|{appArea}|{appAction}";
        }

        public static IEnumerable<Claim> MakeEntityClaims<ENTITY>()
            where ENTITY : class, IRootEntity
        {
            yield return MakeClaim(typeof(ENTITY), ApplicationAction.Insert);
            yield return MakeClaim(typeof(ENTITY), ApplicationAction.Update);
            yield return MakeClaim(typeof(ENTITY), ApplicationAction.Delete);
            yield return MakeClaim(typeof(ENTITY), ApplicationAction.View);
        }

        public static IEnumerable<Claim> MakeAreaClaims(ApplicationArea appArea)
        {
            yield return MakeClaim(appArea, ApplicationAction.Insert);
            yield return MakeClaim(appArea, ApplicationAction.Update);
            yield return MakeClaim(appArea, ApplicationAction.Delete);
            yield return MakeClaim(appArea, ApplicationAction.View);
        }

        public static IEnumerable<Claim> MakeAllClaims()
        {
            var claims = new List<Claim>();
            claims.AddRange(MakeEntityClaims<Branch>());
            claims.AddRange(MakeEntityClaims<Product>());
            claims.AddRange(MakeEntityClaims<Storage>());
            claims.AddRange(MakeEntityClaims<Partner>());
            claims.AddRange(MakeEntityClaims<OrderIn>());
            claims.AddRange(MakeEntityClaims<OrderOut>());
            claims.AddRange(MakeEntityClaims<OrderMove>());
            claims.AddRange(MakeAreaClaims(ApplicationArea.Dictionary));
            claims.AddRange(MakeAreaClaims(ApplicationArea.Document));
            return claims;
        }
    }
}