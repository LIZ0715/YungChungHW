namespace Yungching.Common.Dto
{
    public class CreateEstateDto
    {
        public int? Id { get; set; }
        public int MembershipId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public int Type { get; set; }
        public double Range { get; set; }
    }
}
