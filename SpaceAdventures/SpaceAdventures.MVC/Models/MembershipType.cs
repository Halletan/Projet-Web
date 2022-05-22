namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class MembershipType
    {
        public int IdMemberShipType { get; set; }
        public string Name { get; set; }
        public double DiscountFactor { get; set; }
    }

    [Serializable]
    public class MembershipTypes
    {
        public List<MembershipType> MembershipTypesList { get; set; }
    }
}
