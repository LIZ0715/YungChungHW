namespace Yungching.Common.Dto
{
    public class GetEstate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public int Range { get; set; }
        public bool Status { get; set; }
    }

    public class EstateDto
    {
        public int TotalCount { get; set; }
        public List<GetEstate> EstateList { get; set; }
    }
}
