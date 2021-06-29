namespace OccultMerchant.Controllers
{
    public class Filter
    {
        
        public string name { get; set; }

        public Filter(string name = "")
        {
            this.name = name;
        }
    }
}