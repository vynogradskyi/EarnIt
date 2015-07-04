using Nancy.Routing.Constraints;

namespace EarnIt.Ninja.WebAPI.Infrastructure.Constraints
{
    public class EmailRouteSegmentConstraint : RouteSegmentConstraintBase<string>
    {
        public override string Name
        {
            get { return "email"; }
        }

        protected override bool TryMatch(string constraint, string segment, out string matchedValue)
        {
            if (segment.Contains("@"))
            {
                matchedValue = segment;
                return true;
            }

            matchedValue = null;
            return false;
        }
    }
}