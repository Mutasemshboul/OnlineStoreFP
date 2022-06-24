namespace OnlineStoreFP.Models
{
    public class JoinTable
    {
        public CategoryFp categoryFp { get; set; }
        public ProductFp productFp { get; set; }
        public UserFp userFp { get; set; }
        public ProductuserFp productuserFp { get; set; }
        public Store store { get; set; }
        public UserloginFp userloginFp { get; set; }

    }
}
